using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using DG.Tweening;


public class SpawnManager : Singleton<SpawnManager>
{
    

    #region MAIN METHODS
    
    #endregion MainMethods

    #region CUSTOM METHODS
    
    #endregion CustomMethods
}




//protected override void OnAwake()
//{
//    levelData = GameManager.Instance.LevelData;
//}

//private void OnDrawGizmos()
//{
//    //Gizmos.color = Color.red;
//    //Gizmos.DrawWireSphere(transform.position, spawnRadius);

//    //Gizmos.color = Color.red;
//    //Gizmos.DrawWireSphere(returnTransform.position, returnRadius);

//    //Gizmos.color = Color.green;
//    //Gizmos.DrawWireSphere(blockSpawnerTransform.position, blockSpawnerRadius);
//}

//#endregion

//#region COIN
////[SerializeField] string[] coinNames = new string[10];
//[SerializeField] List<CoinBlock> spawnedCoinBlocks = new();
//public List<CoinBlock> SpawnedCoinBlocks { get => spawnedCoinBlocks; set => spawnedCoinBlocks = value; }
//int totalNumOfCoins;
//[SerializeField] bool showCoinCounter = false;
//[SerializeField] TMP_Text coinCounterText;



//IEnumerator spawnCoinsCoroutine;
//IEnumerator SpawnCoinsCoroutine()
//{
//    int coinCount = totalNumOfCoins - spawnedCoinBlocks.Count;
//    for (int i = 0; i < coinCount; i++)
//    {
//        AddCoin();
//        yield return new WaitForSeconds(spawnTimeInterval);
//    }
//}

//public void SpawnCoins()
//{
//    if (!GameManager.Instance.LevelData.spawnCoins)
//    {
//        return;
//    }
//    if (spawnCoinsCoroutine != null)
//    {
//        StopCoroutine(spawnCoinsCoroutine);
//    }
//    spawnCoinsCoroutine = SpawnCoinsCoroutine();
//    StartCoroutine(spawnCoinsCoroutine);
//}

////List<CoinBlock> selectedCoinsList = new();
////public void SelectedCoins()
////{
////    foreach (var cn in gameManager.coinTypeColors)
////    {

////    }
////}

//private void AddCoin()
//{
//    //CoinTypes randomCoin = (CoinTypes)Random.Range(0, System.Enum.GetValues(typeof(CoinTypes)).Length);

//    //while (!gameManager.coinTypeColors.Contains((int)randomCoin))
//    //{
//    //    randomCoin = (CoinTypes)Random.Range(0, System.Enum.GetValues(typeof(CoinTypes)).Length);
//    //}

//    //while (!gameManager.coinTypeList.Contains(randomCoin))
//    //{
//    //    randomCoin = (CoinTypes)Random.Range(0, System.Enum.GetValues(typeof(CoinTypes)).Length);
//    //}

//    //string blockTag = coinNames[(int)GetRandomCoinTypeToSpawn()];
//    //var coinBlock = objectSpawner.GetBlock(blockTag) as CoinBlock;

//    CoinTypes _randomCoinType;
//    if (Random.value < randomProbaility)
//    {
//        _randomCoinType = gameManager.LevelData.coinTypeList[Random.Range(0, gameManager.LevelData.coinTypeList.Count)];
//    }
//    else
//    {
//        _randomCoinType = GetRandomCoinTypeToSpawn();
//    }

//    var coinBlock = objectSpawner.GetCoinBlock(_randomCoinType);
//    coinBlock.transform.position = GetRandomPos();
//    coinBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//    spawnedCoinBlocks.Add(coinBlock);

//    if (showCoinCounter)
//    {
//        coinCounterText.text = "" + spawnedCoinBlocks.Count;
//    }
//}

//int GetCoinCountOfType(CoinTypes coinType)
//{
//    int _count = 0;
//    foreach (var coin in spawnedCoinBlocks)
//    {
//        if (coinType != coin.CoinType)
//            continue;
//        _count++;
//    }
//    return _count;
//}

//CoinTypes GetRandomCoinTypeToSpawn()
//{
//    List<int> _coinCountList = new();
//    float _totalInverseCoinCount = 0;
//    for(int i = 0; i < gameManager.LevelData.coinTypeList.Count; i++)
//    {
//        int _coinCount = GetCoinCountOfType(gameManager.LevelData.coinTypeList[i]);
//        _coinCountList.Add(_coinCount);
//        if (_coinCount == 0)
//        {
//            return gameManager.LevelData.coinTypeList[i];
//        }
//        _totalInverseCoinCount += 1.0f / _coinCount;
//    }


