using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Button closeButton;
    [SerializeField] Button privacyButton;
    //[SerializeField] Button termsButton;
    //[SerializeField] Button replayButton;
    //AudioManager audioManager;
    //[SerializeField] GameController gameController;
    //[SerializeField] bool isEndlessMode = false;
   
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameObject popUpPanel;
    //[SerializeField] bool isGameScene = false;


    // Start is called before the first frame update
    void Start()
    {

        privacyButton.onClick.AddListener(() =>
        {
            PrivacyButtonPressed();
        });

        //termsButton.onClick.AddListener(() =>
        //{
        //    TermsButtonPressed();
        //});

        closeButton.onClick.AddListener(() =>
        {
            CloseSettingsPanel();
        });
    }

   
    public void OpenSettingsPanel()
    {
        //if (isGameScene)
        //{
        //    InputManager.Instance.IsDisplayOn = true;
        //}


        //InputManager.Instance.DisableClicking();

        AudioManager.Instance.Play("click");
        this.gameObject.SetActive(true);

        canvasGroup.DOFade(1, 0.15f).OnComplete(() =>
        {
            AudioManager.Instance.Play("popup");
            //popUpPanel.transform.DOScale(1, 0.15f).SetEase(Ease.InOutBounce);
            LeanTween.scale(popUpPanel, Vector3.one, 0.25f).setEaseOutBack();
        });
    }


    public void CloseSettingsPanel()
    {
        AudioManager.Instance.Play("click");

        LeanTween.scale(popUpPanel, Vector3.zero, 0.25f).setEaseInBack().setOnComplete(() =>
        {
            canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
            {
                //if (isGameScene)
                //{
                //    InputManager.Instance.IsDisplayOn = false;
                //}

                this.gameObject.SetActive(false);
            });
        });

        //popUpPanel.transform.DOScale(0, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
        //{      
        //    canvasGroup.DOFade(0, 0.15f).OnComplete(() =>
        //    {
        //        //if (isGameScene)
        //        //{
        //        //    InputManager.Instance.IsDisplayOn = false;
        //        //}
                
        //        this.gameObject.SetActive(false);
        //    });
        //});
    }
  
    //public void TermsButtonPressed()
    //{
    //    AudioManager.Instance.Play("click");
    //    Application.OpenURL("https://lionstudios.cc/terms/");
    //}

    public void PrivacyButtonPressed()
    {
        AudioManager.Instance.Play("click");
        Application.OpenURL("https://appfore.com/privacy-policy/");
    }
}
