using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class DayNightCycle : MonoBehaviour
{

    #region -- 資源參考區 --

    [Tooltip("指向你的Directional Light")]
    public Light sun;
    [Tooltip("一天的長度（以秒計）")]
    public float dayDuration = 120f;

    #endregion

    #region -- 變數參考區 --

    private float currentTime = 0f;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        sun = GetComponent<Light>();

    }

    private void FixedUpdate()
    {

        currentTime += Time.fixedDeltaTime;
        float dayFraction = currentTime / dayDuration;
        float sunAngle = Mathf.Lerp(-90f, 270f, dayFraction);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // 調整Temperature
        if (sunAngle > 0f && sunAngle < 180f) // 日間
        {
            sun.colorTemperature = Mathf.Lerp(3000f, 6500f, (sunAngle / 180f)); // 日出到正午
            sun.intensity = Mathf.Lerp(0f, 1f, (sunAngle / 180f));
        }
        else // 傍晚與夜間
        {
            sun.colorTemperature = Mathf.Lerp(6500f, 3000f, ((sunAngle - 180f) / 180f)); // 正午到日落
            sun.intensity = Mathf.Lerp(1f, 0f, ((sunAngle - 180f) / 180f));
        }

        // 調整Filter
        if (sunAngle < 0f || sunAngle > 180f) // 黃昏或黎明時段
        {
            sun.color = Color.Lerp(new Color(1f, 0.5f, 0.5f), new Color(1f, 1f, 1f), ((sunAngle + 90f) % 180f) / 180f);
        }
        else // 白天
        {
            sun.color = Color.Lerp(new Color(1f, 1f, 1f), new Color(1f, 0.5f, 0.5f), ((sunAngle - 90f) % 180f) / 180f);
        }

        if (currentTime >= dayDuration)
        {
            currentTime = 0f;  // 重置時間，開始新的一天
        }

    }

    #endregion

    #region -- 方法參考區 --

    #endregion

}
