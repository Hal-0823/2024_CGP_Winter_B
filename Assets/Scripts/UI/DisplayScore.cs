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
    public Image evaluationImage;
    public Sprite[] evaluationSprite=new Sprite[3];
    public void SetEvaluationImage(int evaluationNum)
    {
        evaluationImage.sprite = evaluationSprite[evaluationNum];
    }

    void Start()
    {
        evaluationImage.enabled = false;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreSlider = GameObject.Find("VOLTAGE").GetComponent<Slider>();
        effectScoreText = GameObject.Find("ScoreEffectText").GetComponent<TextMeshProUGUI>();
        backGround = GameObject.Find("VoltageBackground");
        scoreManager = GetComponent<ScoreManager>();
        beforeScore = scoreManager.GetScore();
        for (int i = 1; i <= 3; i++)
        {
            float borderX = 800f*(float)(HighScore.I.GetBorderScore(i) + 5000f) / (HighScore.I.GetBorderScore(3) + 5000f);
            border[i-1] = Instantiate(prefabBorderImage, backGround.transform);
            border[i-1].transform.localPosition = new Vector3((borderX-400f), 0, 0);
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
            effectScoreText.color = Color.white;
            effectScoreText.text = "+" + residualScore;
            evaluationImage.enabled = true;
            Invoke("DeleteEffect", 2.0f);

        }
        else if(residualScore<0)
        {
            effectScoreText.color = Color.red;
            effectScoreText.text = residualScore.ToString();
            Invoke("DeleteEffect", 2.0f);
        }
        if(scoreManager.GetScore()<=0)
        {scoreText.text = "Score:0\nCombo:x"+scoreManager.GetCombo();}
        else
        {scoreText.text = "Score:" + scoreManager.GetScore()+"\nCombo:x"+scoreManager.GetCombo();}
        beforeScore = scoreManager.GetScore();

    }
    void DeleteEffect()
    {
        effectScoreText.text = "";
        evaluationImage.enabled = false;
    }

    public void BorderEffect(int borderNum)
    {
        for (int i = 0; i < 3; i++)
        {
            border[i].GetComponent<Image>().color = Color.white;
        }
        if(borderNum>0)
        {
            for (int i = 1; i <= borderNum; i++)
            {
                border[i-1].GetComponent<Image>().color = Color.red;
            }
        }
    }
    
}
