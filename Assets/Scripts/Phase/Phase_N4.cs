using UnityEngine;

/// <summary>
/// 二か所から出現するフェーズ
/// </summary>
public class Phase_N4 : PhaseBase
{
    void Start()
    {
        phaseDuration = 20f;
        spawnInterval = 3.9f;
    }

    public override void SpawnEnemy(PhaseManager manager)
    {
        int spawnId = Random.Range(0,manager.SpawnPoints.Length);
        int targetId = spawnId + manager.SpawnPoints.Length/2 + Random.Range(-2, 2);
        targetId = targetId%(manager.SpawnPoints.Length);

        GameObject spawnEnemyPref = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        GameObject enemy = Instantiate(spawnEnemyPref);
        BullController bullCtr = enemy.GetComponent<BullController>();
        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);

        // ２つ目のスポーンポイントの位置を決定
        spawnId += Random.Range(4, manager.SpawnPoints.Length - 4);
        spawnId = spawnId%(manager.SpawnPoints.Length);
        targetId = spawnId + manager.SpawnPoints.Length/2 + Random.Range(-2, 2);
        targetId = targetId%(manager.SpawnPoints.Length);

        spawnEnemyPref = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        enemy = Instantiate(spawnEnemyPref);
        bullCtr = enemy.GetComponent<BullController>();
        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
    }
}