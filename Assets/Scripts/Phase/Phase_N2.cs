using UnityEngine;

/// <summary>
/// プレイヤーがいる方向に向けてスポーンするフェーズ
/// </summary>
public class Phase_N2 : PhaseBase
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

        // 出現する敵を抽選で選んで生成
        GameObject spawnEnemyPref = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemyPref);
        BullController bullCtr = enemy.GetComponent<BullController>();
        
        // 初期化処理(スポーン位置と、目的地を設定)
        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.Player.position);
    }
}