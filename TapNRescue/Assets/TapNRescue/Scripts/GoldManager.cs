using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GoldManager : Singleton<GoldManager>
{
    int totalGold = 0;
    public GameObject icon;
    public TMP_Text goldText;
    public Button addButton;



    protected override void OnAwake()
    {
        TotalGold = PlayerPrefs.GetInt(GlobalSaveDefinitions.totalGoldDef, 50);
    }

    public int TotalGold
    {
        get => totalGold;
        set
        {
            totalGold = value;
            PlayerPrefs.SetInt(GlobalSaveDefinitions.totalGoldDef, totalGold);
            goldText.text = "" + totalGold;
        }
    }

    public void ShakeIcon()
    {
        AudioManager.Instance.Play("error");
        //icon.transform.DOShakeScale(0.1f, 0.3f, 1);
        icon.transform.DOPunchScale(new Vector3(.2f, .2f, .2f), 0.1f, vibrato:3);
    }

    IEnumerator changeValueCoroutine;
    IEnumerator ChangeValueCoroutine(int collectedGold, float duration)
    {
        //AudioManager.Instance.Play("cash_collect");
        int g_start = totalGold;
        int gold = collectedGold;
        int g_end = TotalGold + collectedGold;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            totalGold = (int)Mathf.Lerp(g_start, g_end, elapsed / duration);
            goldText.text = "" + totalGold;
            elapsed += Time.deltaTime;
            yield return null;
        }
        totalGold = g_end;
        goldText.text = "" + totalGold;
        PlayerPrefs.SetInt(GlobalSaveDefinitions.totalGoldDef, TotalGold);
    }

    public void ChangeValue(int collectedGold, float duration)
    {
        if (changeValueCoroutine != null)
        {
            StopCoroutine(changeValueCoroutine);
        }
        changeValueCoroutine = ChangeValueCoroutine(collectedGold, duration);
        StartCoroutine(changeValueCoroutine);
    }

    
}
