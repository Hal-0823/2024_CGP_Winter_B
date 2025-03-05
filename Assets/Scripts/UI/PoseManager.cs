using UnityEngine;

public class PoseManager : MonoBehaviour
{
    public static bool isPose = false;
    public GameObject poseUI;
    GameObject pose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!isPose)
            {
                Time.timeScale = 0; 
                pose = Instantiate(poseUI,this.transform);
            }
            else if(isPose)
            {
                Destroy(pose);
                Time.timeScale = 1.0f; 
            }
            isPose = !isPose;
        }
    }
    
}
