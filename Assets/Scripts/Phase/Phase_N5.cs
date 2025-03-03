using UnityEngine;

/// <summary>
/// 出現ペースが遅いフェーズ
/// プレイヤー狙いと通常スポーンが混在する
/// </summary>
public class Phase_N5 : PhaseBase
{
    void Start()
    {
        phaseDuration = 20f;
        spawnInterval = 6f;
    }

    public override void SpawnEnemy(PhaseManager manager)
    {
        int spawnId = Random.Range(0,manager.SpawnPoints.Length);

        GameObject spawnEnemyPref = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemyPref);
        BullController bullCtr = enemy.GetComponent<BullController>();

        // スポーンIdが偶数ならプレイヤーを狙う
        if (spawnId%2 == 0)
        {
            bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.Player.position);
        }
        else
        {
            int targetId = spawnId + manager.SpawnPoints.Length/2 + Random.Range(-1, 1);
            targetId = targetId%(manager.SpawnPoints.Length-1);
            bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        }
    }
}