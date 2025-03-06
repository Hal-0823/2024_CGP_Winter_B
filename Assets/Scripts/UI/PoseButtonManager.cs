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
        Destroy(this.gameObject);
        Time.timeScale = 1.0f; 
        PoseManager.isPose = false;
    }
    public void GoTitle()
    {
        ScoreManager.isCountScore = false;
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        PoseManager.isPose = false;
        FadeManager.I.LoadSceneWithFade("StageSelect", 2.0f);
    }
    public void ReTry()
    {
        ScoreManager.isCountScore = false;
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        PoseManager.isPose = false;
        FadeManager.I.LoadSceneWithFade(HighScore.I.GetNowStageName(), 2.0f);
    }
}
