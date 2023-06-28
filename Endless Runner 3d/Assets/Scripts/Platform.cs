using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Platform : MonoBehaviour
{
    [SerializeField] GameObject[] _ObstaclePrefabsOne;
    [SerializeField] GameObject[] _obstaclePrefabsTwo;
    [SerializeField] GameObject[] _coinPrefabCentre;
    [SerializeField] GameObject[] _coinPrefabRight;
    [SerializeField] GameObject[] _coinPrefabLeft;

    [SerializeField] bool pooled;

    void Start()
    {

        int noForSpownObs = Random.Range(0, 11);

        if (noForSpownObs % PlatformSpowner.instance.obstacleSpownPropapility == 0)
        {
            EnableDoubleObstacle();
            EnableFullRowCoins();
        }
        else
        {
            EnableSingleObstacle();
            SpownHalfRowCoins();
        }

        pooled = true;
    }

    #region Obstacle Spown/Enable
    public void EnableSingleObstacle()  //This Method Creat Single Obstacle
    {
        int randomObsEnbleOne = Random.Range(0, _ObstaclePrefabsOne.Length);
        int randomObsEnbleTwo = Random.Range(0, _obstaclePrefabsTwo.Length);

        _ObstaclePrefabsOne[randomObsEnbleOne].SetActive(true);
        _obstaclePrefabsTwo[randomObsEnbleTwo].SetActive(true);
    }

    public void EnableDoubleObstacle()  //This Method Create Two Obstacles
    {
        int randomObsEnbleOneOne = Random.Range(0, _ObstaclePrefabsOne.Length); //for First ObstaclesList want to spown 1st Obstacle
        int randomObsEnbleOneTwo = Random.Range(0, _ObstaclePrefabsOne.Length); //for First ObstaclesList want to spown 2nd Obstacle

        int randomObsEnbleTwoOne = Random.Range(0, _obstaclePrefabsTwo.Length); //for Second ObstaclesList want to spown 1st Obstacle
        int randomObsEnbleTwoTwo = Random.Range(0, _obstaclePrefabsTwo.Length); //for Second ObstaclesList want to spown 1st Obstacle

        _ObstaclePrefabsOne[randomObsEnbleOneOne].SetActive(true);
        _ObstaclePrefabsOne[randomObsEnbleOneTwo].SetActive(true);
        _obstaclePrefabsTwo[randomObsEnbleTwoOne].SetActive(true);
        _obstaclePrefabsTwo[randomObsEnbleTwoTwo].SetActive(true);
    }
    #endregion
    #region Coins Spown/Enable
    public void EnableFullRowCoins()
    {
        int randomCoinRow = Random.Range(0, 3);

        switch (randomCoinRow)
        {
            case 0:
                foreach (GameObject coin in _coinPrefabLeft)
                {
                    coin.SetActive(true);
                }
                break;

            case 1:
                foreach (GameObject coin in _coinPrefabRight)
                {
                    coin.SetActive(true);
                }
                break;

            case 2:
                foreach (GameObject coin in _coinPrefabCentre)
                {
                    coin.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
    public void SpownHalfRowCoins()
    {
        /*int randomCoinParentNo = Random.Range(5, 8);
        Transform spownTrans = transform.GetChild(randomCoinParentNo).transform;
        childCount = spownTrans.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (i % 2 == 0)
            {
                coinSpownTrans.Add(spownTrans.GetChild(i).transform);
            }

        }

        foreach (Transform tempCoinSpownTrans in coinSpownTrans)
        {
            Instantiate(_coinPrefab, tempCoinSpownTrans.position, _coinPrefab.transform.rotation, transform);
        }*/
    }
    #endregion
    #region Collider
    private void OnTriggerEnter(Collider other)
    {
        PlatformSpowner.instance.SpownPlatform();
    }

    private void OnTriggerExit(Collider other)  //If player Exit from the Platform, Platform will Disable 
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Object Pooling
    private void OnEnable()
    {
        if (pooled)
        {
            //print("I'm Enable");

            int noForSpownObs = Random.Range(0, 11);

            if (noForSpownObs % PlatformSpowner.instance.obstacleSpownPropapility == 0)
            {
                EnableDoubleObstacle();
                SpownHalfRowCoins();
            }
            else
            {
                EnableSingleObstacle();
                EnableFullRowCoins();
            }

        }
    }
    private void OnDisable()
    {
        //print("I'm Disable");

        foreach (GameObject obj in _ObstaclePrefabsOne) //For FirstObstacle
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in _obstaclePrefabsTwo) //For SecondObstacle
        {
            obj.SetActive(false);
        }

        //Coins Disable
        foreach (GameObject coinRow in _coinPrefabCentre)
        {
            coinRow.SetActive(false);
        }

        foreach (GameObject coinRow in _coinPrefabRight)
        {
            coinRow.SetActive(false);
        }

        foreach (GameObject coinRow in _coinPrefabLeft)
        {
            coinRow.SetActive(false);
        }
    }
    #endregion
}
