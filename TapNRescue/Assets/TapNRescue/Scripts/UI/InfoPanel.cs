using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanel : Singleton<InfoPanel>
{

    #region Variables
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject popUpPanel;
    [SerializeField] Button closeButton;
    [SerializeField] List<GameObject> gameInfoList;
    [SerializeField] List<GameObject> titleTextList;
    [SerializeField] List<GameObject> infoTextList;
    #endregion


    private void Start()
    {
        closeButton.onClick.AddListener(() =>
        {
            CloseInfoPanel();
        });
    }

    public void OpenInfoPanel(int gameObjIndex = 0)
    {        
        InputManager.Instance.isDisplayOn = true;
        AudioManager.Instance.Play("click", vol: 0.5f, pitch: 1.2f);
        this.gameObject.SetActive(true);

        canvasGroup.DOFade(1, 0.15f).OnComplete(() =>
        {
            gameInfoList[gameObjIndex].SetActive(true);
            titleTextList[gameObjIndex].SetActive(true);
            infoTextList[gameObjIndex].SetActive(true);
            AudioManager.Instance.Play("popup", vol: 0.5f, pitch: 1.2f);
            popUpPanel.transform.DOScale(1, 0.1f);
        });
    }


    public void CloseInfoPanel()
    {
        AudioManager.Instance.Play("click", vol: 0.5f, pitch: 1.2f);

        popUpPanel.transform.DOScale(0, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
            {
                InputManager.Instance.isDisplayOn = false;
                this.gameObject.SetActive(false);
            });
        });
    }

    
}
