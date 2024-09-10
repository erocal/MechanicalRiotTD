using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Volume))]
public class VolumeRadialBlur : MonoBehaviour
{

    #region -- 資源參考區 --

    #endregion

    #region -- 變數參考區 --

    #region -- 常數 --

    private const float CAPLOCK_BLUR_INTENSITY = .2f;
    private const float CAPLOCK_BLUR_DURATION = 2.0f;
    private const float CAPLOCK_BLUR_TO_NORMAL_DURATION = .5f;

    #endregion

    private Volume volume;
    private VolumeProfile profile;
    private RadialBlur radialBlur;
    private InputController input;

    private Tween tween;

    private float intensity = 0f;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
    {

        intensity = 0f;

        volume = this.GetComponent<Volume>();
        profile = volume.sharedProfile;
        if (profile == null)
        {
            Debug.LogError("VolumeProfile是空的");
            return;
        }

        if (!profile.TryGet(out radialBlur))
        {
            return;
        }

        input = InputController.Instance;
        input.onPlayerCaplock += OnPlayerCaplock;

    }

    private void Update()
    {
        if (input.GetCapUp())
        {
            tween.Kill();

            tween = DOTween.To(() => intensity, x => intensity = x, 0f, CAPLOCK_BLUR_TO_NORMAL_DURATION);
        }

        radialBlur.intensity.Override(intensity);
    }

    #endregion

    #region -- 方法參考區 --

    private void OnPlayerCaplock()
    {

        if (tween == null || !tween.IsActive() || tween.IsComplete())
        {

            // 將 Tween 存在變數中
            tween = DOTween.To(() => intensity, x => intensity = x, CAPLOCK_BLUR_INTENSITY, CAPLOCK_BLUR_DURATION).OnComplete(() =>
            {
                // 在達到 0.2 後，開始在 .2 和 .25 之間來回擺動
                tween = DOTween.To(() => intensity, x => intensity = x, CAPLOCK_BLUR_INTENSITY + .05f, 0.5f).SetLoops(-1, LoopType.Yoyo);
            });

        }

    }

    #endregion

}
