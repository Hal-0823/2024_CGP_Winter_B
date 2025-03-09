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
    private int[] borderScore;

    public void SetEvaluationImage(int evaluationNum)
    {
        evaluationImage.sprite = evaluationSprite[evaluationNum];
    }

    void Awake()
    {
        evaluationImage = GameObject.Find("EvaluationImage").GetComponent<Image>();
        evaluationImage.enabled = false;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreSlider = GameObject.Find("VOLTAGE").GetComponent<Slider>();
        effectScoreText = GameObject.Find("ScoreEffectText").GetComponent<TextMeshProUGUI>();
        backGround = GameObject.Find("VoltageBackground");
        scoreManager = GetComponent<ScoreManager>();
        beforeScore = scoreManager.GetScore();

        var stageInfo = UserData.I.GetCurrentStageInfo();
        borderScore = BorderScoreTable.GetBorderScores(stageInfo.StageIndex);
        for (int i = 0; i <= 2; i++)
        {
            float borderX = 800f*(float)(borderScore[0] + 5000f) / (borderScore[2] + 5000f);
            border[i] = Instantiate(prefabBorderImage, backGround.transform);
            border[i].transform.localPosition = new Vector3((borderX-400f), 0, 0);
            TextMeshProUGUI borderText = border[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            borderText.text = borderScore[i].ToString();
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
        return ((float)(scoreManager.GetScore()+5000f)/(borderScore[2]+5000f));
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
