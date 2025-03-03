using UnityEngine;

/// <summary>
/// 円周上を反時計回りに１つ飛ばしで闘牛を発生させる
/// スポーンを終えると次のフェーズへ移行する
/// </summary>
public class Phase_V3 : PhaseBase
{
    private int spawnId;

    void Start()
    {
        spawnInterval = 0.9f;
    }

    public override void EnterState(PhaseManager manager)
    {
        spawnId = manager.SpawnPoints.Length - 1;
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
        if (spawnId <= 0)
        {
            manager.NextState();
            return;
        }

        int targetId = spawnId - manager.SpawnPoints.Length/2 + 2;

        // 余りが負になるのを避ける
        while (targetId < 0)
        {
            targetId += manager.SpawnPoints.Length-1;
        }

        targetId = targetId%(manager.SpawnPoints.Length);
        Debug.Log(targetId);

        GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length-1)];
        GameObject enemy = Instantiate(spawnEnemy);
        BullController bullCtr = enemy.GetComponent<BullController>();

        bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        spawnId -= 2;
    }
}
