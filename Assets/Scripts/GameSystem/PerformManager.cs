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
    private CanvasGroup PanelCg;
    private Image PanelIm;
    private CanvasGroup startTextGoCg;
    private CanvasGroup uiCanvasCg;
    private CanvasGroup startTextReady;
    private RectTransform startTextGoRt;
    private AnimationPlayer animationPlayer;
    private GameObject cameraObj;
    private GameManager gameManager; // GameManagerの参照
    private bool hasStarted = false; // 一度だけ実行するためのフラグ
    private bool movingFlag = false; // プレイヤーの移動中

    void Start()
    {
        GameObject panel = GameObject.Find("StartPanel");
        PanelCg = panel.GetComponent<CanvasGroup>();
        PanelIm = panel.GetComponent<Image>();
        animationPlayer = GameManager.I.player.GetComponent<AnimationPlayer>();
        cameraObj = GameManager.I.performCamera;
    }

    void Update()
    {
        if (GameManager.I != null && GameManager.I.isStartPerform && !hasStarted)
        {
            hasStarted = true;
            StartPerformance().Forget();
            StartSE().Forget();
            movingFlag = true;
        }

        if(movingFlag)
        {
            animationPlayer.MoveAnimation(0.06f);
        }

        if (GameManager.I != null && GameManager.I.isOverPerform && hasStarted)
        {
            hasStarted = false;
            OverPerformance().Forget();
        }

        if (GameManager.I != null && GameManager.I.isFinishPerform && hasStarted)
        {
            hasStarted = false;
            FinishPerformance().Forget();
        }
    }

    private async UniTask StartSE()
    {
        await UniTask.Delay(1000);
        AudioManager.I.gameObject.SetActive(true);
        AudioManager.I.PlaySE(SE.Name.ExcellentReaction);
        await UniTask.Delay(5000);
        AudioManager.I.PlaySE(SE.Name.Slide);
        await UniTask.Delay(2500);
        AudioManager.I.PlaySE(SE.Name.Start);
    }

    private async UniTask OverPerformance()
    {
        AudioManager.I.PlaySE(SE.Name.Finish);
        await UniTask.Delay(2000);
        await DOTweenHelper.LerpAsync(1f, 0f, 1f, Ease.InOutQuad, (value) => uiCanvasCg.alpha = value);
    }

    private async UniTask FinishPerformance()
    {
        AudioManager.I.PlaySE(SE.Name.Finish);
        await UniTask.Delay(2000);
        await DOTweenHelper.LerpAsync(1f, 0f, 1f, Ease.InOutQuad, (value) => uiCanvasCg.alpha = value);
    }

    private async UniTask StartPerformance()
    {
        PanelIm.color = GameManager.I.themeColor;
        DOTweenHelper.LerpAsync(1f, 0f, 1.5f, Ease.InOutQuad, (value) => PanelCg.alpha = value);
        DOTweenHelper.LerpAsync(playerStartPos, playerEndPos, 5f, Ease.InOutQuad, (value) => GameManager.I.player.transform.position = value);

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
        startTextReady = GameObject.Find("StartTextReady").GetComponent<CanvasGroup>();
        startTextGoRt = GameObject.Find("StartTextGo").GetComponent<RectTransform>();
        startTextGoCg = GameObject.Find("StartTextGo").GetComponent<CanvasGroup>();
        uiCanvasCg = GameManager.I.uiCanvas.GetComponent<CanvasGroup>();
        await UniTask.Delay(1250);

        // StartTextReadyが浮かび上がる
        await DOTweenHelper.LerpAsync(0f, 1f, 0.5f, Ease.InOutQuad, (value) => startTextReady.alpha = value);
        await UniTask.Delay(2000);
        startTextReady.alpha = 0f;

        // StartTextGoの拡大 & 透明度変化
        startTextGoRt.localScale = Vector3.one * 1.5f;
        await UniTask.WhenAll(
            DOTweenHelper.LerpAsync(0f, 1f, 0.5f, Ease.InOutQuad, (value) => startTextGoCg.alpha = value),
            DOTweenHelper.LerpAsync(3.5f, 4f, 0.5f, Ease.OutBack, (value) => startTextGoRt.localScale = Vector3.one * value)
        );
        await UniTask.Delay(1000);

        // 少し小さくなる
        DOTweenHelper.LerpAsync(4f, 3f, 0.3f, Ease.InOutQuad, (value) => startTextGoRt.localScale = Vector3.one * value);
        DOTweenHelper.LerpAsync(1f, 0f, 0.3f, Ease.InOutQuad, (value) => startTextGoCg.alpha = value);

        // UiCanvasのフェードイン
        uiCanvasCg.alpha = 0f;
        await DOTweenHelper.LerpAsync(0f, 1f, 1f, Ease.InOutQuad, (value) => uiCanvasCg.alpha = value);

        GameManager.I.isStartPerform = false;
    }


}