using UnityEngine;

public class TestCreateScoreManager : MonoBehaviour
{
    public GameObject scoreManagerPrehub;
    bool exsistScoreManager = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    GameObject scoreManager ;
    public void OnButtonClick()
    {
        if (!exsistScoreManager)
        {
            scoreManager = Instantiate(scoreManagerPrehub);
            exsistScoreManager = true;
        }
        else
        {
            Destroy(scoreManager);
            exsistScoreManager = false;
        }
        // ここにボタンがクリックされたときの処理を追加
    }
}
