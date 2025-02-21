using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    [SerializeField] private float descendSpeed = 5.0f; // 等速で下降する速度
    [SerializeField] private float descendTime = 0.1f; // 下降する時間
    [SerializeField] private float waitTime = 0.5f; // 自由落下までの待機時間
    [SerializeField] private float accel = 9.8f; // 下方向の加速度

    private bool isTrigger = false;
    private bool isAccel = false;
    private bool isWait = false;
    private float timer = 0.0f;
    private float velocity = 0.0f;

    void Update()
    {
        if (isTrigger)
        {
            if (!isAccel)
            {
                if (!isWait)
                {
                    // 等速で下降
                    transform.position += Vector3.down * descendSpeed * Time.deltaTime;
                    timer += Time.deltaTime;

                    // 一定時間後に停止
                    if (timer >= descendTime)
                    {
                        isWait = true;
                        timer = 0.0f; // タイマーをリセット
                    }
                }
                else
                {
                    timer += Time.deltaTime; // 停止時間を経過したら自由落下
                    if (timer >= waitTime)
                    {
                        isAccel = true;
                        velocity = descendSpeed; // 現在の速度を初速に
                    }
                }
            }
            else
            {
                velocity += accel * Time.deltaTime;
                transform.position += Vector3.down * velocity * Time.deltaTime; // 自由落下
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTrigger && collision.gameObject.CompareTag("Player"))
        {
            isTrigger = true; // プレイヤーが接触したら開始
        }
    }
}
