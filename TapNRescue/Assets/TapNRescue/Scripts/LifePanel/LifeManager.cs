using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class LifeManager : Singleton<LifeManager>
{
    [SerializeField] TMP_Text textLife;
    [SerializeField] TMP_Text textTimer;
    [SerializeField] TMP_Text lifePanelTimer;
    [SerializeField] int maxLife = 5;
    [SerializeField] int infinityTime = 1800;//sec. 30 min.
    [SerializeField] GameObject infinitySign;
    //[SerializeField] HomePanel homePanel;
    public Button addButton;
    public Transform icon;
    
    //public Button AddButton { get => addButton; set => addButton = value; }
    public int TotalLife { get => totalLife;
        set
        {
            totalLife = value;
            PlayerPrefs.SetInt(GlobalSaveDefinitions.totalLifeDef, totalLife);
            if(totalLife == maxLife)
            {
                UpdateTimer();
                UpdateLife();
                if(restoreRoutineCoroutine != null)
                {
                    StopCoroutine(restoreRoutineCoroutine);
                }
            }
        }
    }

    public bool isLifePanelOn = false;

    int totalLife = 0;
    DateTime nextLifeTime;
    DateTime lastAddedTime;
    [SerializeField]int restoreDurationInSec = 100;//10 sec for testing;
    bool isRestroring = false;

    // Start is called before the first frame update
    void Start()
    {
        Load();
        RestoreRoutine();
    }

    public void RestoreRoutine()
    {
        if (restoreRoutineCoroutine != null)
        {
            UpdateLife();
            StopCoroutine(restoreRoutineCoroutine);
        }
        restoreRoutineCoroutine = RestoreRoutineCoroutine();
        StartCoroutine(restoreRoutineCoroutine);
    }
    IEnumerator restoreRoutineCoroutine;
    IEnumerator RestoreRoutineCoroutine()
    {
        UpdateTimer();
        UpdateLife();
        isRestroring = true;
        while (TotalLife < maxLife)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextLifeTime;
            bool isAdding = false;

            while (currentTime > counter)
            {
                if (TotalLife < maxLife)
                {
                    isAdding = true;
                    TotalLife += 1;
                    UpdateLife();
                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = AddDurationInSec(timeToAdd, restoreDurationInSec);
                }
                else
                {
                    break;
                }
            }
            if (isAdding)
            {
                lastAddedTime = DateTime.Now;
                nextLifeTime = counter;
                Save();
            }
            UpdateTimer();
           
            yield return new WaitForSecondsRealtime(1f);
        }
        isRestroring = false;
    }

    DateTime AddDurationInSec(DateTime time, int durationSec)
    {
        return time.AddSeconds(durationSec);
    }

    DateTime AddDurationInMin(DateTime time, int durationMin)
    {
        return time.AddMinutes(durationMin);
    }

    void UpdateTimer()
    {
        if (TotalLife >= maxLife)
        {
            textTimer.text = "Full";
            return;
        }
        TimeSpan t = nextLifeTime - DateTime.Now;
        string timerValue = String.Format("{0:D2}:{1:D2}",(int)t.Minutes, (int)t.Seconds);
        textTimer.text = timerValue;
        if (isLifePanelOn)
        {
            lifePanelTimer.text = timerValue;
        }
    }

    void UpdateLife()
    {
        if (IsInfinityLife())
        {
            textLife.text = "";
            infinitySign.SetActive(true);
            addButton.gameObject.SetActive(false);
            return;
        }
        if (infinitySign.activeInHierarchy)
        {
            infinitySign.SetActive(false);
            addButton.gameObject.SetActive(true);
        }

        if (TotalLife < maxLife)
        {
            if (!addButton.gameObject.activeInHierarchy)
            {
                addButton.gameObject.SetActive(true);
            }
        }
        else
        {
            if (addButton.gameObject.activeInHierarchy)
            {
                addButton.gameObject.SetActive(false);
            }
        }
        textLife.text = TotalLife.ToString();
    }

    void Load()
    {
        TotalLife = PlayerPrefs.GetInt(GlobalSaveDefinitions.totalLifeDef, 5);
        Debug.Log("TotalLife = " + TotalLife);
        lastAddedTime = StringToDate( PlayerPrefs.GetString(GlobalSaveDefinitions.lastAddedTimeDef, DateTime.Now.ToString()));
        TimeSpan difference = DateTime.Now - lastAddedTime;
        int totalCount = (int) difference.TotalSeconds / restoreDurationInSec;
        if (totalCount > 0)
        {
            TotalLife += totalCount;
            if (TotalLife > maxLife)
                TotalLife = maxLife;


            int timeLeftInSec = (int)difference.TotalSeconds % restoreDurationInSec;
            TimeSpan t = TimeSpan.FromSeconds(timeLeftInSec);        
            lastAddedTime = AddDurationInSec(DateTime.Now, -timeLeftInSec);
        }
        nextLifeTime = AddDurationInSec(lastAddedTime, restoreDurationInSec);
        Save();
        UpdateLife();
    }

    void Save()
    {
        PlayerPrefs.SetInt(GlobalSaveDefinitions.totalLifeDef, TotalLife);
        PlayerPrefs.SetString(GlobalSaveDefinitions.lastAddedTimeDef, lastAddedTime.ToString());
    }

    //TESTING
    public void UseLife()
    {
        if (TotalLife == 0)
            return;

        TotalLife--;
        UpdateTimer();
        UpdateLife();
        if (!isRestroring)
        {
            //nextLifeTime = AddDurationInSec(DateTime.Now, restoreDurationInSec);
            if (TotalLife + 1 == maxLife)
            {
                nextLifeTime = AddDurationInSec(DateTime.Now, restoreDurationInSec);
                //nextLifeTime = AddDurationInMin(DateTime.Now, restoreDurationInMin);
            }
            
            RestoreRoutine();
        }
        Save();
    }

    public bool IsInfinityLife()
    {
        if (PlayerPrefs.GetInt(GlobalSaveDefinitions.infinityLifeDef, 0) == 1)
        {
            DateTime startTimeInfityLife = StringToDate(PlayerPrefs.GetString(GlobalSaveDefinitions.startInfinityLifeTimeDef, DateTime.Now.ToString()));
            TimeSpan difference = DateTime.Now - startTimeInfityLife;
            if (difference.TotalSeconds < infinityTime)
                return true;
            PlayerPrefs.SetInt(GlobalSaveDefinitions.infinityLifeDef, 0);
        }

        return false;
    }


    DateTime StringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
            return DateTime.Now;

        return DateTime.Parse(date);
    }
}
