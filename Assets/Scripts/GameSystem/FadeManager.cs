using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーン遷移とフェードの処理を行うクラス
/// </summary>
public class FadeManager : MonoBehaviour
{
    public static FadeManager I;
    public float FadeDuration = 0.25f;
    public float LoadDuration = 2.0f;
    [SerializeField] CanvasGroup fadeCanvas;
    [SerializeField] Image fadePanel;
    [SerializeField] Image loadingImage;
    private Color fadeColor;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        loadingImage.enabled = false;
        fadeColor = Color.black;
        fadePanel.color = fadeColor;
        fadeCanvas.alpha = 1; // 初期状態は黒画面
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float timer = FadeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            fadeCanvas.alpha = timer / FadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 0;
        fadeCanvas.blocksRaycasts = false;
    }

    public IEnumerator FadeOut()
    {
        fadeCanvas.blocksRaycasts = true;
        float timer = 0;
        while (timer < FadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvas.alpha = timer / FadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 1;
    }

    /// <summary>
    /// シーン遷移を行うメソッド
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    /// <param name="sceneName">遷移先のシーン名</param>
    /// <param name="duration">ロード画面の表示時間</param>
    public void LoadSceneWithFade(string sceneName, float duration)
    {
        LoadDuration = duration;
        StartCoroutine(Transition(sceneName));
    }

    /// <param name="sceneName">遷移先のシーン名</param>
    /// <param name="color">ロード画面の色</param>
    /// <param name="duration">ロード画面の表示時間</param>
    public void LoadSceneWithFade(string sceneName, Color color, float duration)
    {
        fadeColor = color;
        LoadDuration = duration;
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        fadePanel.color = fadeColor;
        yield return StartCoroutine(FadeOut());
        
        if (LoadDuration > 0.2f)
        {
            loadingImage.enabled = true;
            yield return new WaitForSeconds(LoadDuration - 0.2f); // ロード画面表示時間
            loadingImage.enabled = false;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        yield return new WaitForSeconds(0.2f); // ロード画面表示時間
        StartCoroutine(FadeIn());
        
    }
}
