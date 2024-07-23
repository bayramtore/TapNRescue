using System.Collections;
using UnityEngine;


public class Particles : MonoBehaviour
{
    ObjectPooler objectPooler;
    public string particleTag;
    public float timeBackToPool = 1.0f;



    public void InvokeBackToPool()
    {
        if (timeBackToPool != 0)
        {
            StartCoroutine(BackToPoolCoroutine(timeBackToPool));
        }
    }

    public void PutBackToPool()
    {
        if (objectPooler == null)
        {
            objectPooler = ObjectPooler.Instance;
        }
        if (GetComponent<ParticleSystem>().IsAlive(true))
        {
            GetComponent<ParticleSystem>().Stop(withChildren: true);
        }
        objectPooler.ReleaseParticle(this);
    }

    IEnumerator BackToPoolCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        PutBackToPool();
    }

    public void PlayParticles()
    {
        GetComponent<ParticleSystem>().Play();
        InvokeBackToPool();
    }
}

