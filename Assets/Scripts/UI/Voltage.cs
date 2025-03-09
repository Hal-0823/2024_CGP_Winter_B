using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ボルテージUI
/// </summary>
public class Voltage : MonoBehaviour
{
    public Slider Slider;
    public GameObject BackGround;
    public BorderLine BorderLineObj;
    private BorderLine[] borderLine = new BorderLine[3];

    public void Initialize()
    {
        var stageInfo = UserData.I.GetCurrentStageInfo();
        var borderScore = BorderScoreTable.GetBorderScores(stageInfo.StageIndex);
        for (int i = 0; i <= 2; i++)
        {
            float borderX = 800f*(float)(borderScore[0]) / (borderScore[2]);
            borderLine[i] = Instantiate(BorderLineObj, BackGround.transform);
            borderLine[i].transform.localPosition = new Vector3((borderX-400f), 0, 0);
            borderLine[i].SetScore(borderScore[i]);
            Debug.Log("borderRatio:" + borderX);
        }

        Slider.value = 0;
        Slider.maxValue = borderLine[2].GetScore();
    }

    public int GetBorderScore(int index)
    {
        return borderLine[index].GetScore();
    }
}