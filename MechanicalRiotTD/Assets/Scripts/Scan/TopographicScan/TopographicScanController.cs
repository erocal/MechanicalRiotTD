using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class TopographicScanController : MonoBehaviour
{

	#region -- 資源參考區 --

    public Transform sacnCenter;
    public KeyCode scanKey = KeyCode.F;

    #endregion

    #region -- 變數參考區 --

    #region -- 常數 --

    private const float SCAN_SECONDS = 5f;

    #endregion

    Volume volume;
    VolumeProfile profile;
    TopographicScanHDRP topographicScanHDRP;
    float distance;
    bool isScan;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
	{
        volume = this.GetComponent<Volume>();
        profile = volume.sharedProfile;

    }

	private void Update()
	{

        if (!profile.TryGet<TopographicScanHDRP>(out var topographicScanHDRP))
        {
            return;
        }

        this.topographicScanHDRP = topographicScanHDRP;

        this.topographicScanHDRP.distance.Override(distance);

        if (Input.GetKeyUp(scanKey) && !isScan) Scan();

    }

    #endregion

    #region -- 方法參考區 --

    /// <summary>
    /// 掃描
    /// </summary>
    private void Scan()
    {

        isScan = true;

        topographicScanHDRP.origin1.Override(sacnCenter.position);

        DOTween.To(() => distance, x => distance = x, 150f, SCAN_SECONDS)
            .OnComplete(() =>
            {

                isScan = false;
                distance = 0;

            });

    }

    #endregion

}
