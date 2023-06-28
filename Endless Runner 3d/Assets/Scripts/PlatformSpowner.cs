using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpowner : MonoBehaviour
{
    public static PlatformSpowner instance;

    [SerializeField] GameObject platformPrefab;
    [SerializeField] float nextSPownPos;
    public int obstacleSpownPropapility;

    //For Object Pooling
    public List<GameObject> poolingObject;
    public int amountOfObjPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountOfObjPool; i++)  //Make Object Pooling List
        {
            GameObject newPoolObj = Instantiate(platformPrefab);
            newPoolObj.transform.parent = this.transform;
            newPoolObj.SetActive(false);
            poolingObject.Add(newPoolObj);
        }
    }

    private GameObject GetPooledObj()  //Give Disable to Enable ("Object Pooling")
    {
        for (int i = 0; i < poolingObject.Count; i++)
        {
            if (!poolingObject[i].activeInHierarchy)
            {
                return poolingObject[i];
            }
        }

        return null;
    }
    public void SpownPlatform()
    {
        GameObject newPlatform = GetPooledObj();

        if (newPlatform != null)
        {
            newPlatform.SetActive(true);
            newPlatform.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + nextSPownPos);
            nextSPownPos += 100;
        }
    }
}
