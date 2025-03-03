using UnityEngine;

public abstract class PhaseBase : MonoBehaviour, IGamePhase
{
    protected float phaseDuration = 15f;  // フェーズの継続時間
    protected float spawnInterval = 3f;   // スポーンの間隔
    protected float spawnTimer = 0f; // スポーン用のタイマー
    protected float phaseStartTime;   // フェーズが開始したときの時間
    [SerializeField] protected GameObject[] enemyPrefab;

    public virtual void EnterState(PhaseManager manager)
    {
        spawnTimer = 0f;

        // EnterStateした時点での時間を代入
        phaseStartTime += manager.GameTime;
    }

    public virtual void UpdateState(PhaseManager manager)
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy(manager);
            spawnTimer = 0f;
        }

        if (manager.GameTime - phaseStartTime > phaseDuration)
        {
            manager.NextState();
        }
    }

    public abstract void SpawnEnemy(PhaseManager manager);
}