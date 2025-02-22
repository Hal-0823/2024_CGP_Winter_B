using UnityEngine;

[CreateAssetMenu(menuName="OnHitWallActions/Explosion")]
public class ExplosionAction : OnHitWallAction
{
    public ParticleSystem Particle;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    public override void Execute(BullController bull, Collision col)
    {
        skinnedMeshRenderer = bull.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRendererを取得できませんでした");
            return;
        }

        ParticleSystem effect = Instantiate(Particle, bull.transform.position, Quaternion.identity);

        // Skinned Mesh Renderer を設定
        var shape = effect.shape;
        shape.skinnedMeshRenderer = skinnedMeshRenderer;

        Destroy(bull.gameObject, 2.0f);
        bull.gameObject.SetActive(false);
    }
}