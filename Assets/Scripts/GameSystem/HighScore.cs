using UnityEngine;

public class HighScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int[] highScore = new int[6]; //ハイスコア(インデックス０は無限ステージ（仮）)
    public int[] starNum = new int[5]; //各ステージの星の数

    //各ステージのスコアのボーダー

    public static HighScore I;

    void Awake()
    {
        if(I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int[][] borderScore = new int[5][]{
        new int[3]{8000,14000,20000}, //ステージ1：星１、星２、星３
        new int[3]{0,0,0}, //ステージ2：*
        new int[3]{0,0,0},
        new int[3]{0,0,0},
        new int[3]{0,0,0}};
    public int stageNum = 0;
    UserData userData;
    void Start()
    {
        userData = GameObject.Find("UserDataBase").GetComponent<UserData>();
    }
    public void SaveHighScore(int score)
    {
        if (score > highScore[stageNum])
        {
            highScore[stageNum] = score;
            CompareHighScoreWithBorder(score);
        }
    }

    void CompareHighScoreWithBorder(int score)
    {
        int i=0;
        int star=0;
        while(score>=borderScore[stageNum-1][i])
        {
            star++;
            i++;
            if(i==3) break;
        }
        if(star>starNum[stageNum-1])
        {
            userData.AddStar(star-starNum[stageNum-1]);
            starNum[stageNum-1] = star;
        }
    }

    public int GetBorderScore(int border)
    {
        return borderScore[stageNum-1][border-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
