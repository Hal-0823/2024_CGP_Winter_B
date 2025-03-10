using UnityEngine;

public class UserData : MonoBehaviour
{
    public static UserData I;

    private int[] stageStars; // 各ステージの星の数
    private int[] bestScores; // 各ステージのベストスコア
    private StageInfo currentStageInfo; // 現在のステージ情報(保存はしない)
    private const string StarKey = "StageStar_"; // 星の保存キー
    private const string ScoreKey = "BestScore_"; // スコアの保存キー

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(gameObject); // シーンをまたいでも削除されない
            Initialize(5);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize(int stageCount)
    {
        if (stageStars == null)
        {
            stageStars = new int[stageCount];
            bestScores = new int[stageCount];
            LoadData(); // ゲーム起動時に保存データを読み込む
        }
    }

    private void LoadData()
    {
        for (int i = 0; i < stageStars.Length; i++)
        {
            stageStars[i] = PlayerPrefs.GetInt(StarKey + i, 0); // 星のデータ読み込み
            bestScores[i] = PlayerPrefs.GetInt(ScoreKey + i, 0); // ベストスコア読み込み
            Debug.Log($"Load: star{stageStars[i]} bestScore{bestScores[i]}");
        }
    }

    public void SaveStageResult(int stageIndex, int score, int starCount)
    {
        Debug.Log($"Save {stageIndex}, {score}, {starCount}");
        if (stageIndex >= 0 && stageIndex < stageStars.Length)
        {
            stageStars[stageIndex] = Mathf.Max(stageStars[stageIndex], starCount); // 過去の最高記録を保持
            PlayerPrefs.SetInt(StarKey + stageIndex, stageStars[stageIndex]); // データを保存

            bestScores[stageIndex] = Mathf.Max(bestScores[stageIndex], score);
            PlayerPrefs.SetInt(ScoreKey + stageIndex, bestScores[stageIndex]);

            PlayerPrefs.Save(); // 保存を確定
        }
    }

    public int GetStarCount(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < stageStars.Length)
        {
            return stageStars[stageIndex];
        }
        return 0;
    }

    public int GetBestScore(int stageIndex)
    {
        if (stageIndex >= 0 && stageIndex < bestScores.Length)
        {
            return bestScores[stageIndex];
        }
        return 0;
    }

    
    public StageInfo GetCurrentStageInfo()
    {
        return currentStageInfo;
    }

    public void SetCurrentStageInfo(StageInfo stageInfo)
    {
        currentStageInfo = stageInfo;
    }
}
