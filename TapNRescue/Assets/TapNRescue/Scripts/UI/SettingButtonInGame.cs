using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonInGame : MonoBehaviour
{
    [SerializeField] Image cancelImage;
    [SerializeField] string toggleId;
    [SerializeField] Toggle toggle;


    private void Start()
    {
        toggle.onValueChanged.AddListener(OnSwitch);
        toggle.isOn = PlayerPrefs.GetInt(toggleId, 1) == 1 ? true : false;
        cancelImage.gameObject.SetActive(!toggle.isOn);
        //AudioManager.Instance.SetAudioBoolen(toggleId, toggle.isOn);
    }

    void OnStartSwitch(bool on)
    {
        AudioManager.Instance.SetAudioBoolen(toggleId, on);
    }

    void OnSwitch(bool on)
    {
        cancelImage.gameObject.SetActive(!on);
        AudioManager.Instance.SetAudioBoolen(toggleId, on);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

}
