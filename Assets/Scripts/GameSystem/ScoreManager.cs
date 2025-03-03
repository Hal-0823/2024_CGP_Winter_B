using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score; // スコア
    DisplayScore displayScore;
    private int timeScore = 0;
    float time = 0;
    private float comboNum = 0;
    public bool isCountScore = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        isCountScore = true;
        displayScore = this.gameObject.GetComponent<DisplayScore>();
    }
    void OnDestroy()
    {
        isCountScore = false;
        HighScore.I.SaveHighScore(score);

    }

    // Update is called once per frame
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
                if(value>=1000)
                {
                    displayScore.evaluation = "Excellent!";
                }
                else if(value>=800)
                {
                    displayScore.evaluation = "Great!";
                }
                else if(value>=500)
                {
                    displayScore.evaluation = "Nice!";
                }
                else if(value<0)
                {
                    displayScore.evaluation = "Miss!";
                    Debug.Log(displayScore.evaluation);
                }
                Debug.Log("現在コンボ数: " + comboNum);
                score += (int)(value * (1.0f + (comboNum * 0.1f)));
                comboNum++;
            }
            else if(value<0)
            {
                score += value;
                comboNum = 0;
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void GotDamageEffectForScore()
    {
        PlusScore(-1000);
        comboNum = 0;
    }
}
