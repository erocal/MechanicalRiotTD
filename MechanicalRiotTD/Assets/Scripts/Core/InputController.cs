using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    #region -- 參數參考區 --

    private static InputController _instance;

    public static InputController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputController>();
            }

            return _instance;
        }
        private set { }
    }

    #endregion

    #region -- 初始化/運作 --

    private InputController()
    {
    }

    private void Awake()
    {

        GetInstance();
        //設定游標狀態 (鎖定)
        Cursor.lockState = CursorLockMode.None;
        // 是否顯示游標
        Cursor.visible = true;

    }

    private void Update()
    {
        CheckCursorState();
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    #endregion

    #region -- 方法參考區 --

    #region -- 單例模式 --

    /// <summary>
    /// 獲取唯一實例
    /// </summary>
    private void GetInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            _instance = FindObjectOfType<InputController>();
            return;
        }
    }

    #endregion

    /// <summary>
    /// 取得WASD的Axis
    /// </summary>
    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //限制速度
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// 是否按下CapsLock加速
    /// </summary>
    public bool GetCapInput()
    {
        if (CanProcessInput())
        {
            return Input.GetKey(KeyCode.CapsLock);
        }
        return false;
    }

    /// <summary>
    /// 是否按下Scan按鍵
    /// </summary>
    public bool GetScanInput()
    {
        if (CanProcessInput())
        {
            return Input.GetKeyUp(KeyCode.F);
        }
        return false;
    }

    /// <summary>
    /// 取得 Mouse X 的 Axis
    /// </summary>
    public float GetMouseXAxis()
    {
        if (CanProcessInput())
        {
            return Input.GetAxis("Mouse X");
        }
        return 0;
    }

    /// <summary>
    /// 取得 Mouse Y 的 Axis
    /// </summary>
    public float GetMouseYAxis()
    {
        if (CanProcessInput())
        {
            return Input.GetAxis("Mouse Y");
        }
        return 0;
    }

    /// <summary>
    /// 取得滾輪的 Axis
    /// </summary>
    public float GetMouseScrollWheelAxis()
    {
        if (CanProcessInput())
        {
            return -Input.GetAxis("Mouse ScrollWheel");
        }
        return 0;
    }

    /// <summary>
    /// 確認畫面是否為鎖定狀態
    /// </summary>
    private void CheckCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {

            if (Cursor.lockState == CursorLockMode.None)
                CursorStateLocked();
            else 
                CursorStateUnlocked();

        }
    }

    /// <summary>
    /// 更新鼠標狀態為鎖定
    /// </summary>
    public void CursorStateLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// 顯示鼠標&更新鼠標狀態為未鎖定
    /// </summary>
    public void CursorStateUnlocked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// 回傳鼠標狀態是否處於鎖定
    /// </summary>
    public bool CanProcessInput()
    {
        // 如果Cursor狀態不在鎖定中就不能處理Input
        return Cursor.lockState == CursorLockMode.Locked;
    }

    #endregion
}