using UnityEngine;

/// <summary>
/// 闘牛の動きを制御する基底クラス
/// 全ての闘牛に共通する移動処理を記述
/// </summary>
public class BullController : MonoBehaviour
{
    const int ENEMY_LAYER = 6;      // 通常の敵レイヤー
    const int SPAWN_LAYER = 7;      // スポーン直後の、壁に接触しないレイヤー

    [Tooltip("壁に当たったときのイベント")]
    public OnHitWallAction onHitWallAction;
    public float Speed = 5f;
    protected Vector3 direction;    // 闘牛の進行方向
    private int bounceCount;        // 反射した回数
    private ParticleSystem runParticle;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="spawnPosition">スポーンする位置</param>
    /// <param name="targetPosition">ターゲットの位置（プレイヤーや出口のゲート）</param>
    /// <param name="isSpawn">スポーンによる生成か（分裂による生成ではない）</param>
    public virtual void Initialize(Vector3 spawnPosition, Vector3 targetPosition, bool isSpawn = true)
    {
        // スポーンによる生成ならスポーンレイヤーに変更
        if (isSpawn)
        {
            this.gameObject.layer = SPAWN_LAYER;
        }
        else
        {
            this.gameObject.layer = ENEMY_LAYER;
        }
        

        transform.position = spawnPosition;
        targetPosition.y = spawnPosition.y;
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

    /// <summary>
    /// 壁との衝突を判定する
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            onHitWallAction?.Execute(this, collision);
        }
    }

    /// <summary>
    /// スポーン直後のスポーンエリアを抜け出したか判定する
    /// </summary>
    /// <param name="col"></param>
    public void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("SpawnArea"))
        {
            Debug.Log("スポーンレイヤーから脱出");
            this.gameObject.layer = ENEMY_LAYER;
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

    public int GetBounceCount()
    {
        return bounceCount;
    }

    public void AddBounceCount()
    {
        bounceCount++;
    }
}
