using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [SerializeField] ObjectPooler objectPooler;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        objectPooler = ObjectPooler.Instance;
    }

    public Particles GetParticle(string tag)
    {
        return objectPooler.GetParticle(tag);
    }

    public void SpawnParticle(string tag, Vector3 pos, float locScale = 1)
    {
        var particle = objectPooler.GetParticle(tag);
        if (particle == null)
            return;
        particle.transform.position = pos;
        particle.transform.localScale = locScale * Vector3.one;
        particle.GetComponent<ParticleSystem>().Play();
        particle.InvokeBackToPool();
    }

    //public Block GetBlock(string tag)
    //{
    //    return objectPooler.GetBlock(tag);
    //}

    //public void ReleaseBlock(Block block)
    //{
    //    objectPooler.ReleaseBlock(block);
    //}

    //public CoinBlock GetCoinBlock(CoinTypes coinType)
    //{
    //    return objectPooler.GetCoinBlock(coinType);
    //}

    //public void ReleaseCoinBlock(CoinBlock coinBlock)
    //{
    //    objectPooler.ReleaseCoinBlock(coinBlock);
    //}


    public GameObject GetGameObject(string tag)
    {
        return objectPooler.GetGameObject(tag);
    }

    //public BillGoalObject GetBillObject(CoinTypes coinType)
    //{
    //    string tag = "RedBillGoal";
    //    switch (coinType)
    //    {
    //        case CoinTypes.Red:
    //            tag = "RedBillGoal";
    //            break;
    //        case CoinTypes.Green:
    //            tag = "GreenBillGoal";
    //            break;
    //        case CoinTypes.Blue:
    //            tag = "BlueBillGoal";
    //            break;
    //        case CoinTypes.Yellow:
    //            tag = "YellowBillGoal";
    //            break;
    //        case CoinTypes.Purple:
    //            tag = "PurpleBillGoal";
    //            break;
    //        case CoinTypes.Pink:
    //            tag = "PinkBillGoal";
    //            break;
    //        case CoinTypes.Cyan:
    //            tag = "CyanBillGoal";
    //            break;
    //        case CoinTypes.Magenta:
    //            tag = "MagentaBillGoal";
    //            break;
    //        case CoinTypes.Orange:
    //            tag = "OrangeBillGoal";
    //            break;
    //        case CoinTypes.Gray:
    //            tag = "GrayBillGoal";
    //            break;
    //    }

    //    if (objectPooler.GetGameObject(tag).TryGetComponent<BillGoalObject>(out var billGoalObject))
    //    {
    //        return billGoalObject;
    //    }
    //    return null;
    //}

    //public int GetActiveGameObjectsCount(string tag)
    //{
    //    return objectPooler.GetGameObjectsPool(tag).CountActive;
    //}

    public void ReleaseGameObject(GameObject gObj)
    {
        objectPooler.ReleaseGameObject(gObj);
    }

    public void PlaySound(string tag, Vector3 pos, float time, float vol = 1, float pitch = 1)
    {
        if (audioManager.isSoundOn)
        {
            var sound = objectPooler.GetSound(tag);
            sound.transform.position = pos;
            sound.PlaySound(time, vol, pitch);
        }
    }


}

