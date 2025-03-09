using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour //オブジェクトの生成、非アクティブを操作と演出用のフラグ管理をする
{
    private Movement_Player movement_Player;
    private GameObject scoreManagerObj;
    private GameObject stageSystems;
    private int score;
    [HideInInspector] public static GameManager I;
    [HideInInspector] public bool isStartPerform = true, isOverPerform = false, isFinishPerform = false, isEndPhase = false, isFallen = false, isStart = true, isOver = true, poseFlag = false;
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
    [SerializeField] private StageInfo stageInfo;

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
        scoreManagerObj = stageSystems.transform.Find("ScoreManager")?.gameObject;
        
        // PerformManagerのインスタンス化
        performCamera = Instantiate(performCameraPrefab);

        // PerformManagerの演出開始フラグを立てる
        phaseManager.SetActive(false);
        scoreManagerObj.SetActive(false);
        isStartPerform = true;
    }

    void Start()
    {
        var stageInfo = UserData.I.GetCurrentStageInfo();
        themeColor = stageInfo.ThemeColor;
    }

    public void GameStart()
    {
        // PhaseManagerとScoreManagerのインスタンス化
        movement_Player.cantOperate = false;
        AudioManager.I.PlayBGM(BGM.Name.Stage_1);
        phaseManager.SetActive(true);
        scoreManagerObj.SetActive(true);
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
    }

    void Update()
    {
        if(isStart && !isStartPerform)
        {
            GameStart();
            poseFlag = true;
            isStart = false;
        }

        if(isOver)
        {
            // PerformManagerの終了演出開始
            if(isFallen || score < -4000)
            {
                isOverPerform = true;
                GameOver();
                Invoke("ResultAppearOver", 2f);
            }
            if(isEndPhase)
            {
                isFinishPerform = true;
                GameOver();
                Invoke("ResultAppearFinish", 2f);
            }
        }

        if(scoreManager != null)
        {
            score = scoreManager.GetScore();
        }
    }
    public void ResultAppearOver()
    {
        var result = Instantiate(resultCanvas);

        result.Initialize(score, "GameOver");
    }

    public void ResultAppearFinish()
    {
        var result = Instantiate(resultCanvas);

        result.Initialize(score, "Finish");
    }

    void GameOver()
    {
        isOver = false;
        poseFlag = false;
        phaseManager.SetActive(false);
        scoreManagerObj.SetActive(false);
    }
}