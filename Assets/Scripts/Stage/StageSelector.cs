using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ステージ選択画面を処理するクラス
/// </summary>
public class StageSelector : MonoBehaviour
{
    public StageInfo[] Stage;
    public TextMeshProUGUI StageNameText;
    public TextMeshProUGUI BestScoreText;
    public Image BackGroundImage;
    private int selectId;
    private float rotationSpeed;

    private void Start()
    {
        AudioManager.I.PlayBGM(BGM.Name.StageSelect);
        selectId = 0;
        rotationSpeed = 20.0f;

        for (int i=0; i<Stage.Length; i++)
        {
            if (i == selectId)
            {
                Stage[i].gameObject.SetActive(true);
            }
            else
            {
                Stage[i].gameObject.SetActive(false);
            }
        }

        ApplyStageInfo();
    }

    private void Update()
    {
        RotateStageObj();
    }

    private void RotateStageObj()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void ApplyStageInfo()
    {
        StageNameText.text = Stage[selectId].StageName;
        BestScoreText.text = $"BestScore:{Stage[selectId].BestScore}";
        BackGroundImage.color = Stage[selectId].ThemeColor;
    }

    public void NextStage()
    {
        Stage[selectId].gameObject.SetActive(false);

        if (selectId + 1 >= Stage.Length)
        {
            selectId = 0;
        }
        else
        {
            selectId++;
        }

        Stage[selectId].gameObject.SetActive(true);
        ApplyStageInfo();
        transform.rotation = Quaternion.identity;
    }

    public void BackStage()
    {
        Stage[selectId].gameObject.SetActive(false);

        if (selectId - 1 < 0)
        {
            selectId = Stage.Length - 1;
        }
        else
        {
            selectId--;
        }

        Stage[selectId].gameObject.SetActive(true);
        ApplyStageInfo();
        transform.rotation = Quaternion.identity;
    }

    public StageInfo GetStageInfo()
    {
        return Stage[selectId];
    }
}
