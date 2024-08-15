using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class TopographicScanController : MonoBehaviour
{

	#region -- 資源參考區 --

    public Transform sacnCenter;
    public KeyCode scanKey;

    #endregion

    #region -- 變數參考區 --

    Volume volume;
    VolumeProfile profile;
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

        topographicScanHDRP.origin1.Override(sacnCenter.position);
        topographicScanHDRP.distance.Override(distance);

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
        

    }

    #endregion

}
