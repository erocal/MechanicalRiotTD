using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScanner : MonoBehaviour
{

    #region -- 資源參考區 --

    [Tooltip("terrainScanner特效")]
    [SerializeField] private GameObject terrainScannerVFX;
    [Tooltip("生成按鍵")]
    [SerializeField] private KeyCode instantiateKeyCode = KeyCode.F;
    [Tooltip("生成位置")]
    [SerializeField] private Transform instantiatePos;
    [Tooltip("持續時間")]
    [SerializeField] private float duration = 10;
    [Tooltip("生成大小")]
    [SerializeField] private float size = 500;

    #endregion

    #region -- 變數參考區 --

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

    }

    private void Update()
    {

        if (Input.GetKeyUp(instantiateKeyCode)) SpawnTerrainScanner();


    }

    #endregion

    #region -- 方法參考區 --

    /// <summary>
    /// 生成TerrainScanner特效
    /// </summary>
    private void SpawnTerrainScanner()
    {

        GameObject terrainScanner = 
            Instantiate(terrainScannerVFX, instantiatePos.position, Quaternion.identity, this.transform);
        ParticleSystem terrainScannerParticleSystem = terrainScanner.transform.GetChild(0).GetComponent<ParticleSystem>();

        if (terrainScannerParticleSystem != null)
        {

            ParticleSystem.MainModule main = terrainScannerParticleSystem.main;

            main.startLifetime = duration;
            main.startSize = size;

            terrainScanner.SetActive(true);

        }
        else Debug.LogError("找不到terrainScanner特效");

        Destroy(terrainScanner, duration + 1f);

    }

    #endregion

}
