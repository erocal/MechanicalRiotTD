using System.Collections;
using UnityEngine;

public class RobotController : RobotsPool
{

    #region -- 資源參考區 --

    [SerializeField] private float scanRevealDuration = 5f;
    [SerializeField] private float selfDestructDuration = 60f;

    #endregion

    #region -- 變數參考區 --

    int initLayer = 0;
    private Coroutine currentRevealCoroutine;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        initLayer = this.gameObject.layer;
        StartCoroutine("SelfDestruct");

    }

    private void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<ScanParticle>() != null)
        {

            SetLayerRecursively(this.gameObject, 6);

            if (currentRevealCoroutine != null)
            {
                StopCoroutine(currentRevealCoroutine);
            }

            currentRevealCoroutine = StartCoroutine(ScanReveal());

        }

    }

    #endregion

    #region -- 方法參考區 --

    private IEnumerator SelfDestruct()
    {

        yield return new WaitForSeconds(selfDestructDuration);

        Recovery(this.gameObject, ObjectPoolType.Robot);

    }

    /// <summary>
    /// 設置母物件底下的所有Layer
    /// </summary>
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer; // 設定當前物件的 Layer

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer); // 遞迴呼叫來設定子物件的 Layer
        }
    }

    private IEnumerator ScanReveal()
    {

        yield return new WaitForSeconds(scanRevealDuration);

        SetLayerRecursively(this.gameObject, initLayer);

        currentRevealCoroutine = null;

    }

    #endregion

}
