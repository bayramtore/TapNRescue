using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeLevel : MonoBehaviour
{
    //[SerializeField] Image lineImage;
    //[SerializeField] GameObject giftIcon;
    //[SerializeField] GameObject cardIcon;

    public GameObject activeBG;
    public GameObject passiveBG;
    public GameObject completedBG;
    public GameObject tick;
    public GameObject glowParticles;
    public TMP_Text levelText;
    public GameObject giftBG;

    public void UpdateGift(int level)
    {
        //if (level == HomePanel.Instance.boosterScriptable.hammerUnlockLevel)
        //{
        //    giftBG.SetActive(true);
        //    giftBG.transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else if (level == HomePanel.Instance.boosterScriptable.swapUnlockLevel)
        //{
        //    giftBG.SetActive(true);
        //    giftBG.transform.GetChild(1).gameObject.SetActive(true);
        //}
        //else if (level == HomePanel.Instance.boosterScriptable.magnetUnlockLevel)
        //{
        //    giftBG.SetActive(true);
        //    giftBG.transform.GetChild(2).gameObject.SetActive(true);
        //}
        //if (level % 5 == 0)
        //{
        //    giftBG.SetActive(true);//GIFT
        //    giftBG.transform.GetChild(3).gameObject.SetActive(true);
        //}
        //else
        //{
        //    giftBG.SetActive(false);
        //}
        giftBG.SetActive(false);
    }

    public void UpdateHomeLevel(int index, int level)
    {
        UpdateGift(level);
        levelText.text = "" + level;
        if (index == 0)
        {
            activeBG.SetActive(false);
            passiveBG.SetActive(false);
            completedBG.SetActive(true);
            glowParticles.SetActive(false);
            tick.SetActive(false);
            
        }
        else if(index == 1)
        {
            activeBG.SetActive(true);
            passiveBG.SetActive(false);
            completedBG.SetActive(false);
            glowParticles.SetActive(false);
            tick.SetActive(false);
        }
        else
        {
            activeBG.SetActive(false);
            passiveBG.SetActive(true);
            completedBG.SetActive(false);
            glowParticles.SetActive(false);
            tick.SetActive(false);
        }
    }


    ////public Image LineImage { get => lineImage; set => lineImage = value; }

    //public void ShowGift(bool show)
    //{
    //    giftIcon.SetActive(show);
    //}

    //public void ShowCard(bool show)
    //{
    //    cardIcon.SetActive(show);
    //}


}
