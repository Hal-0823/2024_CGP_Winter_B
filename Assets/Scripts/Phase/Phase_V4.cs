using UnityEngine;

/// <summary>
/// 以下を同時に行う
/// 円周上を時計回りに１つ飛ばしで闘牛を発生させる
/// 円周上を反時計回りに１つ飛ばしで闘牛を発生させる
/// スポーンを終えると次のフェーズへ移行する
/// </summary>
public class Phase_V4 : PhaseBase
{
    private int spawnId_v1;
    private int spawnId_v3;

    void Start()
    {
        spawnInterval = 0.9f;
        phaseDuration = 12f;
    }

    public override void EnterState(PhaseManager manager)
    {
        spawnId_v1 = 0;
        spawnId_v3 = manager.SpawnPoints.Length - manager.SpawnPoints.Length/2;
        base.EnterState(manager);
    }

    public override void SpawnEnemy(PhaseManager manager)
    {
        spawnId_v1 = SpawnEnemy_V1(manager, spawnId_v1);
        spawnId_v3 = SpawnEnemy_V1(manager, spawnId_v3);
    }

    private int SpawnEnemy_V1(PhaseManager manager, int spawnId)
    {
        int targetId = spawnId + manager.SpawnPoints.Length/2 - 2;
        while (spawnId < 0)
        {
            spawnId += manager.SpawnPoints.Length;
        }
        spawnId = spawnId%(manager.SpawnPoints.Length);
        

        // 余りが負になるのを避ける
        while (targetId < 0)
        {
            targetId += manager.SpawnPoints.Length;
        }
        targetId = targetId%(manager.SpawnPoints.Length);

        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length-1)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();

        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        return spawnId + 2;
    }

    private int SpawnEnemy_V3(PhaseManager manager, int spawnId)
    {
        int targetId = spawnId - manager.SpawnPoints.Length/2 + 2;

        while (spawnId < 0)
        {
            spawnId += manager.SpawnPoints.Length;
        }
        spawnId = spawnId%(manager.SpawnPoints.Length);
        

        // 余りが負になるのを避ける
        while (targetId < 0)
        {
            targetId += manager.SpawnPoints.Length;
        }
        targetId = targetId%(manager.SpawnPoints.Length);

        Debug.Log(targetId);

        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length-1)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();

        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        return spawnId - 2;
    }
}
