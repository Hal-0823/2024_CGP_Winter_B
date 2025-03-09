using UnityEngine;

public class SensorParent : MonoBehaviour
{
    ScoreManager scoreManager;
    private bool IsBanned;
    public bool isBanned
    {
        get
        {
            return IsBanned;
        }
        set
        {
            IsBanned = value;
            if(value)
            {
                score=0;
                gameProduction.EndJustEscape();
            }
        }
    }
    private int score=0;
    GameProduction gameProduction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameProduction = Camera.main.GetComponent<GameProduction>();
        scoreManager = GameManager.I.scoreManager;
        
        score=0;
    }
    void OnDestroy()
    {
        scoreManager.PlusScore(score);
        gameProduction.EndJustEscape();
    }

    public void ChangeScore(int value)
    {
        if(value>score)
        {
            score = value;
            gameProduction.StartJustEscape();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
