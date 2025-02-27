using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // スコア
    private int timeScore = 0;
    float time = 0;
    private float comboNum = 0;
    public bool isCountScore = false;
    HighScore highScore; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        isCountScore = true;
        highScore = GameObject.Find("HighScoreRegister").GetComponent<HighScore>();
    }
    void OnDestroy()
    {
        isCountScore = false;
        highScore.SaveHighScore(score);

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
                Debug.Log("現在コンボ数: " + comboNum);
                score += (int)(value * (1.0f + (comboNum * 0.25f)));
                comboNum++;
            }
            else
            {
                comboNum = 0;
            }
        }
    }

    public int GetScore()
    {
        return score;
    }
}