//    float _randomValue = Random.value * _totalInverseCoinCount;

//    float _sumOfCoinCount = 0;
//    for (int i = 0; i < _coinCountList.Count; i++)
//    {
//        _sumOfCoinCount += 1.0f / _coinCountList[i];
//        if (_randomValue <= _sumOfCoinCount)
//        {
//            return gameManager.LevelData.coinTypeList[i];
//        }
//    }


//    return gameManager.LevelData.coinTypeList[Random.Range(0, gameManager.LevelData.coinTypeList.Count)];//THIS LINE NEVER REACHES. IF SOMEHOW COULDNT GET FROM ABOVE, WILL RETURN RANDOM LIST
//}



//public Vector3 GetRandomPos()
//{
//    float randomAngle = Random.Range(0, 360) * Mathf.PI / 180;
//    float _radius = Random.Range(spawnRadius / 4, spawnRadius);
//   return new Vector3(transform.position.x + _radius * Mathf.Cos(randomAngle), transform.position.y + _radius * Mathf.Sin(randomAngle), spawn_pos_z);
//}

//public Vector3 GetRandomReturnPos()
//{
//    float randomAngle = Random.Range(0, 360) * Mathf.PI / 180;
//    float _radius = Random.Range(returnRadius / 4, returnRadius);
//    return new Vector3(returnTransform.position.x + _radius * Mathf.Cos(randomAngle), returnTransform.position.y + _radius * Mathf.Sin(randomAngle), spawn_pos_z);
//}

//public Vector3 GetRandomSpawnBlockPos()
//{
//    float randomAngle = Random.Range(0, 360) * Mathf.PI / 180;
//    float _radius = Random.Range(blockSpawnerRadius / 4, blockSpawnerRadius);
//    return new Vector3(blockSpawnerTransform.position.x + _radius * Mathf.Cos(randomAngle), blockSpawnerTransform.position.y + _radius * Mathf.Sin(randomAngle), spawn_pos_z);
//}
//#endregion


//#region BOMB
//[SerializeField] List<BombBlock> spawnedBombBlocks = new();
//public List<BombBlock> SpawnedBombBlocks { get => spawnedBombBlocks; set => spawnedBombBlocks = value; }


//public void SpawnBombBlock(string bombTag, Vector3 pos, float size = 1)
//{
//    var bomb = objectSpawner.GetBlock(bombTag);
//    if (bomb.TryGetComponent<BombBlock>(out var spawnedBomb))
//    {
//        spawnedBombBlocks.Add(spawnedBomb);
//        spawnedBomb.BombSize = size;
//        spawnedBomb.transform.position = pos;
//        spawnedBomb.ScaleTo(0.5f);

//    }

//}

//#endregion

//#region STAR
//[SerializeField] List<StarBlock> spawnedStarBlocks = new();
//public List<StarBlock> SpawnedStarBlocks { get => spawnedStarBlocks; set => spawnedStarBlocks = value; }
//[SerializeField] string[] starNames = new string[5];
//public void SpawnStar(StarTypes starTypes)
//{
//    string _starBlockTag = starNames[(int)starTypes];

//    var starBlock = objectSpawner.GetBlock(_starBlockTag) as StarBlock;
//    SpawnedStarBlocks.Add(starBlock);
//    starBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//    starBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//}

//IEnumerator initialStarSpawnCoroutine;
//IEnumerator InitialStarSpawnCoroutine()
//{
//    foreach (var strBlck in gameManager.LevelData.starTypeColors)
//    {
//        for (int i = 0; i < gameManager.LevelData.minStarOnScene[strBlck]; i++)
//        {
//            yield return new WaitForSeconds(spawnTimeInterval * 10);
//            var starBlock = objectSpawner.GetBlock(starNames[strBlck]) as StarBlock;
//            SpawnedStarBlocks.Add(starBlock);
//            starBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//            starBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);

//        }
//    }
//}

//public void InitialStarSpawn()
//{
//    if (initialStarSpawnCoroutine != null)
//    {
//        StartCoroutine(initialStarSpawnCoroutine);
//    }
//    initialStarSpawnCoroutine = InitialStarSpawnCoroutine();
//    StartCoroutine(initialStarSpawnCoroutine);

//}

//#endregion

//[SerializeField] List<Block> spawnedBlocks = new();
//public List<Block> SpawnedBlocks { get => spawnedBlocks; set => spawnedBlocks = value; }

