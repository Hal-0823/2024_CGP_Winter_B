using UnityEngine;

public class Phase1 : MonoBehaviour, IGamePhase
{
    private float phaseDuration = 20f;  // フェーズの継続時間
    private float spawnInterval = 3f;   // スポーンの間隔
    private float spawnTimer = 0f; // スポーン用のタイマー
    private float phaseStartTime;   // フェーズが開始したときの時間
    [SerializeField] private GameObject[] enemyPrefab;

    public void EnterState(PhaseManager manager)
    {
        spawnTimer = 0f;

        // EnterStateした時点での時間を代入
        phaseStartTime += manager.gameTime;
    }

    public void UpdateState(PhaseManager manager)
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy(manager);
            spawnTimer = 0f;
        }

        if (manager.gameTime - phaseStartTime > phaseDuration)
        {
            manager.NextState();
        }
    }

    public void SpawnEnemy(PhaseManager manager)
    {
        // スポーン地点のインデックス
        int spawnId = Random.Range(0, manager.spawnPoints.Length);
        // 目標地点のインデックス（スポーン地点と反対側にある点のインデックス±1)
        int targetId = spawnId + manager.spawnPoints.Length/2 + Random.Range(-1, 1);
        targetId = targetId%(manager.spawnPoints.Length-1);

        // 出現する敵を抽選で選んで生成
        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();
        
        // 初期化処理(スポーン位置と、目的地を設定)
        bullCtr.Initialize(manager.spawnPoints[spawnId].position, manager.spawnPoints[targetId].position);
    }
}