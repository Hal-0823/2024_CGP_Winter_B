using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour //オブジェクトの生成、非アクティブを操作と演出用のフラグ管理をする
{
    private Movement_Player movement_Player;
    private GameObject scoreManagerObj;
    private GameObject stageSystems;
    private bool isOver = true;
    private int score;
    [HideInInspector] public static GameManager I;
    [HideInInspector] public bool isStartPerform = true, isOverPerform = false, isFinishPerform = false, isEndPhase = false, isFallen = false, isStart = true;
    [HideInInspector] public Color themeColor;
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject uiCanvas;
    [HideInInspector] public GameObject performCamera;
    [HideInInspector] public AnimationPlayer animationPlayer;
    [HideInInspector] public ScoreManager scoreManager;
    [SerializeField] private GameObject stageSystemsPrefab;
    [SerializeField] private GameObject phaseManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject performCameraPrefab;
    [SerializeField] private Result resultCanvas;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // プレイヤーのインスタンス化とスクリプトの取得
        player = Instantiate(playerPrefab, new Vector3(0f, 1.1f, -7f), Quaternion.identity);
        animationPlayer = player.GetComponent<AnimationPlayer>();
        movement_Player = player.GetComponent<Movement_Player>();
        movement_Player.cantOperate = true;

        // キャンバスの表示
        stageSystems = Instantiate(stageSystemsPrefab);
        uiCanvas = stageSystems.transform.Find("StageCanvas")?.gameObject;
        
        // PerformManagerのインスタンス化
        performCamera = Instantiate(performCameraPrefab);

        // PerformManagerの演出開始フラグを立てる
        phaseManager.SetActive(false);
        AudioManager.I.gameObject.SetActive(false);
        isStartPerform = true;
    }

    public void GameStart()
    {
        // PhaseManagerとScoreManagerのインスタンス化
        movement_Player.cantOperate = false;
        AudioManager.I.PlayBGM(BGM.Name.Stage_1);
        phaseManager.SetActive(true);
        scoreManagerObj = stageSystems.transform.Find("StageCanvas")?.gameObject;
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
    }

    void Update()
    {
        if(isStart && !isStartPerform)
        {
            GameStart();
            isStart = false;
        }

        if(isOver && isFallen)
        {
            // PerformManagerの終了演出開始
            isOver = false;
            isOverPerform = true;
            Invoke("GameOver", 0.1f);
            phaseManager.SetActive(false);
            scoreManagerObj.SetActive(false);
            Time.timeScale = 0.1f;
        }

        if(isOver && isEndPhase)
        {
            // PerformManagerの終了演出開始
            isOver = false;
            isFinishPerform = true;
            score = scoreManager.score;
            Invoke("GameOver", 0.1f);
            phaseManager.SetActive(false);
            scoreManagerObj.SetActive(false);
            Time.timeScale = 0.1f;
        }
    }
    public void GameOver()
    {
        var result = Instantiate(resultCanvas);
        result.Initialize(score, "GameOver");
        Time.timeScale = 0f;
    }
}