//#region HEART
//public void SpawnBlock(BlockTypes blockType)
//{
//    Vector3 _spawnPos = GetRandomSpawnBlockPos();
//    switch (blockType)
//    {
//        case BlockTypes.Heart:
//            var _heartBlock = objectSpawner.GetBlock("heartBlock") as HeartBlock;
//            SpawnedBlocks.Add(_heartBlock);
//            //_heartBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//            _heartBlock.transform.position = _spawnPos;
//            _heartBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//            break;
//        case BlockTypes.Ice:
//            var _iceBlock = objectSpawner.GetBlock("iceBlock") as IceBlock;
//            SpawnedBlocks.Add(_iceBlock);
//            //_iceBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//            _iceBlock.transform.position = _spawnPos;
//            _iceBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//            break;
//        case BlockTypes.RedRock:
//            var _redRockBlock = objectSpawner.GetBlock("redRockBlock") as RedRockBlock;
//            SpawnedBlocks.Add(_redRockBlock);
//            //_iceBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//            _redRockBlock.transform.position = _spawnPos;
//            _redRockBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//            break;
//        case BlockTypes.Barrel:
//            var _barrelBlock = objectSpawner.GetBlock("barrelBlock") as BarrelBlock;
//            SpawnedBlocks.Add(_barrelBlock);
//            //_iceBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//            _barrelBlock.transform.position = _spawnPos;
//            _barrelBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//            break;
//    }
//}

////HEART
////IEnumerator initialHeartBlockSpawnCoroutine;
////IEnumerator InitialHeartBlockSpawnCoroutine()
////{
////    for (int i = 0; i < gameManager.LevelData.minHeartOnScene; i++)
////    {
////        yield return new WaitForSeconds(spawnTimeInterval * 10);
////        var _heartBlock = objectSpawner.GetBlock("heartBlock") as HeartBlock;
////        SpawnedBlocks.Add(_heartBlock);
////        _heartBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
////        _heartBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
////    }
////}

//public int GetBlockTypeCountOnScene(BlockTypes blockType)
//{
//    int _count = 0;
//    if(SpawnedBlocks.Count == 0)
//    {
//        return 0;
//    }
//    foreach (var blk in SpawnedBlocks)
//    {
//        if (blk.BlockTypes == blockType)
//        {
//            _count++;
//        }
//    }
//    return _count;
//}

////public void InitialHeartBlockSpawn()
////{
////    if (GetBlockTypeCountOnScene(BlockTypes.Heart) >= gameManager.LevelData.minHeartOnScene)
////    {
////        return;
////    }
////    if (initialHeartBlockSpawnCoroutine != null)
////    {
////        StartCoroutine(initialHeartBlockSpawnCoroutine);
////    }
////    initialHeartBlockSpawnCoroutine = InitialHeartBlockSpawnCoroutine();
////    StartCoroutine(initialHeartBlockSpawnCoroutine);

////}

////ICE
////IEnumerator initialIceBlockSpawnCoroutine;
////IEnumerator InitialIceBlockSpawnCoroutine()
////{
////    for (int i = 0; i < gameManager.LevelData.minIceOnScene; i++)
////    {
////        yield return new WaitForSeconds(spawnTimeInterval * 10);
////        var _iceBlock = objectSpawner.GetBlock("iceBlock") as IceBlock;
////        SpawnedBlocks.Add(_iceBlock);
////        _iceBlock.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
////        _iceBlock.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
////    }
////}

////public void InitialIceBlockSpawn()
////{
////    if(GetBlockTypeCountOnScene(BlockTypes.Ice) >= gameManager.LevelData.minIceOnScene)
////    {
////        return;
////    }
////    if (initialIceBlockSpawnCoroutine != null)
////    {
////        StartCoroutine(initialIceBlockSpawnCoroutine);
////    }
////    initialIceBlockSpawnCoroutine = InitialIceBlockSpawnCoroutine();
////    StartCoroutine(initialIceBlockSpawnCoroutine);

////}

////BLOCK
//IEnumerator initialBlockSpawnCoroutine;
//IEnumerator InitialBlockSpawnCoroutine(int minOnScene, string _blockTag)
//{
//    for (int i = 0; i < minOnScene; i++)
//    {
//        yield return new WaitForSeconds(spawnTimeInterval * 10);
//        var _block = objectSpawner.GetBlock(_blockTag);
//        SpawnedBlocks.Add(_block);
//        //_block.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(this.transform.position.y, this.transform.position.y + 2.5f), spawn_pos_z);
//        _block.transform.position = GetRandomSpawnBlockPos();
//        _block.GetComponent<Rigidbody>().velocity = new Vector3(0, -5, 0);
//    }
//}

//public void InitialBlockSpawn(BlockTypes blockType, int minOnScene, string _blockTag)
//{

