using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score; // スコア
    DisplayScore displayScore;
    private int timeScore = 0;
    float time = 0;
    private float comboNum = 0;
    public static bool isCountScore = false;
    private AudienceController audience;    // ステージの観客を制御するクラス

    void Awake()
    {
        displayScore = this.gameObject.GetComponent<DisplayScore>();
        isCountScore = true;
        audience = GameObject.FindWithTag("Audience").GetComponent<AudienceController>();
        displayScore.Initialize();
        SetScore(0);
    }
    
    void OnDestroy()
    {
        isCountScore = false;
    }


    void Update()
    {
        if(isCountScore)
        {
            time += Time.deltaTime;
            if(time>=1)
            {
                timeScore += 1;
                time = 0;
            }

        }
        
    }

    public void PlusScore(int value)
    {
        if(isCountScore)
        {
            if(value>0)
            {
                audience.SetGlad();

                if(value>=1000)
                {
                    displayScore.SetEvaluationImage(2);
                    AudioManager.I.PlaySE(SE.Name.ExcellentReaction);
                }
                else if(value>=800)
                {
                    displayScore.SetEvaluationImage(1);
                    AudioManager.I.PlaySE(SE.Name.GreatReaction);
                }
                else if(value>=500)
                {
                    displayScore.SetEvaluationImage(0);
                    AudioManager.I.PlaySE(SE.Name.NiceReaction);
                }

                score += (int)(value * (1.0f + (comboNum * 0.1f)));
                comboNum++;
            }
            else if(value<0)
            {
                audience.SetAngry();

                score += value;
                comboNum = 0;
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    private void SetScore(int score)
    {
        this.score = score;
        displayScore.BorderEffect(score);
    }

    public int GetCombo()
    {
        return (int)comboNum;
    }

    public int GotDamageEffectForScore()
    {
        PlusScore(-1000);
        if(isCountScore)
        {
            comboNum = 0;
        }
        return score;
    }
}
