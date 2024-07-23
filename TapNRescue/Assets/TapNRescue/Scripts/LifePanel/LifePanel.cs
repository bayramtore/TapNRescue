using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using LionStudios.Suite.Core.LeanTween;

public class LifePanel : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform popUpPanel;
    [SerializeField] Image[] heartImages = new Image[5];
    //int totalLife;
    int maxLife = 5;
    
    [SerializeField] Button addLifeButton;
    [SerializeField] int lifePrice = 50;


    public void OpenLifePanel()
    {
        AudioManager.Instance.Play("click", vol: 0.5f, pitch: 1.2f);
        this.gameObject.SetActive(true);
        
        //popUpPanel.transform.DOScale(1, 0.15f).SetEase(Ease.InOutBounce);
        canvasGroup.DOFade(1, 0.15f).SetUpdate(true).OnComplete(() =>
        {
            GoldManager.Instance.transform.SetParent(this.transform);

            LifeManager.Instance.isLifePanelOn = true;
            LifeManager.Instance.addButton.interactable = false;

            LifeManager.Instance.TotalLife = PlayerPrefs.GetInt(GlobalSaveDefinitions.totalLifeDef, maxLife);
            for (int i = 0; i < LifeManager.Instance.TotalLife; i++)
            {
                heartImages[i].color = new Color(1, 1, 1, 1);
            }
            for (int i = LifeManager.Instance.TotalLife; i < maxLife; i++)
            {
                heartImages[i].color = new Color(0, 0, 0, 0.35f);
            }
            LeanTween.scale(popUpPanel.gameObject, Vector3.one, 0.25f).setEaseOutBack().setIgnoreTimeScale(true);
        });
    }


    public void AddLife()
    {
        if (LifeManager.Instance.TotalLife ==  maxLife)
        {
            CloseLifePanel();
            return;
        }
        if (GoldManager.Instance.TotalGold < lifePrice)
        {
            //AudioManager.Instance.Play("error");
            GoldManager.Instance.ShakeIcon();
            return;
        }

        GoldManager.Instance.TotalGold -= lifePrice;
        LifeManager.Instance.TotalLife += 1;

        AudioManager.Instance.Play("cash");

        CloseLifePanel();
    }


    public void CloseLifePanel()
    {
        AudioManager.Instance.Play("click", vol: 0.5f, pitch: 1.2f);
        LifeManager.Instance.isLifePanelOn = false;

        LeanTween.scale(popUpPanel.gameObject, Vector3.zero, 0.25f).setEaseInBack().setOnComplete(() =>
        {
            //homePanel.totalgoldTransform.SetParent(homePanel.transform);

            LifeManager.Instance.addButton.interactable = true;
            //LifeManager.Instance.transform.SetParent(HomePanel.Instance.headerTransform);
            GoldManager.Instance.transform.SetParent(HomePanel.Instance.headerTransform);
            canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
            {
                //bottomButtons.SetActive(true);
                this.gameObject.SetActive(false);
            });
        });
       
    }

    private void Start()
    {
        //homePanel = GetComponentInParent<HomePanel>();
        addLifeButton.onClick.AddListener(() =>
        {
            AddLife();
        });
    }
}