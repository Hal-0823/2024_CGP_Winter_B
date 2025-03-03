using UnityEngine;

/// <summary>
/// 最もシンプルなフェーズ
/// </summary>
public class Phase_N1 : PhaseBase
{
    void Start()
    {
        phaseDuration = 15f;
        spawnInterval = 3f;
    }

    public override void SpawnEnemy(PhaseManager manager)
    {
        // スポーン地点のインデックス
        int spawnId = Random.Range(0, manager.SpawnPoints.Length);
        // 目標地点のインデックス（スポーン地点と反対側にある点のインデックス±1)
        int targetId = spawnId + manager.SpawnPoints.Length/2 + Random.Range(-1, 1);
        targetId = targetId%(manager.SpawnPoints.Length-1);

        // 出現する敵を抽選で選んで生成
        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();
        
        // 初期化処理(スポーン位置と、目的地を設定)
        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
    }
}