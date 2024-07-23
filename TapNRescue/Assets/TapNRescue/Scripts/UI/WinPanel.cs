using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    //[Header("SINGLETONS")]
    //ObjectSpawner objectSpawner;
    //GameManager gameManager;
    //MovesPanel movesPanel;

    //[SerializeField] TMP_Text levelText;
    //[SerializeField] CanvasGroup canvasGroup;
    ////[SerializeField] float timeToFadeIn = 0.5f;
    //[SerializeField] Transform completedTextTransfom;
    ////[SerializeField] TMP_Text levelText;
    ////[SerializeField] Transform levelUpTransform;
    ////[SerializeField] GameObject rewardGroupTransform;
    //[SerializeField] Transform rewardGoldTransform;
    //[SerializeField] TMP_Text collectedGoldText;
    ////[SerializeField] TMP_Text totalGoldText;
    ////int collectedCoins = 0;
    //[SerializeField] GameObject horButtons;
    //[SerializeField] Button nextLevelButton;
    //[SerializeField] Button claim2xButton;
    //[SerializeField] Button restartButton;
    //[SerializeField] bool showRestartButton = false;
    //[SerializeField] bool showClaim2xButton = true;

    //[SerializeField] Transform popUpPanel;

    ////int gameLevel = 0;


    ////[Space(5), Header("Particles")]
    ////[SerializeField] ParticleSystem confettiRain;
    ////[SerializeField] ParticleSystem levelUpParticles;
    ////[SerializeField] List<ParticleSystem> fireworksList;


    //// Start is called before the first frame update

    //private void Start()
    //{
    //    objectSpawner = ObjectSpawner.Instance;
    //    gameManager = GameManager.Instance;
    //    movesPanel = MovesPanel.Instance;
    //    collectedGoldText.text = "+" + gameManager.collectedGold;
    //    levelText.text = "Level " + (gameManager.GameLevel + 1);

    //    nextLevelButton.onClick.AddListener(() =>
    //    {
    //        NextLevel();
    //    });

    //    //TURN ON OFF CLAIM BUTTON
    //    //showClaim2xButton = false;
    //    if (showClaim2xButton)
    //    {
    //        claim2xButton.gameObject.SetActive(true);
    //    }

    //    //TESTING
    //    //showRestartButton = false;
    //    if (showRestartButton)
    //    {
    //        restartButton.gameObject.SetActive(true);
    //        restartButton.onClick.AddListener(() =>
    //        {
    //            ReplayLevel();
    //        });
    //    }
    //}

    //public void OpenWinPanel()
    //{
    //    canvasGroup.DOFade(1, 0.15f).OnComplete(() =>
    //    {
    //        TopBar.Instance.SetParentGoldStatTransform(transform, false);
            
    //        Invoke(nameof(PopupPanelScale), 0.5f);
    //    });
    //}

    //void PopupPanelScale()
    //{
    //    AudioManager.Instance.Play("popup", vol: 0.5f, pitch: 1.2f);
    //    popUpPanel.transform.DOScale(1, 0.15f).SetEase(Ease.InOutBounce).OnComplete(() =>
    //    {
    //        if (movesPanel.Moves > 0)
    //        {
    //            movesPanel.transform.SetParent(transform);
    //            StartCoroutine(MoveGoldToReward(movesPanel.transform.position, rewardGoldTransform.position));
    //        }
    //        else
    //        {
    //            ShowContinueButton();
    //            //Invoke(nameof(ShowContinueButton), 0.5f);
    //        }
    //    });
    //}

    //public void ShowContinueButton()
    //{
    //    movesPanel.transform.gameObject.SetActive(false);
    //    horButtons.SetActive(true);
    //    horButtons.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    //}

    //IEnumerator changeValueCoroutine;
    //IEnumerator ChangeValueCoroutine(int v_end, float duration)
    //{
    //    int v_start = movesPanel.Moves;
    //    int _moves = movesPanel.Moves;
    //    float elapsed = 0.0f;
    //    while (elapsed < duration)
    //    {
    //        _moves = (int)Mathf.Lerp(v_start, v_end, elapsed / duration);
    //        movesPanel.UpdateMovesText(_moves);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    _moves = v_end;
    //    movesPanel.UpdateMovesText(_moves);
    //}

    //public void ChangeValue(int v_end, float duration)
    //{
    //    if (changeValueCoroutine != null)
    //    {
    //        StopCoroutine(changeValueCoroutine);
    //    }
    //    changeValueCoroutine = ChangeValueCoroutine(v_end, duration);
    //    StartCoroutine(changeValueCoroutine);
    //}

    //public void NextLevel()
    //{
    //    nextLevelButton.interactable = false;
    //    if (showClaim2xButton)
    //    {
    //        claim2xButton.interactable = false;
    //    }
    //    if (showRestartButton)
    //    {
    //        restartButton.interactable = false;
    //    }
    //    StartCoroutine(NextLevelCoroutine(rewardGoldTransform.position, TopBar.Instance.goldStatTransform.position));
    //}

    

    //public IEnumerator MoveGoldToReward(Vector2 from, Vector2 to)
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    ChangeValue(0, 0.3f);
    //    int _moves = movesPanel.Moves;
    //    for (int i = 0; i < 10; i++)
    //    {
    //        objectSpawner.PlaySound("cash_sound", transform.position, 0.2f);
    //        //movesPanel.UpdateMovesText(_moves - 1 - i);
    //        var _gold = objectSpawner.GetGameObject("Gold");
    //        _gold.transform.SetParent(this.transform);
    //        _gold.transform.position = new Vector2(from.x + Random.Range(-50, 50), from.y + Random.Range(-50, 50));
    //        _gold.transform.DOScale(0.75f, 0.5f);
    //        _gold.transform.DOMove(to, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
    //        {
    //            objectSpawner.ReleaseGameObject(_gold);   
    //        });

    //        yield return new WaitForSeconds(0.05f);
    //    }
    //    gameManager.collectedGold += _moves;
    //    collectedGoldText.text = "+" + gameManager.collectedGold;
    //    objectSpawner.PlaySound("cash_register", transform.position, 0.25f);
    //    Invoke(nameof(ShowContinueButton), 0.5f);
    //}

    //public IEnumerator NextLevelCoroutine(Vector2 from, Vector2 to)
    //{
    //    int _spawnCounter = 10;
    //    for (int i = 0; i < _spawnCounter; i++)
    //    {
    //        objectSpawner.PlaySound("cash_sound", transform.position, 0.15f);
    //        var _gold = objectSpawner.GetGameObject("Gold");
    //        _gold.transform.SetParent(this.transform);
    //        _gold.transform.position = new Vector2(from.x + Random.Range(-50, 50), from.y + Random.Range(-50, 50));
    //        _gold.transform.DOScale(0.75f, 0.5f);
    //        _gold.transform.DOMove(to, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
    //        {
    //            objectSpawner.ReleaseGameObject(_gold);
                
    //        });

    //        yield return new WaitForSeconds(0.05f);
    //    }

    //    TopBar.Instance.UpdateTotalGold(gameManager.collectedGold, true);
    //    objectSpawner.PlaySound("cash_register", transform.position, 0.25f);
    //    yield return new WaitForSeconds(1.0f);
    //    Invoke(nameof(LoadNextLevel), 0.5f);
    //}

    //public void LoadNextLevel()
    //{
    //    gameManager.GameLevel += 1;
    //    ES3.Save(GlobalSaveDefinitions.gameLevelDef, gameManager.GameLevel, GlobalSaveDefinitions.defaultSaveFile);
    //    int _sceneIndex = gameManager.GameLevel % GlobalSaveDefinitions.totalLevelCount;
    //    SceneManager.LoadScene("Level " + (_sceneIndex + 1));
    //}

    //public void ReplayLevel()
    //{
    //    var scene = SceneManager.GetActiveScene();
    //    Debug.Log("scenName = " + scene.name);
    //    SceneManager.LoadScene(scene.name);
    //}

    
}


