using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public string soundTag;
    //[Range(0.01f, 10f)]
    //public float pitchRandomMultiplier = 1f;
    [SerializeField] AudioSource m_Source;
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        //m_Source = this.GetComponent<AudioSource>();

        //Multiply pitch
        //if (pitchRandomMultiplier != 1)
        //{
        //    if (Random.value < .5)
        //        m_Source.pitch *= Random.Range(1 / pitchRandomMultiplier, 1);
        //    else
        //        m_Source.pitch *= Random.Range(1, pitchRandomMultiplier);
        //}
    }


    public void PlaySound(float time, float vol = 1f, float pitch = 1f)
    {
        if (m_Source == null)
        {
            Debug.LogWarning("NO AUDIO SOURCE COMPONENT");
            return;
        }

        m_Source.volume = vol;
        m_Source.pitch = pitch;
        m_Source.Play();
        if (time != 0f)
        {
            StartCoroutine(BackToPoolCoroutine(time));
        }

    }



    public void UpdatePitch(float pitch)
    {
        m_Source.pitch = pitch;
    }

    IEnumerator BackToPoolCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        //PutBackToPool();
        objectPooler.ReleaseSound(this);
    }

}

