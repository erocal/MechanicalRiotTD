using System;
using System.Security.Claims;
using UnityEngine;
using static UnityEngine.LightAnchor;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    #region -- 資源參考區 --

    [SerializeField] ThirdPersonCamera thirdPersonCamera;

    [Header("移動速度")]
    [SerializeField, Tooltip("移動速度")] float moveSpeed = 8;
    [SerializeField, Range(1, 3), Tooltip("加速的倍數")] float sprintSpeedModifier = 2;
    [SerializeField, Range(0, 1), Tooltip("蹲下時的減速倍數")] float crouchedSpeedModifer = 0.5f;
    [SerializeField, Tooltip("旋轉速度")] float rotateSpeed = 5f;
    [SerializeField, Tooltip("加速度百分比")] float addSpeedRatio = 0.1f;

    [Space(20)]
    [Header("重力參數")]
    [SerializeField, Tooltip("在空中下施加的力量")] float gravityDownForce = 50;

    #endregion

    #region -- 變數參考區 --

    #region -- Action --

    public event Action onCaplock;

    #endregion

    private InputController input;
    private CharacterController characterController;
    private Camera m_Camera;

    [Tooltip("下一幀要移動到的目標位置")]
    private Vector3 targetMovement;
    [Tooltip("上一幀的移動速度")]
    private float lastFrameSpeed = 0.0f;

    [Tooltip("下一幀跳躍到的方向")]
    Vector3 jumpDirection;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        Init();

    }

    private void Update()
    {

        MoveBehaviour();
        GravityBehaviour();

    }

    #endregion

    #region -- 方法參考區 --

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {

        input = InputController.Instance;
        m_Camera = Camera.main;
        characterController = GetComponent<CharacterController>();

    }

    /// <summary>
    /// 處理移動行為
    /// </summary>
    private void MoveBehaviour()
    {
        targetMovement = Vector3.zero;
        Vector3 pretargetMovement = targetMovement;
        targetMovement += input.GetMoveInput().z * GetCurrentCameraForward();
        targetMovement += input.GetMoveInput().x * GetCurrentCameraRight();

        // 避免對角線超過1
        targetMovement = Vector3.ClampMagnitude(targetMovement, 1);

        // 下一幀的移動速度
        float nextFrameSpeed = 0;

        if (targetMovement == Vector3.zero)
        {
            nextFrameSpeed = 0f;
        }
        // 如果加速鍵被按下且不在瞄準時
        else if (input.GetCapInput())
        {

            nextFrameSpeed = 1f;
            targetMovement *= sprintSpeedModifier;
            SmoothRotation(targetMovement);
            onCaplock?.Invoke();

        }
        else
        {
            nextFrameSpeed = 0.5f;
            SmoothRotation(targetMovement);
        }

        // 當前後Frame速度不一致，線性更改速度
        if (lastFrameSpeed != nextFrameSpeed)
        {
            lastFrameSpeed = Mathf.Lerp(lastFrameSpeed, nextFrameSpeed, addSpeedRatio);
        }

        // 動態變化移動速度
        characterController.Move(moveSpeed * Time.deltaTime * targetMovement);

    }

    /// <summary>
    /// 取得目前相機的正面方向
    /// </summary>
    private Vector3 GetCurrentCameraForward()
    {
        Vector3 cameraForward = m_Camera.transform.forward;
        cameraForward.y = 0f;
        //歸一化
        cameraForward.Normalize();
        return cameraForward;
    }

    /// <summary>
    /// 取得目前相機的右側方向
    /// </summary>
    private Vector3 GetCurrentCameraRight()
    {
        Vector3 cameraRight = m_Camera.transform.right;
        cameraRight.y = 0f;
        //歸一化
        cameraRight.Normalize();
        return cameraRight;
    }

    /// <summary>
    /// 平滑旋轉角度到目標方向
    /// </summary>
    /// <param name="targetMovement">目標方向</param>
    private void SmoothRotation(Vector3 targetMovement)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetMovement, Vector3.up), rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 重力行為
    /// </summary>
    private void GravityBehaviour()
    {

        jumpDirection.y -= gravityDownForce * Time.deltaTime;
        jumpDirection.y = Mathf.Max(jumpDirection.y, -gravityDownForce);

        characterController.Move(jumpDirection * Time.deltaTime);

    }

    #endregion

}
