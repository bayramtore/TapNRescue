using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
//using LionStudios.Suite.Core.LeanTween;
//using LionStudios.Suite.Analytics;


public class UIManager : Singleton<UIManager>
{

    public Transform headerTransform;//USED TO CHANGE PARENT OF GOLDMANAGER

    //public GameDefaults gameDefaults;

    [SerializeField] GameObject dimmedNaveBG;
    [SerializeField] GameObject settingContainer;
    bool isSettingOn = false;

    [Space(5)]
    [Header("Replay UI")]
    [SerializeField] GameObject ReplayPanel;
    [SerializeField] GameObject replayPopup;
    [SerializeField] Button replayCloseButton;
    [SerializeField] Button replayButton;

    [Space(5)]
    [Header("OutOfShots UI")]
    [SerializeField] GameObject outOfShotsPanel;
    [SerializeField] GameObject outOfShotsPopup;
    [SerializeField] Button outOfShotsReplayButton;
    [SerializeField] Button outOfShotsContinueButton;
    [SerializeField] TMP_Text extraShotsPriceText;

    [Space(5)]
    [Header("Win UI")]
    [SerializeField] GameObject winPanel;
    [SerializeField] TMP_Text rewardGoldText;
    [SerializeField] int rewardGold;
    [SerializeField] Transform rewardGoldIcon;
    [SerializeField] Button winNextLevelButton;
    [SerializeField] Button winDoubleButton;

    //[Space(5)]
    //[Header("START UI")]
    //[SerializeField] GameObject startPanel;
    //[SerializeField] Image startBG;
    //[SerializeField] GameObject startPopup;
    //[SerializeField] TMP_Text startLevelText;
    //[SerializeField] TMP_Text startGoalText;

    [Space(5)]
    [Header("OBSTACLE TOUCH UI")]
    [SerializeField] GameObject obstaclePanel;
    [SerializeField] GameObject obstaclePopup;
    [SerializeField] Button obstacleReplayButton;
    [SerializeField] Button obstacleContinueButton;

    [Space(5)]
    [Header("PLAY UI")]
    [SerializeField] TMP_Text shotCountText;
    [SerializeField] TMP_Text gameLevelText;
    [SerializeField] GameObject shotCountBG;



    int shot = 0;
    int gameLevel = 1;
    public int Shot
    {
        get => shot;
        set
        {
            shot = value;
            shotCountText.text = "SHOTS: " + shot;
        }
    }

    public void UpdateShots()
    {
        if (gameLevel < 5)
            return;
        Shot -= 1;
    }

    #region PLAY UI

    public void OpenReplayPanel()
    {
        ShowSettingInGame();
        InputManager.Instance.isDisplayOn = true;
        ReplayPanel.SetActive(true);

        Time.timeScale = 1f;
        AudioManager.Instance.Play("popup");
        ReplayPanel.GetComponent<CanvasGroup>().alpha = 1;
        LeanTween.scale(replayPopup, Vector3.one, 0.25f).setIgnoreTimeScale(true).setEaseOutBack();
    }

