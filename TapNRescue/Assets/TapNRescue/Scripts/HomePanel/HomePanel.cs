using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;
//using LionStudios.Suite.Core.LeanTween;
using UnityEngine.EventSystems;


public class HomePanel : Singleton<HomePanel>
{
    #region VARIABLES
    [SerializeField] EventSystem eventSystem;
    [SerializeField] EventSystem[] eventSystems;
    public Transform headerTransform;
    [SerializeField] List<HomeLevel> homeLevels = new();
    [SerializeField] Button playButton;
    public int gameLevel;
    [SerializeField] GameObject coverScreen;
    #endregion Variables


    protected override void OnAwake()
    {
        gameLevel = PlayerPrefs.GetInt(GlobalSaveDefinitions.gameLevelDef, 0);
      
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameLevel < 5)
        {
            coverScreen.SetActive(true);
            int _sceneIndex = gameLevel % GlobalSaveDefinitions.totalLevelCount;
            SceneManager.LoadScene("Level " + (_sceneIndex + 1));
            return;
        }
       

        eventSystems = FindObjectsOfType(typeof(EventSystem)) as EventSystem[];

        if (eventSystems.Length > 1)
        {
            eventSystem.enabled = false;
        }

        if (PlayerPrefs.GetInt("homeLevel", 0) != gameLevel)
        {
            for (int i = 0; i < homeLevels.Count; i++)
            {
                homeLevels[i].UpdateHomeLevel(i, gameLevel + i);
            }
            UpdateCompletedLevel();
        }
        else
        {
            for (int i = 0; i < homeLevels.Count; i++)
            {
                homeLevels[i].UpdateHomeLevel(i + 1, gameLevel + i + 1);
            }
            homeLevels[0].glowParticles.SetActive(true);
        }

    }

    

    IEnumerator updateCompletedLevelCoroutine;
    IEnumerator UpdateCompletedLevelCoroutine()
    {
        homeLevels[0].glowParticles.SetActive(false);
        homeLevels[1].glowParticles.SetActive(true);
        yield return new WaitForSecondsRealtime(0.05f);
        homeLevels[0].tick.SetActive(true);
        LeanTween.scale(homeLevels[0].tick, Vector3.one, 0.3f).setEaseInOutBack().setOnComplete(() =>
        {
            StartCoroutine(MoveHomeLevels());
        });
        
        
    }

    IEnumerator MoveHomeLevels()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        foreach (var gObj in homeLevels)
        {
            float _tempY = gObj.GetComponent<RectTransform>().anchoredPosition.y - 250;
            gObj.GetComponent<RectTransform>().DOAnchorPosY(_tempY, 0.5f).SetEase(Ease.OutQuad);
        }
    }

    public void UpdateCompletedLevel()
    {
        if (updateCompletedLevelCoroutine != null)
        {
            StopCoroutine(updateCompletedLevelCoroutine);
        }
        updateCompletedLevelCoroutine = UpdateCompletedLevelCoroutine();
        StartCoroutine(updateCompletedLevelCoroutine);
    }

    public void PlayButtonPressed()
    {
        if (PlayerPrefs.GetInt(GlobalSaveDefinitions.totalLifeDef, 5) == 0 && !LifeManager.Instance.IsInfinityLife())
        {
            //NOT ENOUGH LIFE
            
            AudioManager.Instance.Play("error");
            LifeManager.Instance.icon.DOShakeScale(0.05f, strength: 1, vibrato: 1);
            return;
        }
        PlayerPrefs.SetInt("homeLevel", gameLevel);
        AudioManager.Instance.Play("click");
        //GAME LEVEL START FROM 0.
        int _sceneIndex = gameLevel % GlobalSaveDefinitions.totalLevelCount;
        SceneManager.LoadScene("Level " + (_sceneIndex + 1));
    }

    
    
}
