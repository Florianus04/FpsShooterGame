using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static List<pooledObjectInfo> objectPools = new List<pooledObjectInfo>();
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition)
    {
        pooledObjectInfo pool = null;
        foreach (pooledObjectInfo obj in objectPools)
        {
            if (obj.lookUpString == objectToSpawn.name)
            {
                pool = obj;
                break;
            }
        }
        //if the pool doesnt exitst, create it;
        if (pool == null)
        {
            pool = new pooledObjectInfo() { lookUpString = objectToSpawn.name };
            objectPools.Add(pool);
        }
        //check if there any inactive objects in the pool
        GameObject spawnAbleObj = null;
        foreach (GameObject obj in pool.inActiveObjects)
        {
            if (obj != null)
            {
                spawnAbleObj = obj;
                break;
            }
        }
        if (spawnAbleObj == null)
        {
            //if there are no inactive obj, create a new one;
            spawnAbleObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
        else
        {
            //if there is an inactive obj, recreate it;
            spawnAbleObj.transform.position = spawnPosition;
            spawnAbleObj.transform.rotation = Quaternion.identity;
            pool.inActiveObjects.Remove(spawnAbleObj);
            spawnAbleObj.SetActive(true);
        }
        return spawnAbleObj;
    }
    public static void ReturnObjtoPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);
        // by taking off 7, we are removing the(clone) from the name of the passed in obj;
        pooledObjectInfo pool = objectPools.Find(p => p.lookUpString == goName);
        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.inActiveObjects.Add(obj);
        }
    }
}
public class pooledObjectInfo
{
    public string lookUpString;
    public List<GameObject> inActiveObjects = new List<GameObject>();
}
