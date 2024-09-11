using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class RobotsPool : ObjectPoolsManager
{

    #region -- 資源參考區 --

    [Header("RobotsPool")]
    [SerializeField] private GameObject poolGameObject;
    [SerializeField] private Transform instantiateRoot;
    [SerializeField] private float firstDelayInstantiate = 5f;
    [SerializeField] private float repeatRate = 2f;

    #endregion

    #region -- 變數參考區 --

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
	{

        InvokeRepeating("SpawnRobots", firstDelayInstantiate, repeatRate);

    }

	private void Update()
	{
		
	}

	#endregion
	
	#region -- 方法參考區 --

    private void SpawnRobots()
    {

        ReUse(new Vector3(RandomHelper.RandomRangeSubSpecificRange(-150f, 150f, -50f, 50f), 100f, 
            RandomHelper.RandomRangeSubSpecificRange(-100f, 100f, -50f, 50f)),
            Quaternion.identity, ObjectPoolType.Robot, poolGameObject, instantiateRoot);

    }

    #endregion

}
