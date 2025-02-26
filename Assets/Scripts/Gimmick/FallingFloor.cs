using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingFloor : MonoBehaviour
{
    [SerializeField] private float descendSpeed = 5.0f; // 等速で下降する速度
    [SerializeField] private float firstDescendRange = 0.1f; // 初手の下降距離
    [SerializeField] private float waitTime = 0.5f; // 自由落下までの待機時間
    [SerializeField] private float accel = 9.8f; // 下方向の加速度
    [SerializeField] private float repopTime = 12.0f; // 再び床が生成されるまでの時間 

    private bool isTrigger = false;
    private bool isAccel = false;
    private bool isWait = false;
    private float timer = 0.0f;
    private float velocity = 0.0f;
    private Vector3 originalPos; // 元の場所
    private Vector3 originalScale; // 元の大きさ

    void Start()
    {
        originalPos = transform.position;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isTrigger)
        {
            if (!isAccel)
            {
                if (!isWait)
                {
                    transform.position = Vector3.Lerp(originalPos, originalPos + Vector3.down * firstDescendRange, timer * 2);
                    timer += Time.deltaTime;

                    // 一定時間後に停止
                    if (timer * 2 >= 1.0f)
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

                timer += Time.deltaTime;
                if (timer > repopTime)
                {
                    StartCoroutine("Repop");
                }
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

    /// <summary>
    /// 再生成
    /// </summary>
    /// <returns></returns>
    private IEnumerator Repop()
    {
        this.transform.localScale = Vector3.zero;
        this.transform.position = originalPos;
        isTrigger = false;
        isAccel = false;
        isWait = false;

        for (int i=1; i<=10; i++)
        {
            this.transform.localScale = originalScale * (float)i / 10;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
