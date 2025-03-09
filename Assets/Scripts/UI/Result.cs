using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// リザルト画面の更新
/// </summary>
public class Result : MonoBehaviour
{
    public Star[] Stars;
    public TextMeshProUGUI ScoreText;
    public Voltage Voltage;
    public float FillSpeed = 6000f;
    private int currentStarIndex = 0;
    private float targetScore = 0f;
    private float currentScore = 0f;

    public void Initialize(int finalScore, string anim)
    {
        Voltage.Initialize();
        targetScore = finalScore;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(anim);

        SaveResult(finalScore);
    }

    private void SaveResult(int finalScore)
    {
        int star = 0;
        for ( ; finalScore >= Voltage.GetBorderScore(star); star++);
        
        var stageInfo = UserData.I.GetCurrentStageInfo();
        UserData.I.SaveStageResult(stageInfo.StageIndex, finalScore, star);
    }

    /// <summary>
    /// AnimationのShowが終わると呼ばれる
    /// </summary>
    public void FinishShowAnimation()
    {
        StartFilling();
    }

    public void StartFilling()
    {
        StartCoroutine(FillGauge());
    }

    private IEnumerator FillGauge()
    {
        while (currentScore < targetScore)
        {
            currentScore += FillSpeed * Time.deltaTime;
            currentScore = Mathf.Min(currentScore, targetScore);

            Voltage.Slider.value = currentScore;

            // ボーダーを超えたら星を順番に点灯させる
            if (currentStarIndex < Stars.Length && currentScore >= Voltage.GetBorderScore(currentStarIndex))
            {
                Stars[currentStarIndex].SetState(true);
                currentStarIndex++;
            }

            ScoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();

            yield return null;
        }
    }

    public void OnClickTitle()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        FadeManager.I.LoadSceneWithFade("Title");
    }

    public void OnClickRetry()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        var stageInfo = UserData.I.GetCurrentStageInfo();
        FadeManager.I.LoadSceneWithFade(stageInfo.SceneName, 2.0f);
    }
}
