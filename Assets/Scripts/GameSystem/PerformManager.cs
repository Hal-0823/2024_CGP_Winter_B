using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class PerformManager : MonoBehaviour
{
    [SerializeField] private Vector3 cameraStartPos;
    [SerializeField] private Vector3 cameraEndPos;
    [SerializeField] private Vector3 cameraGamePos;
    [SerializeField] private Quaternion cameraStartRot;
    [SerializeField] private Quaternion cameraEndRot;
    [SerializeField] private Quaternion cameraGameRot;
    [SerializeField] private Vector3 playerStartPos;
    [SerializeField] private Vector3 playerEndPos;
    private CanvasGroup Panel;
    private CanvasGroup startTextGoCg;
    private CanvasGroup uiCanvasCg;
    private RectTransform stageText;
    private RectTransform startTextReady;
    private RectTransform startTextGoRt;
    private AnimationPlayer animationPlayer;
    private GameObject cameraObj;
    private GameManager gameManager; // GameManagerの参照
    private bool hasStarted = false; // 一度だけ実行するためのフラグ
    private bool movingFlag = false; // プレイヤーの移動中

    void Start()
    {
        Camera camera = Camera.main;
        cameraObj = camera.gameObject;
        Panel = GameObject.Find("Panel").GetComponent<CanvasGroup>();
        stageText = GameObject.Find("StageText").GetComponent<RectTransform>();
        startTextReady = GameObject.Find("StartTextReady (TMP)").GetComponent<RectTransform>();
        startTextGoRt = GameObject.Find("StartTextGo (TMP)").GetComponent<RectTransform>();
        startTextGoCg = GameObject.Find("StartTextGo (TMP)").GetComponent<CanvasGroup>();
        gameManager = FindAnyObjectByType<GameManager>();
        animationPlayer = gameManager.player.GetComponent<AnimationPlayer>();
        uiCanvasCg = gameManager.uiCanvas.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.isStartPerform && !hasStarted)
        {
            hasStarted = true;
            StartPerformance().Forget();
        }
    }

    private async UniTask StartPerformance()
    {
        //カメラの移動 & 回転
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(1f, 0f, 0.5f, Ease.InOutQuad, (value) => Panel.alpha = value),
            DOTweenHelper.LerpAsync(cameraStartPos, cameraEndPos, 3f, Ease.InOutQuad, (value) => cameraObj.transform.position = value),
            DOTweenHelper.LerpAsync(cameraStartRot, cameraEndRot, 3f, Ease.InOutQuad, (value) => cameraObj.transform.rotation = value)
        );

        cameraObj.transform.position = cameraGamePos;
        cameraObj.transform.rotation = cameraGameRot;

        // プレイヤーの移動 (アニメーション再生)
        animationPlayer.MoveAnimation(0.06f);
        await DOTweenHelper.LerpAsync(playerStartPos, playerEndPos, 2f, Ease.InOutQuad, (value) => gameManager.player.transform.position = value);
        
        // StageTextスライドイン & アウト
        await DOTweenHelper.LerpAsync(new Vector2(-800f, 0f), new Vector2(0f, 0f), 1.5f, Ease.InOutQuad, (value) => stageText.anchoredPosition = value);
        await UniTask.Delay(1000);
        await DOTweenHelper.LerpAsync(new Vector2(0f, 0f), new Vector2(800f, 0f), 1.5f, Ease.InOutQuad, (value) => stageText.anchoredPosition = value);

        // StartTextReadyが浮かび上がる
        startTextReady.GetComponent<CanvasGroup>().alpha = 0f;
        await DOTweenHelper.LerpAsync(0f, 1f, 1f, Ease.InOutQuad, (value) => startTextReady.GetComponent<CanvasGroup>().alpha = value);

        // StartTextGoの拡大 & 透明度変化
        startTextGoCg.alpha = 0f;
        startTextGoRt.localScale = Vector3.one * 0.5f;
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 1f, 1f, Ease.InOutQuad, (value) => startTextGoCg.alpha = value),
            DOTweenHelper.LerpAsync(0.5f, 1.2f, 1f, Ease.OutBack, (value) => startTextGoRt.localScale = Vector3.one * value)
        );

        // 少し小さくなる
        await DOTweenHelper.LerpAsync(1.2f, 1f, 0.5f, Ease.InOutQuad, (value) => startTextGoRt.localScale = Vector3.one * value);

        // UiCanvasのフェードイン
        uiCanvasCg.alpha = 0f;
        await DOTweenHelper.LerpAsync(0f, 1f, 1f, Ease.InOutQuad, (value) => uiCanvasCg.alpha = value);

        gameManager.isStartPerform = false;
    }
}