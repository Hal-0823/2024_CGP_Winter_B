using UnityEngine;
using System.Collections.Generic;

public class HighScore : MonoBehaviour
{

    //ステージセレクトシーンで使うメソッド達 stageNuM　ステージ番号１～５　星の数１～３
    //各ステージの各星のボーダーを返す　
    public int GetBorderScoreForStageSelect(int stageNum,int border)
    {
        return borderScore[stageNum-1][border-1];
    }

    //各ステージのハイスコアを返す
    public int GetUserHighScoreEachStage(int stageNum)
    {
        return highScore[stageNum-1];
    }

    //各ステージの取得済み星の数
    public int GetUserStarNumEachStage(int stageNum)
    {
        return starNum[stageNum-1];
    }

    //ステージ番号<stageNum>　各ステージシーン起動時に適切に設定してほしい
    //山本が担当したものは大体これで動くはず
    public int stageNum = 0;

    //今回のスコアを取得（リザルト画面とかに使ってください）
    // さらにCompareScoreWithBorder(”今回のスコア”)で今回の星の数を取得する
    public int GetOneTimeScore()
    {
        return oneTimeScore;
    }


    public int[] highScore = new int[5]; //ハイスコア(インデックス０は無限ステージ（仮）)
    public int[] starNum = new int[5]; //各ステージの星の数

    

    public static HighScore I;

    //ステージ名とステージ番号の対応(PoseMenuButtonのリトライに使っている)
    Dictionary<int, string> stageDictionary = new Dictionary<int, string>();
    

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
        stageDictionary.Add(1, "NormalStage");
        stageDictionary.Add(2, "FactoryStage");
        stageDictionary.Add(3, "SnowStage");
        stageDictionary.Add(4, "PoisonStage");
        stageDictionary.Add(5, "VolcanoStage");
    }

    //各ステージのスコアのボーダー
    public int[][] borderScore = new int[5][]{
        new int[3]{8000,14000,20000}, //ステージ1：星１、星２、星３
        new int[3]{0,0,0}, //ステージ2：*
        new int[3]{0,0,0},
        new int[3]{0,0,0},
        new int[3]{0,0,0}};
    
    int oneTimeScore = 0;

    void Start()
    {
    }
    public void SaveHighScore(int score)
    {
        if (score > highScore[stageNum-1])
        {
            highScore[stageNum-1] = score;
            CompareScoreWithBorder(highScore[stageNum-1]);
        }
        oneTimeScore = score;
    }

    public int CompareScoreWithBorder(int score)
    {
        int i=0;
        int star=0;
        while(score>=borderScore[stageNum-1][i])
        {
            star++;
            i++;
            if(i==3) break;
        }

        //星の数が増えたらUserDataに保存
        if(star>starNum[stageNum-1])
        {
            //UserData.I.AddStar(star-starNum[stageNum-1]);
            starNum[stageNum-1] = star;
        }
        return star;
    }

    public int GetBorderScore(int border)
    {
        return borderScore[stageNum-1][border-1];
    }

    public string GetNowStageName()
    {
        return stageDictionary[stageNum];
    }

    
}