//_totalCoins += gameManager.collectedCoins;
//totalCointsText.text = "" + _totalCoins;
//ES3.Save(GlobalSaveDefinitions.totalCoinsDef, _totalCoins, GlobalSaveDefinitions.defaultSaveFile);

//ES3.Save(GlobalSaveDefinitions.currentLevelIndexDef, gameManager.GameLevel, GlobalSaveDefinitions.defaultSaveFile);

//SceneManager.LoadScene(gameManager.GameLevel % GlobalSaveDefinitions.totalLevelCount + 1);

//gameLevel += 1;
//ES3.Save(GlobalSaveDefinitions.gameLevelDef, gameLevel, GlobalSaveDefinitions.defaultSaveFile);
//Debug.Log("Win Menu gameLevel = " + gameLevel % GlobalSaveDefinitions.totalLevelCount);
//int _sceneIndex = gameManager.GameLevel % GlobalSaveDefinitions.totalLevelCount;
//SceneManager.LoadScene("Level " + (_sceneIndex));

//private void OnEnable()
//{
//    LevelManager.levelSuccesedEvent += FadeIn;
//}

//public void OnDisable()
//{
//    LevelManager.levelSuccesedEvent -= FadeIn;
//}


//public void FadeIn()
//{
//    if (fadeInCoroutine != null)
//    {
//        StopCoroutine(fadeInCoroutine);
//    }
//    fadeInCoroutine = FadeInCoroutine();
//    StartCoroutine(fadeInCoroutine);
//}

//IEnumerator fadeInCoroutine;
//IEnumerator FadeInCoroutine()
//{
//    yield return new WaitForSeconds(1.5f);
//    canvasGroup.DOFade(1, timeToFadeIn).OnComplete(() =>
//    {
//        Invoke(nameof(FadeInCompletedTextTransform), 0.4f);
//    });
//}

//IEnumerator PlayFireWorksCoroutine(float time)
//{
//    //UIManager.Instance.UpdateLevelTextTransform(this.transform, 500, 2.25f, 0.25f);

//    foreach (var firework in fireworksList)
//    {
//        firework.gameObject.SetActive(true);
//        firework.Play();
//        yield return new WaitForSeconds(time);
//    }
//    yield return null;
//}

//void FadeInCompletedTextTransform()
//{
//    completedTextTransfom.DOScale(1, 0.5f).SetEase(Ease.InBack);
//    Invoke(nameof(ShowRewardGameObjectGroup), 0.25f);
//}

//public void ShowRewardGameObjectGroup()
//{
//    if (movesPanel.Moves > 0)
//    {
//        movesPanel.transform.SetParent(transform);
//    }
//    //rewardGroupTransform.GetComponent<CanvasGroup>().DOFade(1, 0.5f).OnComplete(() =>
//    //{
//    ////    if (movesPanel.Moves > 0) {
//    ////        StartCoroutine(MoveGoldToReward(movesPanel.transform.position, collectedCoinText.transform.position));
//    ////    }
//    ////    else
//    ////    {
//    ////        Invoke(nameof(ShowContinueButton), 0.25f);
//    ////    }

//    //    Invoke(nameof(ShowContinueButton), 0.25f);
//    //});

//}