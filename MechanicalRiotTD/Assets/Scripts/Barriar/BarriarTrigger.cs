using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BarriarTrigger : MonoBehaviour
{

    #region -- 資源參考區 --

    [SerializeField] private Transform playerTransform;
    [SerializeField] private OrientationEnum orientationEnum = OrientationEnum.Horizontal;

    #endregion

    #region -- 變數參考區 --

    private Material material;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        material = GetComponent<MeshRenderer>().material;

    }

    private void Update()
    {
        if(orientationEnum == OrientationEnum.Horizontal)
            material.SetFloat("_ABSPlayerDistance", Mathf.Abs(playerTransform.position.z - this.transform.position.z));
        else
            material.SetFloat("_ABSPlayerDistance", Mathf.Abs(playerTransform.position.x - this.transform.position.x));

    }

    #endregion

    #region -- 方法參考區 --

    #endregion

}
