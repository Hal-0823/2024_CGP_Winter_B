using UnityEngine;

/// <summary>
/// ParticleSystemのShapeにSkinnedMeshRendererを割り当てるためのクラス
/// </summary>
public class SetSkinnedMeshToParticle : MonoBehaviour
{
    public ParticleSystem Particle; // パーティクルのプレハブ
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void PlayDeathEffect()
    {
        if (skinnedMeshRenderer == null) return;

        // パーティクルを生成
        ParticleSystem effect = Instantiate(Particle, transform.position, Quaternion.identity);
        
        // Skinned Mesh Renderer を設定
        var shape = effect.shape;
        shape.skinnedMeshRenderer = skinnedMeshRenderer;

        // パーティクル再生
        effect.Play();

        // 一定時間後に削除
        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
    }
}
