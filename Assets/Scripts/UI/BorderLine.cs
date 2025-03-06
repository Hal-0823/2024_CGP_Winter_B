using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ボーダーラインUI
/// </summary>
public class BorderLine : MonoBehaviour
{
    public TextMeshProUGUI BorderScoreText;
    private int score;

    public void SetScore(int score)
    {
        this.score = score;
        BorderScoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}