using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] private StageSelector stageSelector;
    [SerializeField] private GameObject optionPanel;

    void Awake()
    {
        optionPanel.SetActive(false);
    }

    public void OnClickPlay()
    {
        StageInfo stage = stageSelector.GetStageInfo();
        UserData.I.SetCurrentStageInfo(stage);  // UserDataに現在のステージ情報を格納
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        FadeManager.I.LoadSceneWithFade(stage.SceneName, stage.ThemeColor, 3.0f);
    }

    public void OnClickNextStage()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        stageSelector.NextStage();
    }

    public void OnClickBackStage()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        stageSelector.BackStage();
    }

    public void OnClickBackTitle()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        FadeManager.I.LoadSceneWithFade("Title", Color.black, 2.0f);
    }

    public void OnClickOption()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        optionPanel.SetActive(true);
    }

    public void OnClickBackOption()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        optionPanel.SetActive(false);
    }
}
