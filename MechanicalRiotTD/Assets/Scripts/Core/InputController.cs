using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    #region -- �ѼưѦҰ� --

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

    public float vertical;
    public float horizontal;

    #endregion

    #region -- ��l��/�B�@ --

    private InputController()
    {
    }

    private void Awake()
    {

        GetInstance();
        //�]�w��Ъ��A (��w)
        Cursor.lockState = CursorLockMode.None;
        // �O�_��ܴ��
        Cursor.visible = true;

    }

    private void Update()
    {
        CheckCursorState();
        //vertical = Input.GetAxis("Vertical");
        //horizontal = Input.GetAxis("Horizontal");
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    #endregion

    #region -- ��k�ѦҰ� --

    #region -- ��ҼҦ� --

    /// <summary>
    /// ����ߤ@���
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
    /// ���oWASD��Axis
    /// </summary>
    public Vector3 GetMoveInput()
    {
        if (CanProcessInput())
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            //����t��
            move = Vector3.ClampMagnitude(move, 1);

            return move;
        }

        return Vector3.zero;
    }

    /// <summary>
    /// �O�_���UCapsLock�[�t
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
    /// �O�_����Control�[�t
    /// </summary>
    public bool GetCprintInput()
    {
        if (CanProcessInput())
        {
            return Input.GetKey(KeyCode.LeftControl);
        }
        return false;
    }

    /// <summary>
    /// �O�_���UControl�[�t
    /// </summary>
    public bool GetCprintInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetKeyDown(KeyCode.LeftControl);
        }
        return false;
    }

    /// <summary>
    /// �O�_����Space���D
    /// </summary>
    public bool GetJumpInput()
    {
        if (CanProcessInput())
        {
            return Input.GetKey(KeyCode.Space);
        }
        return false;
    }

    /// <summary>
    /// �O�_���USpace���D
    /// </summary>
    public bool GetJumpInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        return false;
    }

    /// <summary>
    /// ���o Mouse X �� Axis
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
    /// ���o Mouse Y �� Axis
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
    /// ���o�u���� Axis
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
    /// �O�_���U�ƹ�����
    /// </summary>
    public bool GetClick()
    {
        return Input.GetMouseButtonDown(0);
    }

    /// <summary>
    /// ���o�O�_���U�ƹ�����(�}��)
    /// </summary>
    public bool GetFireInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetMouseButtonDown(0);
        }
        return false;
    }

    /// <summary>
    /// ���o�O�_������U�ƹ�����(�}��)
    /// </summary>
    public bool GetFireInputHeld()
    {
        if (CanProcessInput())
        {
            return Input.GetMouseButton(0);
        }
        return false;
    }

    /// <summary>
    /// ���o�O�_��}�ƹ�����(�}��)
    /// </summary>
    public bool GetFireInputUp()
    {
        if (CanProcessInput())
        {
            return Input.GetMouseButtonUp(0);
        }
        return false;
    }

    /// <summary>
    /// ���o�O�_���U�ƹ��k��(�˷�)
    /// </summary>
    public bool GetAimInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetMouseButtonDown(1);
        }
        return false;
    }

    /// <summary>
    /// ���o�ƹ��O�_���UReload
    /// </summary>
    public bool GetReloadInputDown()
    {
        if (CanProcessInput())
        {
            return Input.GetKeyDown(KeyCode.R);
        }
        return false;
    }

    /// <summary>
    /// ���o�O�_���U�����Z���A�V������ : -1�A�V�k���� : 1
    /// </summary>
    public int GetSwichWeaponInput()
    {
        if (CanProcessInput())
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                return -1;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                return 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// �T�{�e���O�_����w���A
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
    /// ��s���Ъ��A����w
    /// </summary>
    public void CursorStateLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// ��ܹ���&��s���Ъ��A������w
    /// </summary>
    public void CursorStateUnlocked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// �^�ǹ��Ъ��A�O�_�B����w
    /// </summary>
    public bool CanProcessInput()
    {
        // �p�GCursor���A���b��w���N����B�zInput
        return Cursor.lockState == CursorLockMode.Locked;
    }

    #endregion
}