using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(SphereCollider))]
public class ScanParticle : MonoBehaviour
{

    #region -- 資源參考區 --

    #endregion

    #region -- 變數參考區 --

    private ParticleSystem particleSystem;
    private SphereCollider sphereCollider;

    #endregion

    #region -- 初始化/運作 --

    private void Awake()
	{

		particleSystem = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<SphereCollider>();

        // 獲取粒子系統的 sizeOverLifetime 模組
        var sizeOverLifetime = particleSystem.sizeOverLifetime;

        // 獲取動畫曲線
        AnimationCurve curve = sizeOverLifetime.size.curve;

        // 初始化 radius 為 0
        sphereCollider.radius = 0f;

        // 使用 DOTween 對 radius 進行動畫
        DOTween.To(() => sphereCollider.radius, x => sphereCollider.radius = x, 250f, particleSystem.main.startLifetimeMultiplier)
            .SetEase(curve);  // 使用 sizeOverLifetime 的曲線速率

    }

	private void Update()
	{

    }

	#endregion
	
	#region -- 方法參考區 --

	#endregion
	
}
