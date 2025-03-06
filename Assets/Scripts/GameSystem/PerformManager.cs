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
    private CanvasGroup startTextReady;
    private RectTransform startTextGoRt;
    private AnimationPlayer animationPlayer;
    private GameObject cameraObj;
    private GameManager gameManager; // GameManagerの参照
    private bool hasStarted = false; // 一度だけ実行するためのフラグ
    private bool movingFlag = false; // プレイヤーの移動中

    void Start()
    {
        cameraObj = GameObject.Find("Camera");
        Panel = GameObject.Find("Panel").GetComponent<CanvasGroup>();
        stageText = GameObject.Find("StageText").GetComponent<RectTransform>();
        startTextReady = GameObject.Find("StartTextReady (TMP)").GetComponent<CanvasGroup>();
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
            movingFlag = true;
        }

        if(movingFlag)
        {
            animationPlayer.MoveAnimation(0.06f);
        }
    }

    private async UniTask StartPerformance()
    {
        DOTweenHelper.LerpAsync(1f, 0f, 1f, Ease.InOutQuad, (value) => Panel.alpha = value);
        DOTweenHelper.LerpAsync(playerStartPos, playerEndPos, 5f, Ease.InOutQuad, (value) => gameManager.player.transform.position = value);

        //カメラの移動 & 回転
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(cameraStartPos, cameraEndPos, 3f, Ease.InOutQuad, (value) => cameraObj.transform.position = value),
            DOTweenHelper.LerpAsync(cameraStartRot, cameraEndRot, 3f, Ease.InOutQuad, (value) => cameraObj.transform.rotation = value)
        );

        await UniTask.Delay(1000);
        cameraObj.SetActive(false);
        await UniTask.Delay(750);
        movingFlag = false;
        animationPlayer.StopMoveAnimation();
        await UniTask.Delay(1250);
        
        // StageTextスライドイン & アウト
        await DOTweenHelper.LerpAsync(new Vector2(-800f, 0f), new Vector2(0f, 0f), 1.5f, Ease.InOutQuad, (value) => stageText.anchoredPosition = value);
        await UniTask.Delay(1000);
        await DOTweenHelper.LerpAsync(new Vector2(0f, 0f), new Vector2(800f, 0f), 1.5f, Ease.InOutQuad, (value) => stageText.anchoredPosition = value);

        // StartTextReadyが浮かび上がる
        await DOTweenHelper.LerpAsync(0f, 1f, 0.5f, Ease.InOutQuad, (value) => startTextReady.alpha = value);
        await UniTask.Delay(2000);
        startTextReady.alpha = 0f;

        // StartTextGoの拡大 & 透明度変化
        startTextGoRt.localScale = Vector3.one * 1.5f;
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 1f, 0.5f, Ease.InOutQuad, (value) => startTextGoCg.alpha = value),
            DOTweenHelper.LerpAsync(1.5f, 2f, 0.5f, Ease.OutBack, (value) => startTextGoRt.localScale = Vector3.one * value)
        );

        // 少し小さくなる
        DOTweenHelper.LerpAsync(2f, 1.5f, 0.5f, Ease.InOutQuad, (value) => startTextGoRt.localScale = Vector3.one * value);
        DOTweenHelper.LerpAsync(1f, 0f, 0.5f, Ease.InOutQuad, (value) => startTextGoCg.alpha = value);

        // UiCanvasのフェードイン
        uiCanvasCg.alpha = 0f;
        await DOTweenHelper.LerpAsync(0f, 1f, 1f, Ease.InOutQuad, (value) => uiCanvasCg.alpha = value);

        gameManager.isStartPerform = false;
    }
}