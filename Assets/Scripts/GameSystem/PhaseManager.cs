using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    private IGamePhase currentState; //現在のState
    public float gameTime = 0f; //ゲーム内経過時間
    public Transform[] spawnPoints; //敵のスポーン地点
    public Transform playerPosition; //プレイヤー地点

    private void Start()
    {
        SwitchState(new Phase1());
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        currentState.UpdateState(this);
    }

    public void SwitchState(IGamePhase newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public interface IGamePhase
    {
        void EnterState(PhaseManager manager);
        void UpdateState(PhaseManager manager);
    }

    public class Phase1 : IGamePhase
    {
        private float spawnInterval = 3f;
        private float elapsedTime = 0f;
        [SerializeField] private GameObject[] enemyPrefab;

        public void EnterState(PhaseManager manager)
        {
            elapsedTime = 0f;
        }

        public void UpdateState(PhaseManager manager)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= spawnInterval)
            {
                SpawnEnemy(manager);
                elapsedTime = 0f;
            }

            if (manager.gameTime > 20f)
            {
                //manager.SwitchState(new Phase2());
            }
        }

        public void SpawnEnemy(PhaseManager manager)
        {
            Transform spawnPoint = manager.spawnPoints[Random.Range(0, manager.spawnPoints.Length)];
            GameObject spawnEnemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            GameObject enemy = Instantiate(spawnEnemy, spawnPoint.position, Quaternion.identity);
            BullController bullCtr = enemy.GetComponent<BullController>();
            bullCtr.Initialize(spawnPoint.position, manager.playerPosition.position);
            //enemy.transform.rotation = Quaternion.LookRotation(bullCtr.direction);
        }
    }
}

