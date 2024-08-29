using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolsManager : MonoBehaviour
{

    #region -- 變數參考區 --

    static Dictionary<ObjectPoolType, Queue<GameObject>> objectPoolsDictionary = new Dictionary<ObjectPoolType, Queue<GameObject>>()
    {
        { ObjectPoolType.Robot, new Queue<GameObject>() },
    };

    static Dictionary<ObjectPoolType, List<GameObject>> activeObjectsDictionary = new Dictionary<ObjectPoolType, List<GameObject>>()
    {
        { ObjectPoolType.Robot, new List<GameObject>() },
    };

    #endregion

    #region -- 方法參考區 --

    protected void ReUse(Vector3 position, Quaternion rotation, ObjectPoolType objectPoolType, GameObject objectPoolGameObject, Transform objectsInstantiateRoot)
    {

        if (objectPoolsDictionary[objectPoolType].Count > 0)
        {
            GameObject reuse = objectPoolsDictionary[objectPoolType].Dequeue();
            reuse.transform.position = position;
            reuse.transform.rotation = rotation;
            reuse.SetActive(true);
            activeObjectsDictionary[objectPoolType].Add(reuse);
        }
        else
        {

            if(objectPoolGameObject == null || objectsInstantiateRoot == null)
            {
                Debug.LogError($"{objectPoolType}掛載的物件或生成位置為空");
                return;
            }

            GameObject go = Instantiate(objectPoolGameObject, objectsInstantiateRoot);
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);
            activeObjectsDictionary[objectPoolType].Add(go);

        }

    }

    protected void Recovery(GameObject recovery, ObjectPoolType objectPoolType)
    {

        activeObjectsDictionary[objectPoolType].Remove(recovery);
        objectPoolsDictionary[objectPoolType].Enqueue(recovery);
        recovery.SetActive(false);

    }

    #endregion

}
