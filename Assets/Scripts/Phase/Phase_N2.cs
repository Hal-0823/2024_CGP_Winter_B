using UnityEngine;

public class Phase_N2 : MonoBehaviour, IGamePhase
{
    private float phaseDuration = 15f;  // フェーズの継続時間
    private float spawnInterval = 3f;   // スポーンの間隔
    private float spawnTimer = 0f; // スポーン用のタイマー
    private float phaseStartTime;   // フェーズが開始したときの時間
    [SerializeField] private GameObject[] enemyPrefab;

    public void EnterState(PhaseManager manager)
    {
        spawnTimer = 0f;

        // EnterStateした時点での時間を代入
        phaseStartTime += manager.GameTime;
    }

    public void UpdateState(PhaseManager manager)
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

    public void SpawnEnemy(PhaseManager manager)
    {
        // スポーン地点のインデックス
        int spawnId = Random.Range(0, manager.SpawnPoints.Length);

        // 出現する敵を抽選で選んで生成
        GameObject spawnEnemyPref = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemyPref);
        BullController bullCtr = enemy.GetComponent<BullController>();
        
        // 初期化処理(スポーン位置と、目的地を設定)
        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.Player.position);
    }
}