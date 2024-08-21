using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    #region -- 資源參考區 --

    [SerializeField] InputController input;

    [Header("Camera跟隨的目標")]
    [SerializeField] Transform target;

    [Header("水平軸靈敏度")]
    [SerializeField] float sensitivity_X = 2;
    [Header("垂直軸靈敏度")]
    [SerializeField] float sensitivity_Y = 2;
    [Header("滾輪靈敏度")]
    [SerializeField] float sensitivity_Z = 5;

    [Header("最小垂直角度")]
    [SerializeField] float minVerticalAngle = -10;
    [Header("最大垂直角度")]
    [SerializeField] float maxVerticalAngle = 85;
    [Header("相機與目標的距離")]
    [SerializeField] float cameraToTargetDistance = 10;
    [Header("最小相機與目標的距離")]
    [SerializeField] float minDistance = 2;
    [Header("最大相機與目標的距離")]
    [SerializeField] float maxDistance = 25;

    [Header("Offset")]
    [SerializeField] Vector3 offset;
    [SerializeField] float offset_Y = 100f;

    #endregion

    #region -- 變數參考區 --

    float mouse_X = 0;
    float mouse_Y = 30;

    bool isChange;

    // 應該是看滑鼠是不是鎖住的狀態
    bool isLocked = false;


    #endregion

    #region -- 初始化/運作 --

    private void Awake()
	{

    }

    private void LateUpdate()
	{

        if (Cursor.lockState == CursorLockMode.Locked)
        {

            mouse_X += input.GetMouseXAxis() * sensitivity_X;
            mouse_Y += input.GetMouseYAxis() * sensitivity_Y;

            // 限制垂直角度
            mouse_Y = Mathf.Clamp(mouse_Y, minVerticalAngle, maxVerticalAngle);

            // 固定相機在玩家身後
            transform.rotation = Quaternion.Euler(mouse_Y, mouse_X, 0);
            transform.position = Quaternion.Euler(mouse_Y, mouse_X, 0) * new Vector3(0, 0, -cameraToTargetDistance) + target.position + Vector3.up * offset.y;

            // 滑鼠滾輪控制遠近
            cameraToTargetDistance += input.GetMouseScrollWheelAxis() * sensitivity_Z;
            cameraToTargetDistance = Mathf.Clamp(cameraToTargetDistance, minDistance, maxDistance);

            isLocked = false;

        }
        else
        {

            isLocked = true;

        }

        if (isLocked != isChange)
        {
            isChange = isLocked;
        }

    }

	#endregion
	
	#region -- 方法參考區 --

	#endregion
	
}
