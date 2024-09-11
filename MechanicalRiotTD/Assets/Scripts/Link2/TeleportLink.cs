using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeLink2), typeof(Collider))]
public class TeleportLink : MonoBehaviour
{

    #region -- 資源參考區 --

    [SerializeField] private GameObject startTeleportVFX;
    [SerializeField] private GameObject endTeleportVFX;

    #endregion

    #region -- 變數參考區 --

    NodeLink2 link;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        link = GetComponent<NodeLink2>();

    }

    private void Update()
    {

    }

    #endregion

    #region -- 方法參考區 --

    public void Teleport(Transform transform)
    {

        if (startTeleportVFX != null)
            startTeleportVFX.SetActive(true);

        transform.position = link.end.transform.position;

        if (endTeleportVFX != null)
            endTeleportVFX.SetActive(true);

    }

    #endregion

}
