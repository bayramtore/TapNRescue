using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager>
{
    #region VARIABLES
    public List<Animal> animals;
    public GameObject confettiCamera;

    [SerializeField] EventSystem eventSystem;
    [SerializeField] EventSystem[] eventSystems;

    public GameObject ballsParent;
    #endregion


    #region MAIN METHODS
    private void Start()
    {
        eventSystems = FindObjectsOfType(typeof(EventSystem)) as EventSystem[];
        if (eventSystems.Length > 1)
        {
            Debug.LogWarning("there are " + eventSystems.Length + " EventSystem on Scene. Deleting one");
            eventSystem.enabled = false;
        }
    }

    #endregion MainMethods

    #region CUSTOM METHODS
    public void CheckWin()
    {
        if (animals.Count > 0)
            return;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            AudioManager.Instance.Play("gameWin");
            confettiCamera.SetActive(true);
            DOVirtual.DelayedCall(1.0f, () =>
            {
                UIManager.Instance.OpenWinPanel();
            });
        });
        
    }

    //public void UpdateLevelText()
    //{

    //}


    #endregion CustomMethods
}