    public void CloseReplayPanel()
    {
        InputManager.Instance.isDisplayOn = false;
        Time.timeScale = 1f;
        AudioManager.Instance.Play("popup");
        LeanTween.scale(replayPopup, Vector3.zero, 0.25f).setIgnoreTimeScale(true).setEaseInBack().setOnComplete(() =>
        {
            LeanTween.alphaCanvas(ReplayPanel.GetComponent<CanvasGroup>(), 0, 0.25f).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                ReplayPanel.SetActive(false);
            });
        });
    }

    IEnumerator restartSceneCoroutine;
    IEnumerator RestartSceneCoroutine(string methodName)
    {
        //AudioManager.Instance.Play("gameOver");
        Invoke(methodName, 0);// CloseReplayPanel();
        yield return new WaitForSecondsRealtime(0.5f);
        int _gameLevel = (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0) + 1);

        int tempLife = PlayerPrefs.GetInt(GlobalSaveDefinitions.totalLifeDef, 5);
        if (PlayerPrefs.GetInt(GlobalSaveDefinitions.infinityLifeDef, 0) != 1)
        {
            tempLife -= 1;
            PlayerPrefs.SetInt(GlobalSaveDefinitions.totalLifeDef, tempLife);
        }
        
        if (tempLife == 0)
        {
            HomeScene();
        }
        else
        {
            Time.timeScale = 1f;
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    public void RestartScene(string methodName)
    {
        if (restartSceneCoroutine != null)
        {
            StopCoroutine(restartSceneCoroutine);
        }
        restartSceneCoroutine = RestartSceneCoroutine(methodName);
        StartCoroutine(restartSceneCoroutine);
    }


    IEnumerator nextSceneCoroutine;
    IEnumerator NextSceneCoroutine()
    {
        //AudioManager.Instance.Play("gameOver");
        //Invoke(methodName, 0);// CloseReplayPanel();
        yield return new WaitForSecondsRealtime(0.5f);
        //LIFE -= 1;
        //GridManager.Instance.DeleteData();


        //AudioManager.Instance.Play("click");
        //GAME LEVEL START FROM 0.
        int _gameLevel = PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0);
        _gameLevel += 1;
        PlayerPrefs.SetInt(GlobalSaveDefinitions.gameLevelDef, _gameLevel);
        int _sceneIndex = _gameLevel % GlobalSaveDefinitions.totalLevelCount;
        SceneManager.LoadScene("Level " + (_sceneIndex + 1));
    }

    public void NextScene()
    {
        //GAME LEVEL START FROM 0.
        int _gameLevel = PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0);

        _gameLevel += 1;
        PlayerPrefs.SetInt(GlobalSaveDefinitions.gameLevelDef, _gameLevel);
        if (_gameLevel < 5)
        {
            int _sceneIndex = _gameLevel % GlobalSaveDefinitions.totalLevelCount;
            SceneManager.LoadScene("Level " + (_sceneIndex + 1));
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }

    bool endGame = false;
    public void NextButton()
    {
        if (endGame)
            return;
        endGame = true;
        MoveGold(5, .1f, .5f);
    }

    //public void HomeScene()
    //{
    //    SceneManager.LoadScene(0);
    //}

    public void ShowSettingInGame()
    {
        AudioManager.Instance.Play("popup");
        if (showSettingInGameAnimCoroutine != null)
        {
            StopCoroutine(showSettingInGameAnimCoroutine);
        }
        showSettingInGameAnimCoroutine = ShowSettingInGameAnimCoroutine();
        StartCoroutine(showSettingInGameAnimCoroutine);
    }
    IEnumerator showSettingInGameAnimCoroutine;
    IEnumerator ShowSettingInGameAnimCoroutine()
    {
        isSettingOn = !isSettingOn;
        dimmedNaveBG.SetActive(isSettingOn);
        float pos = settingContainer.transform.GetChild(0).localPosition.x;
        if (isSettingOn)
        {
            InputManager.Instance.isDisplayOn = true;
            Time.timeScale = 0f;
            dimmedNaveBG.GetComponent<Image>().DOFade(1, 0.3f).SetUpdate(true);
            for (int i = 1; i < 5; i++)//Total child excluding settingbutton
            {
                var t = settingContainer.transform.GetChild(i);
                LeanTween.moveLocalX(t.gameObject, pos, 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
                yield return new WaitForSecondsRealtime(0.04f);
            }
        }
        else
        {
            InputManager.Instance.isDisplayOn = false;
            Time.timeScale = 1f;
            dimmedNaveBG.GetComponent<Image>().DOFade(0, 0.3f).SetUpdate(true);
            for (int i = 4; i > 0; i--)//Total child excluding settingbutton
            {
                var t = settingContainer.transform.GetChild(i);
                LeanTween.moveLocalX(t.gameObject, 250f, 0.2f).setIgnoreTimeScale(true);
                yield return new WaitForSecondsRealtime(0.04f);
            }
        }
    }

    public void HomeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void MoveHeaderOut()
    {
        LeanTween.moveLocalY(headerTransform.gameObject, headerTransform.localPosition.y + 250f, 0.25f).setEaseInBack();
    }

    public void MoveHeaderIn()
    {
        LeanTween.moveLocalY(headerTransform.gameObject, headerTransform.localPosition.y - 250f, 0.25f).setEaseOutBack();
    }

    #region OUT OF SHOTS
    public void OpenOutOfShotsPanel()
    {
        InputManager.Instance.isDisplayOn = true;
        outOfShotsPanel.SetActive(true);
        DOVirtual.DelayedCall(0.5f, () =>
        {
            LeanTween.alphaCanvas(outOfShotsPanel.GetComponent<CanvasGroup>(), 1, 0.25f).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                GoldManager.Instance.transform.SetParent(this.transform);
                //REMOVE FOR RELEASE. 
                extraShotsPriceText.text = GlobalSaveDefinitions.extraShotPrice.ToString();
                AudioManager.Instance.Play("popup");
                LeanTween.scale(outOfShotsPopup, Vector3.one, 0.25f).setIgnoreTimeScale(true).setEaseOutBack();
            });
        });
    }

    public void CloseOutOfShotsPanel()
    {
        InputManager.Instance.isDisplayOn = false;
        Time.timeScale = 1f;
        AudioManager.Instance.Play("popup");
        LeanTween.scale(outOfShotsPopup, Vector3.zero, 0.25f).setIgnoreTimeScale(true).setEaseInBack().setOnComplete(() =>
        {
            GoldManager.Instance.transform.SetParent(headerTransform);
            outOfShotsPanel.GetComponent<CanvasGroup>().alpha = 0;
            outOfShotsPanel.SetActive(false);
        });
    }

    public void AddExtraShot(int extraShotCount)
    {
        if (!InputManager.Instance.isDisplayOn)
            return;

        if (GoldManager.Instance.TotalGold < GlobalSaveDefinitions.extraShotPrice)
        {
            GoldManager.Instance.ShakeIcon();
            return;
        }

        GoldManager.Instance.TotalGold -= GlobalSaveDefinitions.extraShotPrice;
        AudioManager.Instance.Play("cash");
        Shot += extraShotCount;
        CloseOutOfShotsPanel();
    }





    #endregion OutOfShots


    public void OpenWinPanel()
    {
        InputManager.Instance.isDisplayOn = true;
        winPanel.SetActive(true);
        
        //levelCompletedText.text = "LEVEL " + (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0) + 1) + "\nCOMPLETED";
        rewardGoldText.text = "+" + rewardGold; 
        DOVirtual.DelayedCall(0.25f, () =>
        {
            //AudioManager.Instance.Play("gameWin");

            //REMOVE FOR RELEASE. 

            LeanTween.alphaCanvas(winPanel.GetComponent<CanvasGroup>(), 1, 0.25f).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                GoldManager.Instance.transform.SetParent(winPanel.transform);
                //AudioManager.Instance.Play("popup");
                //LeanTween.scale(winPopup, Vector3.one, 0.25f).setIgnoreTimeScale(true).setEaseOutBack();
            });



        });
    }

    //public void CloseWinPanel()
    //{
    //    //isDisplayOn = false;
    //    Time.timeScale = 1f;
    //    AudioManager.Instance.Play("popup");
    //    winBG.DOFade(1, .1f);
    //    LeanTween.scale(winPopup, Vector3.zero, 0.25f).setIgnoreTimeScale(true).setEaseInBack().setOnComplete(() =>
    //    {
    //        //winPanel.SetActive(false);
    //        NextScene();
    //    });
    //}

    public void MoveGold(int count, float timeInterval, float timeToMove)
    {
        if (moveGoldCoroutine != null)
        {
            StopCoroutine(moveGoldCoroutine);
        }
        moveGoldCoroutine = MoveGoldCoroutine(count, timeInterval, timeToMove);
        StartCoroutine(moveGoldCoroutine);
    }

    IEnumerator moveGoldCoroutine;
    IEnumerator MoveGoldCoroutine(int count, float timeInterval, float timeToMove)
    {
        int _goldValue = (int)(rewardGold / count);

        List<Gold> golds = new();

        for (int i = 0; i < rewardGold; i++)
        {
            //ObjectSpawner.Instance.PlaySound("coin", transform.position, 0.15f, pitch: 1.0f + i * 0.01f);
            var tempGold = ObjectSpawner.Instance.GetGameObject("Gold");
            tempGold.transform.SetParent(this.transform);
            tempGold.transform.localPosition = new Vector3(rewardGoldIcon.transform.localPosition.x + UnityEngine.Random.Range(-200, 200), rewardGoldIcon.transform.localPosition.y + UnityEngine.Random.Range(-200, 200), rewardGoldIcon.transform.localPosition.z);
            tempGold.layer = 5;//UI LAYER
            tempGold.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (tempGold.TryGetComponent<Gold>(out var _tempGold))
            {
                _tempGold.goldValue = _goldValue;
                _tempGold.pitch = 1.0f + 0.01f * i;
                golds.Add(_tempGold);
                //_tempGold.MoveGold(GoldManager.Instance.icon.transform, timeInterval * -.95f, timeToMove);
            }
            if(i%3 == 0)ObjectSpawner.Instance.PlaySound("coin", transform.position, 0.5f, pitch: 1);
            yield return new WaitForSecondsRealtime(timeInterval / 5);
        }

        foreach(var gld in golds)
        {
            LeanTween.move(gld.gameObject, GoldManager.Instance.icon.transform.position, timeToMove).setEaseInBack().setOnComplete(() =>
            {
                GoldManager.Instance.TotalGold += 1;

                //GoldManager.Instance.icon.transform.DOPunchScale(new Vector3(-1.01f, 1.01f, 1.01f), 0.1f);
                
                ObjectSpawner.Instance.ReleaseGameObject(gld.gameObject);
            });
            yield return new WaitForSecondsRealtime(timeInterval / 3);
        }

        ObjectSpawner.Instance.PlaySound("cash", transform.position, 0.5f, pitch: 1);
        yield return new WaitForSecondsRealtime(timeToMove);
        
        NextScene();
       
    }


  

    #endregion PlayUI


    #region MAIN METHODS

    protected override void OnAwake()
    {
        //startPanel.SetActive(true);
    }
    private void Start()
    {
        //int _defaultLevel = 6;//UPDATE TO 0 BEFORE SUBMITTING. ???
        gameLevel = (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, GlobalSaveDefinitions.defaultGameLevel) + 1);
        gameLevelText.text = "LVL " + gameLevel;
        
        if (gameLevel < 6)
        {
            shotCountBG.GetComponent<CanvasGroup>().alpha = 0;
        }

        Invoke(nameof(UpdateShotCount), 0.5f);
        

        winNextLevelButton.onClick.AddListener(() =>
        {
            NextButton();
        });

        replayButton.onClick.AddListener(() =>
        {
            RestartScene("CloseReplayPanel");
        });

        replayCloseButton.onClick.AddListener(() =>
        {
            CloseReplayPanel();
        });
        
       
    }

    void UpdateShotCount()
    {
        Shot = GameManager.Instance.animals.Count + 3;
    }

    #endregion MainMethods


    //#region Singletons
    //AudioManager audioManager;
    //GameManager gameManager;
    //InputManager inputManager;
    //ObjectSpawner objectSpawner;
    //TopBar topBar;
    //#endregion

    //[SerializeField] Transform gamePanelTransform;
    //public Transform GamePanelTransform { get => gamePanelTransform;  }

    //[SerializeField] InfoPanel infoPanel;
    //public InfoPanel InfoPanel { get => infoPanel; }


    //[SerializeField] SettingsPanel settingsPanel;
    //[SerializeField] ReplayPanel replayPanel;


    //#region WIN PANEL
    //[Space(5)]
    //[Header("Win Panel")]
    //[SerializeField] private GameObject winPanel;
    //[SerializeField] GameGiftPanel gameGiftPanel;
    //[SerializeField] Transform giftBoxParentTransform;
    //public Transform GiftBoxParentTransform { get => giftBoxParentTransform; set => giftBoxParentTransform = value; }
    //#endregion


    //#region FAIL PANEL
    //[Space(5)]
    //[Header("FAIL PANEL")]
    //[SerializeField] TMP_Text failCollectedCoin;
    //[SerializeField] private GameObject failPanel;
    //[SerializeField] Transform popUpFailPanel;
    //[SerializeField] TMP_Text extraMovePriceText;
    //[SerializeField] Button extraMoveButton;
    //[SerializeField] Button replayButton;
    //[SerializeField] Transform extraMoveTransform;
    //int maxLife = 5;
    //[SerializeField] int infinitySeconds = 900;//15 min or 900 sec.
    //#endregion


    //[SerializeField] GameObject goldPrefab;
    //[SerializeField] Transform lastFivesMovesTransform;
    //public static event Action gameBeganEvent;
    //public Button settingsButton;



    //private void Start()
    //{
    //    audioManager = AudioManager.Instance;
    //    gameManager = GameManager.Instance;
    //    inputManager = InputManager.Instance;
    //    objectSpawner = ObjectSpawner.Instance;
    //    topBar = TopBar.Instance;

    //    extraMoveButton.onClick.AddListener(() =>
    //    {
    //        CloseFailPanel();
    //    });

    //    replayButton.onClick.AddListener(() =>
    //    {
    //        popUpFailPanel.DOScale(0, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
    //        {
    //            if (failPanel.TryGetComponent<CanvasGroup>(out var canvasGroup))
    //            {
    //                //audioManager.Play("game_over");
    //                canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
    //                {
    //                    Invoke(nameof(ReplayLevel), 0.15f);
    //                });
    //            }
    //            else
    //            {
    //                Invoke(nameof(ReplayLevel), 0.5f);
    //            }


    //        });
    //    });
    //    Invoke(nameof(StartGame), 0.5f);
    //}

    //void StartGame()
    //{
    //    //gameBegan = true;
    //    SpawnManager.Instance.SpawnCoins();
    //    SpawnManager.Instance.InitialStarSpawn();

    //    SpawnManager.Instance.InitialBlockSpawn(BlockTypes.Ice, GameManager.Instance.LevelData.minIceOnScene, "iceBlock");
    //    SpawnManager.Instance.InitialBlockSpawn(BlockTypes.Heart, GameManager.Instance.LevelData.minHeartOnScene, "heartBlock");
    //    SpawnManager.Instance.InitialBlockSpawn(BlockTypes.RedRock, GameManager.Instance.LevelData.minRedRockOnScene, "redRockBlock");
    //    SpawnManager.Instance.InitialBlockSpawn(BlockTypes.Barrel, GameManager.Instance.LevelData.minBarrelOnScene, "barrelBlock");

    //    MakeStakers.Instance.SetupStackerPositions(gameManager.LevelData.numbOfStacker);
    //    gameBeganEvent?.Invoke();
    //    inputManager.EnableClicking();
    //}

    //public void HomeScene()
    //{
    //    SceneManager.LoadScene(0);
    //}

    //public void ReplayLevel()
    //{

    //    if (IsInfinityLife())
    //    {
    //        var scene = SceneManager.GetActiveScene();
    //        SceneManager.LoadScene(scene.name);
    //    }
    //    else
    //    {
    //        int _totalLife = ES3.Load(GlobalSaveDefinitions.totalLifeDef, GlobalSaveDefinitions.defaultSaveFile, 5);
    //        if (_totalLife == maxLife)
    //        {
    //            ES3.Save(GlobalSaveDefinitions.lastAddedTimeDef, DateTime.Now.ToString(), GlobalSaveDefinitions.defaultSaveFile);
    //        }
    //        _totalLife--;
    //        if (_totalLife < 0)
    //        {
    //            _totalLife = 0;
    //        }
    //        ES3.Save(GlobalSaveDefinitions.totalLifeDef, _totalLife, GlobalSaveDefinitions.defaultSaveFile);
    //        if (_totalLife > 0)
    //        {
    //            var scene = SceneManager.GetActiveScene();
    //            SceneManager.LoadScene(scene.name);
    //        }
    //        else
    //        {
    //            SceneManager.LoadScene(0);//HOME
    //        }
    //    }
    //}

    //public bool IsInfinityLife()
    //{
    //    if (ES3.Load(GlobalSaveDefinitions.infinityLifeDef, GlobalSaveDefinitions.defaultSaveFile, false))
    //    {
    //        DateTime startTimeInfityLife = StringToDate(ES3.Load(GlobalSaveDefinitions.startInfinityLifeTimeDef, GlobalSaveDefinitions.defaultSaveFile, DateTime.Now.ToString()));
    //        TimeSpan difference = DateTime.Now - startTimeInfityLife;
    //        if (difference.TotalSeconds < infinitySeconds)
    //            return true;
    //        ES3.Save(GlobalSaveDefinitions.infinityLifeDef, false, GlobalSaveDefinitions.defaultSaveFile);
    //    }

    //    return false;
    //}


    //DateTime StringToDate(string date)
    //{
    //    if (String.IsNullOrEmpty(date))
    //        return DateTime.Now;

    //    return DateTime.Parse(date);
    //}


    //public void OpenSettingsPanel()
    //{
    //    settingsPanel.gameObject.SetActive(true);
    //    settingsPanel.OpenSettingsPanel();
    //}

    //public void OpenReplayPanel(int index)
    //{
    //    replayPanel.gameObject.SetActive(true);
    //    replayPanel.OpenReplayPanel(index);
    //}


    //#region WIN PANEL
    //public void OpenWinMenu()
    //{
    //    SettingsButtonInterable(false);
    //    audioManager.ChangeValue("background_music", 0, 0.5f);
    //    AudioManager.Instance.Play("game_won");
    //    inputManager.DisableClicking();

    //    LionAnalytics.LevelComplete((gameManager.GameLevel + 1), gameManager.LevelData.moves, score:MovesPanel.Instance.Moves);

    //    //gameGiftPanel.OpenGameGiftPanel();
    //    if ((gameManager.GameLevel + 1) % 10 == 0)
    //    {
    //        //OPEN Card
    //        gameGiftPanel.OpenGameGiftPanel();
    //    }
    //    else if ((gameManager.GameLevel + 1) % 5 == 0)
    //    {
    //        //OPEN GIFT
    //        gameGiftPanel.OpenGameGiftPanel();
    //    }
    //    else
    //    {
    //        OpenWinPanel();
    //    }
    //}

    //public void OpenWinPanel()
    //{
    //    winPanel.SetActive(true);
    //    winPanel.GetComponent<WinPanel>().OpenWinPanel();
    //}


    //#endregion



    //#region  FAIL PANEL
    //public void OpenFailPanel()
    //{
    //    SettingsButtonInterable(false);
    //    audioManager.ChangeValue("background_music", 0, 0.5f);
    //    extraMoveButton.interactable = false;
    //    replayButton.interactable = false;
    //    inputManager.DisableClicking();
    //    failPanel.SetActive(true);

    //    LionAnalytics.LevelFail((gameManager.GameLevel + 1), attemptNum: gameManager.LevelData.moves);

    //    extraMovePriceText.text = gameManager.GameRules.extraMovePrice.ToString();


    //    extraMoveButton.interactable = true;
    //    if (failPanel.TryGetComponent<CanvasGroup>(out var canvasGroup))
    //    {
    //        //audioManager.Play("game_over");
    //        canvasGroup.DOFade(1, 0.5f).OnComplete(() =>
    //        {
    //            //topBar.goalsBarTransform.SetParent(failPanel.transform);
    //            audioManager.Play("popup");
    //            popUpFailPanel.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
    //            {
    //                extraMoveButton.interactable = true;
    //                replayButton.interactable = true;
    //            });
    //        });
    //    }

    //}


    //public void CloseFailPanel()
    //{
    //    if (topBar.TotalGold < gameManager.GameRules.extraMovePrice)
    //    {
    //        audioManager.Play("error");
    //        return;
    //    }
    //    SettingsButtonInterable(true);
    //    extraMoveButton.interactable = false;
    //    topBar.UpdateTotalGold(-gameManager.GameRules.extraMovePrice);

    //    StartCoroutine(SpawnGold(topBar.goalsBarTransform.position, extraMoveTransform.position));
    //}

    //public IEnumerator SpawnGold(Vector2 from, Vector2 to)
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        if (i % 3 == 0)
    //        {
    //            objectSpawner.PlaySound("cash_sound", transform.position, 0.25f);
    //        }
    //        var _gold = objectSpawner.GetGameObject("Gold");
    //        _gold.transform.SetParent(failPanel.transform);
    //        _gold.transform.position = new Vector2(from.x + UnityEngine.Random.Range(-50, 50), from.y + UnityEngine.Random.Range(-50, 50));
    //        _gold.transform.DOScale(0.5f, 0.5f);
    //        _gold.transform.DOMove(to, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
    //        {
    //            objectSpawner.PlaySound("cash_sound", transform.position, 0.25f);
    //            objectSpawner.ReleaseGameObject(_gold);
    //        });
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //    objectSpawner.PlaySound("cash_register", transform.position, 0.25f);
    //    yield return new WaitForSeconds(1.0f);

    //    audioManager.Play("popup");
    //    popUpFailPanel.DOScale(0, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
    //    {
    //        topBar.SetParentGoldStatTransform(transform, true);
    //        if (failPanel.TryGetComponent<CanvasGroup>(out var canvasGroup))
    //        {
    //            canvasGroup.DOFade(0, 0.25f).OnComplete(() =>
    //            {
    //                audioManager.ChangeValue("background_music", 1, 1.5f);
    //                LevelManager.Instance.isLevelActive = true;
    //                inputManager.EnableClicking();
    //                MovesPanel.Instance.Moves += 5;
    //                failPanel.SetActive(false);
    //            });
    //        }
    //    });
    //}

    //public void ShowLastFiveMovesNotice()
    //{
    //    lastFivesMovesTransform.gameObject.SetActive(true);
    //    lastFivesMovesTransform.DOScale(1.75f, .75f).SetEase(Ease.OutBack).OnComplete(() =>
    //    {
    //        lastFivesMovesTransform.DOScale(0.75f, 1.25f);
    //        lastFivesMovesTransform.DOLocalMoveY(400f, 1.5f).OnComplete(() =>
    //        {
    //            lastFivesMovesTransform.gameObject.SetActive(false);
    //        });
    //    });
    //}

    //public void StartInfinityLife()
    //{
    //    ES3.Save(GlobalSaveDefinitions.startInfinityLifeTimeDef, DateTime.Now.ToString(), GlobalSaveDefinitions.defaultSaveFile);
    //    ES3.Save(GlobalSaveDefinitions.infinityLifeDef, true, GlobalSaveDefinitions.defaultSaveFile);
    //}

    //#endregion

    //public void SettingsButtonInterable(bool interactable)
    //{
    //    //settingsButton.interactable = interactable;
    //}


    //private void OnEnable()
    //{
    //    LevelManager.levelFailedEvent += OpenFailPanel;
    //}
    //private void OnDisable()
    //{
    //    LevelManager.levelFailedEvent -= OpenFailPanel;
    //}
}


