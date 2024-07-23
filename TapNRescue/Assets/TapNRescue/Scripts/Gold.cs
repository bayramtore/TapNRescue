using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using LionStudios.Suite.Core.LeanTween;

public class Gold : MonoBehaviour
{
    public int goldValue = 5;
    public float deltaX = 1;
    public float pitch = 1.0f;
    //public ParticleSystem partSystem;

    public void MoveGold(Transform target, float timeToWait, float timeToMove)
    {
        if (moveGoldCoroutine != null)
        {
            StopCoroutine(moveGoldCoroutine);
        }
        moveGoldCoroutine = MoveGoldCoroutine(target, timeToWait, timeToMove);
        StartCoroutine(moveGoldCoroutine);
    }

    IEnumerator moveGoldCoroutine;
    IEnumerator MoveGoldCoroutine(Transform target, float timeToWait, float timeToMove)
    {
        //partSystem.Stop();
        //yield return new WaitForSecondsRealtime(timeToWait);
        //List<Vector3> points = new();

        //var t = transform.position;
        //points.Add(t);
        //t += new Vector3(-deltaX / 2, -1.5f * Mathf.Sin(30 * Mathf.PI / 180), -1.5f *Mathf.Cos(30 * Mathf.PI / 180));
        //points.Add(t);
        //t += new Vector3(-deltaX / 2, 0, 0);
        //points.Add(t);
        //points.Add(target.position);


        //transform.DOScale(0.75f * Vector3.one, timeToMove);
        //LeanTween.move(this.gameObject, points.ToArray(), timeToMove).setOnComplete(() =>
        //{

        //    GoldManager.Instance.TotalGoal += goldValue;

        //    GoldManager.Instance.icon.transform.DOPunchScale(new Vector3(-1.01f, 1.01f, 1.01f), 0.1f);
        //    ObjectSpawner.Instance.PlaySound("cash", transform.position, 0.5f, pitch: pitch);
        //    ObjectSpawner.Instance.ReleaseGameObject(this.gameObject);
        //});

        LeanTween.move(gameObject, target.position, timeToMove).setEaseInBack().setOnComplete(() =>
        {
            GoldManager.Instance.TotalGold += goldValue;

            GoldManager.Instance.icon.transform.DOPunchScale(new Vector3(1.01f, 1.01f, 1.01f), 0.1f);
            ObjectSpawner.Instance.PlaySound("cash", transform.position, 0.5f, pitch: pitch);
            ObjectSpawner.Instance.ReleaseGameObject(this.gameObject);
        });
        return null;
    }

    private void Start()
    {
        //transform.eulerAngles = new Vector3(0, 0, 0);
        transform.localScale = 1.0f * Vector3.one;
        //partSystem.Stop();
    }
}
