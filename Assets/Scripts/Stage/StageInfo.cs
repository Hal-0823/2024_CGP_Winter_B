using UnityEngine;

/// <summary>
/// ステージの情報を格納しているクラス
/// </summary>
public class StageInfo : MonoBehaviour
{
    public int StageIndex;
    public string StageName;
    public string SceneName;
    public Color ThemeColor;
    public int BestScore { get; private set; }
    public int StarNum { get; private set; }

    void Awake()
    {
        BestScore = UserData.I.GetBestScore(StageIndex);
        StarNum = UserData.I.GetStarCount(StageIndex);
    }
}