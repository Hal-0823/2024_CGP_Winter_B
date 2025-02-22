using UnityEngine;

[CreateAssetMenu(menuName="OnHitWallActions/Bounce")]
public class BounceAction : OnHitWallAction
{
    [Tooltip("反射回数の上限")]
    public int maxBounces = 1;

    public override void Execute(BullController bull, Collision col)
    {
        Rigidbody rb = bull.GetComponent<Rigidbody>();
        
        // enemy.CurrentBouces++

        Debug.Log($"元の向き{bull.GetDirection()}");
        Vector3 reflectDir = Vector3.Reflect(bull.GetDirection(), col.contacts[0].normal);
        reflectDir.y = 0;
        bull.SetDirection(reflectDir);

        Debug.Log($"反射後の向き{bull.GetDirection()}");
    }
}