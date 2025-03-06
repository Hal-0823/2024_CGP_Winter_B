using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 指定したとおりに同時にスポーンさせる
/// </summary>
public class Phase_V2 : PhaseBase
{
    public List<Root> Roots = new List<Root>();
    private int spawnId;

    void Start()
    {
        spawnInterval = 1.0f;
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
        foreach (Root root in Roots)
        {
            GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length-1)];
            GameObject enemy = Instantiate(spawnEnemy);
            BullController bullCtr = enemy.GetComponent<BullController>();
            int spawnId = root.SpawnId%(manager.SpawnPoints.Length);
            int targetId = root.TargetId%(manager.SpawnPoints.Length);

            bullCtr.Initialize(manager.SpawnPoints[spawnId].position, manager.SpawnPoints[targetId].position);
        }

        manager.NextState();
    }

    [System.Serializable]
    public struct Root
    {
        public int SpawnId;
        public int TargetId;
    }
}
