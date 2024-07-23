
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<GameObject> particlePrefabs = new();
    Dictionary<string, ObjectPool<Particles>> _poolDict;

    public List<GameObject> soundPrefabs = new();
    Dictionary<string, ObjectPool<SoundManager>> _poolSoundManagerDict;

    public List<GameObject> gObjPrefabs = new();
    Dictionary<string, ObjectPool<GameObject>> _poolGameObjectDict;

    //public List<Block> blockPrefabs = new();
    //Dictionary<string, ObjectPool<Block>> _poolBlockDict;

    //public List<CoinBlock> coinBlockPrefabs = new();
    //Dictionary<CoinTypes, ObjectPool<CoinBlock>> _poolCoinBlockDict;

    #region Main Methods
    protected override void OnAwake()
    {
        #region PARTICLES
        _poolDict = new Dictionary<string, ObjectPool<Particles>>();
        foreach (var particlePrefab in particlePrefabs)
        {
            ObjectPool<Particles> _pool = new(() =>
            {
                var particle = Instantiate(particlePrefab).GetComponent<Particles>();
                return particle;
            }, particle =>
            {
                particle.gameObject.SetActive(true);
            }, particle =>
            {
                particle.gameObject.SetActive(false);
            }, particle =>
            {
                Destroy(particle.gameObject);
            });
            _poolDict.Add(particlePrefab.GetComponent<Particles>().particleTag, _pool);
        }
        #endregion


        #region GAMEOBJECT
        _poolGameObjectDict = new Dictionary<string, ObjectPool<GameObject>>();
        foreach (var gObjectPrefab in gObjPrefabs)
        {
            ObjectPool<GameObject> _pool = new (() =>
            {
                var gObj = Instantiate(gObjectPrefab);//.GetComponent<BTFloatingText>();
                return gObj;
            }, gObj =>
            {
                gObj.SetActive(true);
            }, gObj =>
            {
                gObj.SetActive(false);
            }, gObj =>
            {
                Destroy(gObj);
            });

            _poolGameObjectDict.Add(gObjectPrefab.tag, _pool);
        }
        #endregion


        #region SOUND PREFABS
        _poolSoundManagerDict = new Dictionary<string, ObjectPool<SoundManager>>();
        foreach (var soundPrefab in soundPrefabs)
        {
            ObjectPool<SoundManager> _pool = new ObjectPool<SoundManager>(() =>
            {
                var snd = Instantiate(soundPrefab).GetComponent<SoundManager>();
                return snd;
            }, snd =>
            {
                snd.gameObject.SetActive(true);
            }, snd =>
            {
                snd.gameObject.SetActive(false);
            }, snd =>
            {
                Destroy(snd.gameObject);
            });

            _poolSoundManagerDict.Add(soundPrefab.GetComponent<SoundManager>().soundTag, _pool);
        }
        #endregion

        //#region BLOCK PREFABS
        //_poolBlockDict = new Dictionary<string, ObjectPool<Block>>();
        //foreach (var blockPrefab in blockPrefabs)
        //{
        //    ObjectPool<Block> _pool = new ObjectPool<Block>(() =>
        //    {
        //        var blk = Instantiate(blockPrefab).GetComponent<Block>();
        //        return blk;
        //    }, blk =>
        //    {
        //        blk.gameObject.SetActive(true);
        //    }, blk =>
        //    {
        //        blk.gameObject.SetActive(false);
        //    }, blk =>
        //    {
        //        Destroy(blk.gameObject);
        //    });

        //    _poolBlockDict.Add(blockPrefab.GetComponent<Block>().BlockTag, _pool);
        //}
        //#endregion

        //#region COIN PREFABS
        //_poolCoinBlockDict = new Dictionary<CoinTypes, ObjectPool<CoinBlock>>();
        //foreach (var coinBlockPrefab in coinBlockPrefabs)
        //{
        //    ObjectPool<CoinBlock> _pool = new ObjectPool<CoinBlock>(() =>
        //    {
        //        var coin = Instantiate(coinBlockPrefab).GetComponent<CoinBlock>();
        //        return coin;
        //    }, coin =>
        //    {
        //        coin.gameObject.SetActive(true);
        //    }, coin =>
        //    {
        //        coin.gameObject.SetActive(false);
        //    }, coin =>
        //    {
        //        Destroy(coin.gameObject);
        //    });

        //    _poolCoinBlockDict.Add(coinBlockPrefab.GetComponent<CoinBlock>().CoinType, _pool);
        //}
        //#endregion

    }

    #endregion

    #region GET PARTICLE
    public Particles GetParticle(string particleTag)
    {
        if (!_poolDict.ContainsKey(particleTag))
        {
            Debug.LogWarning("No _pool with tag = " + particleTag + " to GET particle");
            return null;
        }

        return _poolDict[particleTag].Get();
    }

    public void ReleaseParticle(Particles particle)
    {
        if (!_poolDict.ContainsKey(particle.particleTag))
        {
            Debug.LogWarning("No _pool with tag = " + particle.particleTag + " to RETURN particle");
            Destroy(particle.gameObject);
            return;
        }

        _poolDict[particle.particleTag].Release(particle);
    }

    public void DestroyPool(string particleTag)
    {
        if (!_poolDict.ContainsKey(particleTag))
        {
            Debug.LogWarning("No _pool with tag = " + particleTag + " to RETURN enemy");
            return;
        }

        //RemoveElementFromParticlePoolList(particleTag);
        _poolDict[particleTag].Clear();
    }

    #endregion

   

    

    #region GET GAME OBJECT
    public GameObject GetGameObject(string gObjTag)
    {
        if (!_poolGameObjectDict.ContainsKey(gObjTag))
        {
            Debug.LogWarning("No _pool with tag = " + gObjTag + " to GET GAME OBJECT Text");
            return null;
        }

        return _poolGameObjectDict[gObjTag].Get();
    }

    public void ReleaseGameObject(GameObject gObj)
    {
        if (!_poolGameObjectDict.ContainsKey(gObj.tag))
        {
            Debug.LogWarning("No _pool with tag = " + gObj.tag + " to RETURN GAME OBJECT");
            Destroy(gObj);
            return;
        }

        _poolGameObjectDict[gObj.tag].Release(gObj);
    }

    public void DestroyGameObjecttPool(string gObjTag)
    {
        if (!_poolGameObjectDict.ContainsKey(gObjTag))
        {
            Debug.LogWarning("No _pool with tag = " + gObjTag + " to RETURN GAME OBJ");
            return;
        }

        //RemoveElementFromParticlePoolList(particleTag);
        _poolGameObjectDict[gObjTag].Clear();
    }

    public ObjectPool<GameObject> GetGameObjectsPool(string tag)
    {
        return _poolGameObjectDict[tag];
    }

    #endregion


   

    #region GET SOUND PERFABS
    public SoundManager GetSound(string soundTag)
    {
        if (!_poolSoundManagerDict.ContainsKey(soundTag))
        {
            Debug.LogWarning("No _pool with tag = " + _poolSoundManagerDict + " to GET Sound Prefab");
            return null;
        }

        return _poolSoundManagerDict[soundTag].Get();
    }

    public void ReleaseSound(SoundManager soundManager)
    {
        if (!_poolSoundManagerDict.ContainsKey(soundManager.soundTag))
        {
            Debug.LogWarning("No _pool with tag = " + soundManager.soundTag + " to RETURN sound");
            Destroy(soundManager.gameObject);
            return;
        }

        _poolSoundManagerDict[soundManager.soundTag].Release(soundManager);
    }

    public void DestroySoundPool(string soundTag)
    {
        if (!_poolSoundManagerDict.ContainsKey(soundTag))
        {
            Debug.LogWarning("No _pool with tag = " + soundTag + " to RETURN sound");
            return;
        }

        //RemoveElementFromParticlePoolList(particleTag);
        _poolSoundManagerDict[soundTag].Clear();
    }

    #endregion


    //#region GET BLOCK PERFABS
    //public Block GetBlock(string blockTag)
    //{
    //    if (!_poolBlockDict.ContainsKey(blockTag))
    //    {
    //        Debug.LogWarning("No _pool with tag = " + _poolBlockDict + " to GET Block Prefab");
    //        return null;
    //    }

    //    return _poolBlockDict[blockTag].Get();
    //}

    //public void ReleaseBlock(Block block)
    //{
    //    if (!_poolBlockDict.ContainsKey(block.BlockTag))
    //    {
    //        Debug.LogWarning("No _pool with tag = " + block.BlockTag + " to RETURN block");
    //        Destroy(block.gameObject);
    //        return;
    //    }

    //    _poolBlockDict[block.BlockTag].Release(block);
    //}

    //public void DestroyBlockPool(string blockTag)
    //{
    //    if (!_poolBlockDict.ContainsKey(blockTag))
    //    {
    //        Debug.LogWarning("No _pool with tag = " + blockTag + " to RETURN block");
    //        return;
    //    }

    //    //RemoveElementFromParticlePoolList(particleTag);
    //    _poolBlockDict[blockTag].Clear();
    //}

    //#endregion

    //#region GET COIN BLOCK PERFABS
    //public CoinBlock GetCoinBlock(CoinTypes coinType)
    //{
    //    if (!_poolCoinBlockDict.ContainsKey(coinType))
    //    {
    //        Debug.LogWarning("No _pool with CoinType = " + _poolCoinBlockDict + " to GET Block Prefab");
    //        return null;
    //    }

    //    return _poolCoinBlockDict[coinType].Get();
    //}

    //public void ReleaseCoinBlock(CoinBlock coinBlock)
    //{
    //    if (!_poolCoinBlockDict.ContainsKey(coinBlock.CoinType))
    //    {
    //        Debug.LogWarning("No _pool with CoinType = " + coinBlock.CoinType + " to RETURN block");
    //        Destroy(coinBlock.gameObject);
    //        return;
    //    }

    //    _poolCoinBlockDict[coinBlock.CoinType].Release(coinBlock);
    //}

    //public void DestroyCoinBlockPool(CoinTypes coinType)
    //{
    //    if (!_poolCoinBlockDict.ContainsKey(coinType))
    //    {
    //        Debug.LogWarning("No _pool with CoinType = " + coinType + " to RETURN block");
    //        return;
    //    }

    //    //RemoveElementFromParticlePoolList(particleTag);
    //    _poolCoinBlockDict[coinType].Clear();
    //}

    //#endregion


}
