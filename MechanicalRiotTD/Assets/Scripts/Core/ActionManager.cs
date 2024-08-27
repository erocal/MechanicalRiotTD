using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    #region -- 資源參考區 --

    public Action onScan;

    #endregion

    #region -- 變數參考區 --

    private static ActionManager _instance;

    public static ActionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ActionManager>();
            }

            return _instance;
        }
        private set { }
    }

    #endregion

    #region -- 初始化/運作 --

    private ActionManager()
    {

    }

    private void Awake()
	{

        GetInstance();

    }

	private void Update()
	{
		
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
            _instance = FindObjectOfType<ActionManager>();
            return;
        }
    }

    #endregion

    #endregion

}
