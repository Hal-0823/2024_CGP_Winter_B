using UnityEngine;

[CreateAssetMenu(menuName="OnHitWallActions/Bounce")]
public class BounceAction : OnHitWallAction
{
    [Tooltip("反射回数の上限")]
    public int maxBounces = 1;
    [Tooltip("反射回数が上限を超えたときのイベント")]
    public OnHitWallAction endBounceAction;

    public override void Execute(BullController bull, Collision col)
    {
        // 反射上限を超えたらイベントを実行
        if (bull.GetBounceCount() == maxBounces)
        {
            endBounceAction?.Execute(bull, col);
            return;
        }

        Rigidbody rb = bull.GetComponent<Rigidbody>();
        
        Vector3 reflectDir = Vector3.Reflect(bull.GetDirection(), col.contacts[0].normal);
        reflectDir.y = 0;
        bull.SetDirection(reflectDir);

        bull.AddBounceCount();
    }
}