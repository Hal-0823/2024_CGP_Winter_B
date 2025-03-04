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
}
