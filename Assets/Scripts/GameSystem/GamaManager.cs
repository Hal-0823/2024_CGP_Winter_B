using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour //オブジェクトの生成、非アクティブを操作と演出用のフラグ管理をする
{
    public bool isStartPerform = true;
    public bool isEndPerform;
    public GameObject player;
    public GameObject uiCanvas;
    public StatementPlayer statementPlayer;
    public AnimationPlayer animationPlayer;
    public Movement_Player movement_Player;
    private GameObject phaseManager;
    private GameObject scoreManager;
    private bool isStart = true;
    [SerializeField] private GameObject uiCanvasPrefab;
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private GameObject performManagerPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject phaseManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;

    void Awake()
    {
        // プレイヤーのインスタンス化とスクリプトの取得
        player = Instantiate(playerPrefab);
        //statementPlayer = player.GetComponent<StatementPlayer>();
        animationPlayer = player.GetComponent<AnimationPlayer>();
        movement_Player = player.GetComponent<Movement_Player>();
        movement_Player.cantOperate = true;

        // キャンバスの表示
        uiCanvas = Instantiate(uiCanvasPrefab);
        Instantiate(startCanvas);
        Instantiate(endCanvas);

        // PerformManagerのインスタンス化
        Instantiate(performManagerPrefab);

        // PerformManagerの演出開始フラグを立てる
        isStartPerform = true;
    }

    public void GameStart()
    {
        // PhaseManagerとScoreManagerのインスタンス化
        phaseManager = Instantiate(phaseManagerPrefab);
        scoreManager = Instantiate(scoreManagerPrefab);
    }

    void Update()
    {
        if(!isStartPerform && isStart)
        {
            GameStart();
            isStart = false;
        }
    }
    public void GameOver()
    {
        // PerformManagerの終了演出開始
        isEndPerform = true;

        // UI・PhaseManager・ScoreManagerを非アクティブ化
        uiCanvas.SetActive(false);
        phaseManager.SetActive(false);
        scoreManager.SetActive(false);
    }
}