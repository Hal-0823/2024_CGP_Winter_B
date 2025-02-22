using UnityEngine;

/// <summary>
/// 闘牛の動きを制御する基底クラス
/// 全ての闘牛に共通する移動処理を記述
/// </summary>
public class BullController : MonoBehaviour
{
    [Tooltip("壁に当たったときにどうなるか")]
    public OnHitWallAction onHitWallAction;
    public float Speed = 5f;
    protected Vector3 direction;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="spawnPosition">スポーンする位置</param>
    /// <param name="targetPosition">ターゲットの位置（プレイヤーや出口のゲート）</param>
    public virtual void Initialize(Vector3 spawnPosition, Vector3 targetPosition)
    {
        transform.position = spawnPosition;
        direction = (targetPosition - spawnPosition).normalized;

        //　闘牛を進行方向に向ける
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    protected virtual void Move()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            onHitWallAction?.Execute(this, collision);
        }
    }

    public Vector3 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector3 direction)
    {
        var n_dir = direction.normalized;
        this.direction = n_dir;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
