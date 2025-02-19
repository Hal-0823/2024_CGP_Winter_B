using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // スコア
    float time = 0;
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
                score += 1;
                time = 0;
            }

        }
        
    }

    public void PlusScore(int value)
    {
        if(isCountScore)
        {
            score += value;
            Debug.Log("加点");
        }
    }

    public int GetScore()
    {
        return score;
    }
}