//public void UpdateSceneLight(float lightIntensity)
//{
//    ChangeValue(0, 0.5f);
//}

//IEnumerator changeValueCoroutine;
//IEnumerator ChangeValueCoroutine(float v_end, float duration)
//{
//    float v_start = mainLight.intensity;
//    float elapsed = 0.0f;
//    while (elapsed < duration)
//    {
//        mainLight.intensity = Mathf.Lerp(v_start, v_end, elapsed / duration);
//        elapsed += Time.deltaTime;
//        yield return null;
//    }
//    mainLight.intensity = v_end;
//}

//public void ChangeValue(float v_end, float duration)
//{
//    if (changeValueCoroutine != null)
//    {
//        StopCoroutine(changeValueCoroutine);
//    }
//    changeValueCoroutine = ChangeValueCoroutine(v_end, duration);
//    StartCoroutine(changeValueCoroutine);
//}

//void OpenGoalPanel()
//{
//    //UpdateLevelTextTransform(topBarTransform, -40, 1, timeForGoalPanel);
//    //goalPanelTransform.DOScale(1.25f, timeForGoalPanel).SetEase(Ease.OutBack).OnComplete(() =>
//    //{
//    //    //StartCoroutine(WaitToMoveGoalPanel());
//    //    if (goalsBarTransform.TryGetComponent<CanvasGroup>(out var canvasGroup))
//    //    {

