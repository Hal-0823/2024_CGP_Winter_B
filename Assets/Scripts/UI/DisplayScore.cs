using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI effectScoreText;
    int beforeScore;
    ScoreManager scoreManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        effectScoreText = GameObject.Find("ScoreEffectText").GetComponent<TextMeshProUGUI>();
        scoreManager = GetComponent<ScoreManager>();
        beforeScore = scoreManager.GetScore();
    }
    void OnDestroy()
    {
        DeleteEffect();
        scoreText.text = "";
    }


    string evaluation = "";
    // Update is called once per frame
    void Update()
    {
        
        int residualScore=scoreManager.GetScore()-beforeScore;
        if (residualScore>=10)
        {
            if(residualScore>=3000)
            {
                evaluation = "Funtastic!!!";
            }
            else if(residualScore>=2000)
            {
                evaluation = "Excellent!!";
            }
            else if(residualScore>=1000)
            {
                evaluation = "Great!";
            }
            else
            {
                evaluation = "Good";
            }

            effectScoreText.text = "+" + residualScore+"\n  " + evaluation;
            Invoke("DeleteEffect", 2.0f);
        }
        scoreText.text = "Score:" + scoreManager.GetScore();
        beforeScore = scoreManager.GetScore();

    }
    void DeleteEffect()
    {
        effectScoreText.text = "";
    }
    
}
