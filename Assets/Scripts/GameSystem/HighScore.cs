using UnityEngine;

public class HighScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int[] highScore = new int[6]; //ハイスコア
    public static int stageNum = 0;
    void Start()
    {
        
    }
    public void SaveHighScore(int score)
    {
        if (score > highScore[stageNum])
        {
            highScore[stageNum] = score;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