//    //        canvasGroup.DOFade(1, timeForGoalPanel * 2).OnComplete(() =>
//    //        {
//    //            goalPanelTransform.SetParent(goalsBarTransform);

//    //            goalPanelTransform.DOScale(1.0f, timeForGoalPanel);

//    //            goalPanelTransform.DOLocalMove(goalsBarPos, timeForGoalPanel);

//    //            if (gameManager.GameLevel == 0)
//    //            {
//    //                infoPanel.OpenInfoPanel(0);//SHOW STACKING
//    //            }
//    //            else if (gameManager.GameLevel == gameManager.gameLevelToMakeBomb)
//    //            {
//    //                infoPanel.OpenInfoPanel(1);//SHOW BOMB INFO
//    //            }
//    //            else if(gameManager.GameLevel == gameManager.gameLevelToMergeBomb)
//    //            {
//    //                infoPanel.OpenInfoPanel(2);//MERGE BOMB INFO
//    //            }
//    //        });
//    //    }
//    //});
//}

//public void ShowPlayButton()
//{
//    if (gameBegan)
//        return;
//    playButton.gameObject.SetActive(true);
//    playButton.GetComponent<CanvasGroup>().DOFade(1, 0.25f).OnComplete(() =>
//    {
//        playButton.interactable = true;
//    });
//}

