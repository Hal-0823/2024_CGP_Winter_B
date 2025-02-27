using UnityEngine;

public class Sensor : MonoBehaviour
{
    SensorParent parent;
    bool isMant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = GetComponentInParent<SensorParent>();
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
            if(this.gameObject.tag == "EscapeSensor")
            {
                if(this.gameObject.name == "NiceSensor")
                {
                    parent.ChangeScore(500);
                }
                if(this.gameObject.name == "GreatSensor")
                {
                    parent.ChangeScore(800);
                }
                if(this.gameObject.name == "PerfectSensor")
                {
                    parent.ChangeScore(1000);
                }
            }
            
        }
    }
}
