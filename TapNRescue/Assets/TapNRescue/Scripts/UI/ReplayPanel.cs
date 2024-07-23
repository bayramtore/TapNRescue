using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ReplayPanel : MonoBehaviour
{
    //[SerializeField] Button closeButton;
    //[SerializeField] Button replayButton;
    //[SerializeField] Button homeButton;
    ////[SerializeField] Button replayButton;
    //AudioManager audioManager;
    ////[SerializeField] GameController gameController;
    ////[SerializeField] bool isEndlessMode = false;

    //[SerializeField] CanvasGroup canvasGroup;
    //[SerializeField] GameObject popUpPanel;
    //[SerializeField] TMP_Text[] titles = new TMP_Text[2];

    //[SerializeField] GameObject[] infoPanel = new GameObject[2];
    //[SerializeField] int infinitySeconds = 900;//15 min or 900 sec.

    //// Start is called before the first frame update
    //void Start()
    //{
    //    replayButton.onClick.AddListener(() =>
    //    {
    //        ReplayLevel();
    //    });

    //    homeButton.onClick.AddListener(() =>
    //    {
    //        Home();
    //    });


    //    closeButton.onClick.AddListener(() =>
    //    {
    //        CloseReplayPanel();
    //    });
    //}

    //public void ReplayLevel()
    //{
    //    audioManager.Play("click", vol: 0.5f, pitch: 1.2f);

    //    popUpPanel.transform.DOScale(0, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
    //    {
    //        canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
    //        {
                
    //            if (IsInfinityLife())
    //            {
    //                var scene = SceneManager.GetActiveScene();
    //                SceneManager.LoadScene(scene.name);
    //            }
    //            else
    //            {
    //                int _totalLife = ES3.Load(GlobalSaveDefinitions.totalLifeDef, GlobalSaveDefinitions.defaultSaveFile, 5);
    //                if (_totalLife == maxLife)
    //                {
    //                    ES3.Save(GlobalSaveDefinitions.lastAddedTimeDef, DateTime.Now.ToString(), GlobalSaveDefinitions.defaultSaveFile);
    //                }
    //                _totalLife--;
    //                if (_totalLife < 0)
    //                {
    //                    _totalLife = 0;
    //                }
    //                ES3.Save(GlobalSaveDefinitions.totalLifeDef, _totalLife, GlobalSaveDefinitions.defaultSaveFile);
    //                if (_totalLife > 0)
    //                {
    //                    var scene = SceneManager.GetActiveScene();
    //                    SceneManager.LoadScene(scene.name);
    //                }
    //                else
    //                {
    //                    SceneManager.LoadScene(0);//HOME
    //                }
    //            }
                
                
    //        });
    //    });
       
    //}

    //public void Home()
    //{
    //    audioManager.Play("click", vol: 0.5f, pitch: 1.2f);

    //    popUpPanel.transform.DOScale(0, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
    //    {
    //        canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
    //        {
    //            SceneManager.LoadScene(0);
    //        });
    //    });
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

 
    //int maxLife = 5;

    //public void OpenReplayPanel(int index = 0)
    //{
    //    if (audioManager == null)
    //    {
    //        audioManager = AudioManager.Instance;
    //    }

    //    InputManager.Instance.IsDisplayOn = true;

    //    audioManager.Play("click", vol: 0.5f, pitch: 1.2f);
    //    this.gameObject.SetActive(true);
    //    switch (index)
    //    {
    //        case 0://REPLAY INFO
    //            homeButton.gameObject.SetActive(false);
    //            replayButton.gameObject.SetActive(true);
    //            titles[0].gameObject.SetActive(true);
    //            titles[1].gameObject.SetActive(false);
    //            infoPanel[0].SetActive(true);
    //            infoPanel[1].SetActive(false);
    //            break;

    //        case 1://HOME INFO
    //            homeButton.gameObject.SetActive(true);
    //            replayButton.gameObject.SetActive(false);
    //            titles[0].gameObject.SetActive(false);
    //            titles[1].gameObject.SetActive(true);
    //            infoPanel[0].SetActive(false);
    //            infoPanel[1].SetActive(true);
    //            break;
    //    }

    //    canvasGroup.DOFade(1, 0.15f).OnComplete(() =>
    //    {
    //        audioManager.Play("popup", vol: 0.5f, pitch: 1.2f);
    //        popUpPanel.transform.DOScale(1, 0.15f).SetEase(Ease.InOutBounce);
    //    });
    //}

    //public void CloseReplayPanel()
    //{
    //    audioManager.Play("click", vol: 0.5f, pitch: 1.2f);

    //    popUpPanel.transform.DOScale(0, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
    //    {

    //        canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
    //        {
    //            InputManager.Instance.IsDisplayOn = false;
    //            this.gameObject.SetActive(false);
    //        });
    //    });
    //}

}
