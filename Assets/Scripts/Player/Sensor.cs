using UnityEngine;

public class Sensor : MonoBehaviour
{
    public ScoreManager scoreManager;
    bool isMant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Movement_Player movement_Player = transform.parent.GetComponent<Movement_Player>();
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("センサーが " + other.gameObject.name + " を感知");
            if(isMant)
            {
                movement_Player.LookAtEnemy(other.gameObject);
            }
            if(!Movement_Player.isGrounded&&GameObject.Find("ScoreManager(Clone)"))
            {
                scoreManager = GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManager>();
                scoreManager.PlusScore(1000);
            }
            if(this.gameObject.tag == "RollSensor")
            {
                scoreManager = GameObject.Find("ScoreManager(Clone)").GetComponent<ScoreManager>();
                scoreManager.PlusScore(1000);
            }
        }
    }
}