//public void HidePlayButton()
//{
//    playButton.interactable = false;
//    playButton.GetComponent<CanvasGroup>().DOFade(0, 0.25f).OnComplete(() =>
//    {
//        playButton.gameObject.SetActive(false);
//    });
//}



//public void CloseStartPanel(float timeToWait)
//{
//    if(closeStartPanelCoroutine != null)
//    {
//        StopCoroutine(closeStartPanelCoroutine);
//    }
//    closeStartPanelCoroutine = CloseStartPanelCoroutine(timeToWait);
//    StartCoroutine(closeStartPanelCoroutine);
//}

//IEnumerator closeStartPanelCoroutine;
//IEnumerator CloseStartPanelCoroutine(float timeToWait)
//{
//    yield return new WaitForSecondsRealtime(timeToWait);
//    AudioManager.Instance.Play("throw");
//    LeanTween.moveY(startPopup, ProgressManager.Instance.transform.position.y, 0.5f).setEaseInBack().setIgnoreTimeScale(true);
//    DOVirtual.DelayedCall(0.3f, () =>
//    {
//        if (startPopup.TryGetComponent<CanvasGroup>(out var startCanvasGroup))
//        {
//            ProgressManager.Instance.GetComponent<CanvasGroup>().DOFade(1.0f, 0.2f).SetUpdate(true);
//            startCanvasGroup.DOFade(0, 0.2f).SetUpdate(true).OnComplete(() =>
//            {
//                Time.timeScale = 1f;
//                startBG.DOFade(0, 0.1f).OnComplete(() =>
//                {
//                    startPanel.SetActive(false);
//                    if (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0) == boosterDefaultData.hammerUnlockLevel - 1)
//                    {
//                        OpenHammerPanel();
//                    }
//                    else if (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0) == boosterDefaultData.swapUnlockLevel - 1)
//                    {
//                        OpenSwapPanel();
//                    }
//                    else if (PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0) == boosterDefaultData.magnetUnlockLevel - 1)
//                    {
//                        OpenMagnetPanel();
//                    }
//                });
//            });
//        }
//    });
//}

