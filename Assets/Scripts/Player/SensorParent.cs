using UnityEngine;

public class SensorParent : MonoBehaviour
{
    ScoreManager scoreManager;
    public bool isBanned;
    private int score=0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManager>();
        score=0;
    }
    void OnDestroy()
    {
        if(!isBanned)
        {
            scoreManager.PlusScore(score);
        }
    }

    public void ChangeScore(int value)
    {
        if(value>score)
        {
            score = value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
