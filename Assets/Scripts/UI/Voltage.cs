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
        for (int i = 1; i <= 3; i++)
        {
            float borderX = 800f*(float)(HighScore.I.GetBorderScore(i)) / (HighScore.I.GetBorderScore(3));
            borderLine[i-1] = Instantiate(BorderLineObj, BackGround.transform);
            borderLine[i-1].transform.localPosition = new Vector3((borderX-400f), 0, 0);
            borderLine[i-1].SetScore(HighScore.I.GetBorderScore(i));
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