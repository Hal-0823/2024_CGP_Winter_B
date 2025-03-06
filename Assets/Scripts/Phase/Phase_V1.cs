using UnityEngine;

/// <summary>
/// 円周上を時計回りに１つ飛ばしで闘牛を発生させる
/// スポーンを終えると次のフェーズへ移行する
/// </summary>
public class Phase_V1 : PhaseBase
{
    private int spawnId;

    void Start()
    {
        spawnInterval = 0.9f;
    }

    public override void EnterState(PhaseManager manager)
    {
        spawnId = 0;
        base.EnterState(manager);
    }

    public override void UpdateState(PhaseManager manager)
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy(manager);
            spawnTimer = 0f;
        }
    }

    public override void SpawnEnemy(PhaseManager manager)
    {
        if (spawnId >= manager.SpawnPoints.Length)
        {
            manager.NextState();
            return;
        }

        int targetId = spawnId + manager.SpawnPoints.Length/2 - 2;
        targetId = targetId%(manager.SpawnPoints.Length);

        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length-1)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();

        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        spawnId += 2;
    }
}
