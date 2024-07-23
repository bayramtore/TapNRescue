using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultScripts
{
    public class BTSwitchToggle : MonoBehaviour
    {
        [SerializeField] RectTransform uiHandleRectTransform;
        [SerializeField] string toggleId;
        [SerializeField] Toggle toggle;
        Vector2 handlePosition;
        //[SerializeField] Color backgroundPassiveColor;
        //[SerializeField] Color handleActiveColor;

        [SerializeField] Image backgroundImage;//, handleImage;
        [SerializeField] Image handleImage;

        //[SerializeField] Image activeBackgroundImage;
        //[SerializeField] Image passiveBackgroundImage;

        //[SerializeField] Image activeHandleImage;
        //[SerializeField] Image passiveHandleImage;

        [SerializeField] Sprite activeHandleSprite;
        [SerializeField] Sprite passiveHandleSprite;
        [SerializeField] Sprite activeBackgroundSprite;
        [SerializeField] Sprite passiveBackgroundSprite;


        Color backgroundDefaultColor;//, handleDefaultColor;
        AudioManager audioManager;

        private void Start()
        {
            //toggle.isOn = ES3.Load(toggleId, defaultValue: true);
            handlePosition = uiHandleRectTransform.anchoredPosition;
            //backgroundDefaultColor = backgroundImage.color;
            toggle.onValueChanged.AddListener(OnSwitch);
            audioManager = AudioManager.Instance;
            toggle.isOn = true ? PlayerPrefs.GetInt(toggleId, 1) == 1 : false;
            //toggle.isOn = ES3.Load(toggleId, GlobalSaveDefinitions.defaultSaveFile, defaultValue: true);
            OnStartSwitch(toggle.isOn);


        }

        void OnStartSwitch(bool on)
        {
            //uiHandleRectTransform.DOAnchorPos(on ? handlePosition : -1 * handlePosition, .4f).SetEase(Ease.InOutBack);
            uiHandleRectTransform.DOAnchorPos(on ? new(handlePosition.x, handlePosition.y) : new(-handlePosition.x, handlePosition.y), .4f).SetEase(Ease.InOutBack);
            backgroundImage.sprite = (on ? activeBackgroundSprite : passiveBackgroundSprite);
            handleImage.sprite = (on ? activeHandleSprite : passiveHandleSprite);

            //backgroundImage.DOColor(on ? backgroundDefaultColor : backgroundPassiveColor, .6f);
            //handleBackgroundImage.DOColor(on ? handleActiveColor : backgroundPassiveColor, .6f);
            //if (toggleId == "sound")
            //{
            //    audioManager.isSoundOn = on;
            //}
            //else if (toggleId == "haptic")
            //{
            //    audioManager.isHapticOn = on;
            //}

            switch (toggleId)
            {
                case "sound":
                    audioManager.isSoundOn = on;
                    break;

                case "haptic":
                    audioManager.isHapticOn = on;
                    break;

                case "music":
                    audioManager.isBackgroundOn = on;
                    if (on)
                    {
                        audioManager.PlayBackgroundMusic("background_music");
                    }
                    else
                    {
                        audioManager.StopBackgroundMusic("background_music");
                    }
                    break;
            }

        }

        void OnSwitch(bool on)
        {
            uiHandleRectTransform.DOAnchorPos(on ? new(handlePosition.x, handlePosition.y) : new(-handlePosition.x, handlePosition.y), .4f).SetEase(Ease.InOutBack);
            backgroundImage.sprite = (on ? activeBackgroundSprite : passiveBackgroundSprite);
            handleImage.sprite = (on ? activeHandleSprite : passiveHandleSprite);
            //backgroundImage.DOColor(on ? backgroundDefaultColor : backgroundPassiveColor, .6f);
            //handleBackgroundImage.DOColor(on ? handleActiveColor : backgroundPassiveColor, .6f);
            if (toggleId == "sound")
            {
                audioManager.isSoundOn = on;
                audioManager.Play("click");
            }
            else if (toggleId == "haptic")
            {
                audioManager.isHapticOn = on;
                audioManager.Play("click");
            }
            switch (toggleId)
            {
                case "sound":
                    audioManager.isSoundOn = on;
                    break;

                case "haptic":
                    audioManager.isHapticOn = on;
                    break;

                case "music":
                    audioManager.isBackgroundOn = on;
                    if (on)
                    {
                        audioManager.PlayBackgroundMusic("background_music");
                    }
                    else
                    {
                        audioManager.StopBackgroundMusic("background_music");
                    }
                    break;
            }

            PlayerPrefs.SetInt(toggleId, (on) ? 1 : 0);

        }

        private void OnDestroy()
        {
            toggle.onValueChanged.RemoveListener(OnSwitch);
        }
    }
}