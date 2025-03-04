using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class DisplayScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI effectScoreText;
    int beforeScore;
    Slider scoreSlider;
    public GameObject prefabBorderImage;
    GameObject backGround;
    ScoreManager scoreManager;
    private GameObject[] border=new GameObject[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreSlider = GameObject.Find("VOLTAGE").GetComponent<Slider>();
        effectScoreText = GameObject.Find("ScoreEffectText").GetComponent<TextMeshProUGUI>();
        backGround = GameObject.Find("Background");
        scoreManager = GetComponent<ScoreManager>();
        beforeScore = scoreManager.GetScore();
        for (int i = 1; i <= 3; i++)
        {
            float borderX = 400f*(float)(HighScore.I.GetBorderScore(i) + 5000f) / (HighScore.I.GetBorderScore(3) + 5000f);
            border[i-1] = Instantiate(prefabBorderImage, backGround.transform);
            border[i-1].transform.localPosition = new Vector3((borderX-200f), 0, 0);
            TextMeshProUGUI borderText = border[i - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            borderText.text = HighScore.I.GetBorderScore(i).ToString();
            Debug.Log("borderRatio:" + borderX);
        }
    }
    void OnDestroy()
    {
        DeleteEffect();
        scoreText.text = "";
    }


    public string evaluation = "";
    float AchivementRate()
    {
        return ((float)(scoreManager.GetScore()+5000f)/(HighScore.I.GetBorderScore(3)+5000f));
    }
    void Update()
    {
        
        scoreSlider.value = AchivementRate();
        int residualScore=scoreManager.GetScore()-beforeScore;
        if (residualScore>=10)
        {
            effectScoreText.text = "+" + residualScore+"\n  " + evaluation;
            Invoke("DeleteEffect", 2.0f);
            Debug.Log((scoreManager.GetScore()+" / "+HighScore.I.GetBorderScore(3)));

        }
        else if(residualScore<0)
        {
            effectScoreText.color = Color.red;
            effectScoreText.text = residualScore.ToString();
            Invoke("DeleteEffect", 2.0f);
        }
        scoreText.text = "Score:" + scoreManager.GetScore();
        beforeScore = scoreManager.GetScore();

    }
    void DeleteEffect()
    {
        effectScoreText.text = "";
        effectScoreText.color = Color.white;
        evaluation = "";
    }
    
}
