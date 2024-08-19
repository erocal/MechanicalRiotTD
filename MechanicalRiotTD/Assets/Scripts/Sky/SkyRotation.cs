using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Volume))]
public class SkyRotation : MonoBehaviour
{

    #region -- 資源參考區 --

    [SerializeField] private float rotationSpeed = .5f;

    #endregion

    #region -- 變數參考區 --

    private Volume volume;
    private VolumeProfile profile;
    private PhysicallyBasedSky physicallyBasedSky;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
	{

        volume = this.GetComponent<Volume>();
        profile = volume.sharedProfile;
        if (profile == null )
        {
            Debug.LogError("VolumeProfile是空的");
            return;
        }

        if (!profile.TryGet(out physicallyBasedSky))
        {
            return;
        }

    }

	private void FixedUpdate()
	{

        physicallyBasedSky.spaceRotation.Override(new Vector3(0, Time.timeSinceLevelLoad % 360 * rotationSpeed, 0));

    }

	#endregion
	
	#region -- 方法參考區 --

	#endregion
	
}
