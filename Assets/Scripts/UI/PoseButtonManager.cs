using UnityEngine;

public class PoseButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void ContinueGame()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        Destroy(this.gameObject);
        Time.timeScale = 1.0f; 
        PoseManager.isPose = false;
    }
    public void GoTitle()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        ScoreManager.isCountScore = false;
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        PoseManager.isPose = false;
        FadeManager.I.LoadSceneWithFade("Title", 2.0f);
    }
    public void ReTry()
    {
        AudioManager.I.PlaySE(SE.Name.Click);
        AudioManager.I.StopBGM();
        ScoreManager.isCountScore = false;
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        PoseManager.isPose = false;

        var stageInfo = UserData.I.GetCurrentStageInfo();
        FadeManager.I.LoadSceneWithFade(stageInfo.SceneName, 2.0f);
    }
}