//    //if (GetBlockTypeCountOnScene(blockType) >= minOnScene)
//    //{
//    //    return;
//    //}
//    //if (initialBlockSpawnCoroutine != null)
//    //{
//    //    StartCoroutine(initialBlockSpawnCoroutine);
//    //}

//    if (GetBlockTypeCountOnScene(blockType) >= minOnScene)
//    {
//        return;
//    }
//    Debug.Log("BLOCKTYEP = " + blockType);
//    initialBlockSpawnCoroutine = InitialBlockSpawnCoroutine(minOnScene, _blockTag);
//    StartCoroutine(initialBlockSpawnCoroutine);

//}

//#endregion
//}

//bool ControlTopColor(int minAvailable)//minAvailable is the number that needs to control spawning. for example. if minAvailable==3, means there are only 3 spots available.
//{
//    int count = 0;
//    for (int i = 0; i < GridManager.Instance.dataLevel.grid.GridSizeX; i++)
//    {
//        for (int j = 0; j < GridManager.Instance.dataLevel.grid.GridSizeY; j++)
//        {
//            if (GridManager.Instance.cellMap2DArray[i].rows[j].cellStatus == CellStatus.Open
//                && GridManager.Instance.cellMap2DArray[i].rows[j].isPlacable)
//            {
//                count++;
//                if (count >= minAvailable)
//                {
//                    return false;
//                }
//            }
//        }
//    }
//    return true;
//}

//public void UpdateSwapButtonPos()
//{
//    var _spareBlockStack = blockStacks[0];
//    if (_spareBlockStack.isReadyToShoot)
//    {
//        _spareBlockStack = blockStacks[1];
//    }

//    UIManager.Instance.swapButtonTransform.localPosition = spareBlockStackSpawnerTransform.localPosition +  (_spareBlockStack.GetTopLocalPosition().y + GridManager.Instance.offsetY) * Vector3.up;
//}

//Check how many available spaces are available? Control top color when there is less than 3? available spaces.


//if (emptyCellCount < 4)//SINGLE COLOR
//{
//    int[] _c = new int[currentNumbOfColorsInStack];
//    for (int j = 0; j < numbOfBlocksInStack - currentNumbOfColorsInStack; j++)
//    {
//        _c[Random.Range(0, currentNumbOfColorsInStack)] += 1;
//    }

//    List<int> _tempTypes = GetTopBlockTypes();

//    if (_tempTypes.Count == 0)
//    {
//        //int[] _c = new int[currentNumbOfColorsInStack];
//        for (int j = 0; j < numbOfBlocksInStack - currentNumbOfColorsInStack; j++)
//        {
//            _c[Random.Range(0, currentNumbOfColorsInStack)] += 1;
//        }

//        for (int i = 0; i < currentNumbOfColorsInStack; i++)
//        {
//            CreateBlock(_blockStack, _blockTypes[i], _c[i] + 1);
//        }
//    }
//    else
//    {
//        Debug.Log("_tempTypes.Count = " + _tempTypes.Count);
//        int _tmpRnd = _tempTypes[Random.Range(0, _tempTypes.Count)];
//        Debug.Log("_tmpRnd = " + _tmpRnd);
//        for (int i = 0; i < currentNumbOfColorsInStack; i++)
//        {
//            CreateBlock(_blockStack, _tmpRnd, _c[i] + 1);//ONE COLOR
//        }
//    }

//}
////else if (emptyCellCount < 6)//DOUBLE COLOR
////{
////    //PURE RANDOM. How can I make sure that there is matching color.
////    int[] _c = new int[currentNumbOfColorsInStack];
////    for (int j = 0; j < numbOfBlocksInStack - currentNumbOfColorsInStack; j++)
////    {
////        _c[Random.Range(0, currentNumbOfColorsInStack)] += 1;
////    }

////    for (int i = 0; i < currentNumbOfColorsInStack; i++)
////    {
////        CreateBlock(_blockStack, _blockTypes[i], _c[i] + 1);
////    }
////}
//else//PURE RANDOM
//{
//    //PURE RANDOM. How can I make sure that there is matching color.

//    int[] _c = new int[currentNumbOfColorsInStack];
//    for (int j = 0; j < numbOfBlocksInStack - currentNumbOfColorsInStack; j++)
//    {
//        _c[Random.Range(0, currentNumbOfColorsInStack)] += 1;
//    }

//    for (int i = 0; i < currentNumbOfColorsInStack; i++)
//    {
//        CreateBlock(_blockStack, _blockTypes[i], _c[i] + 1);
//    }
//}


