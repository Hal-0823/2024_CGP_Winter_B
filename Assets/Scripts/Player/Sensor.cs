using UnityEngine;

public class Sensor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Movement_Player Script = transform.parent.GetComponent<Movement_Player>();
        Debug.Log("センサーが " + other.gameObject.name + " を感知");
        if (other.gameObject.tag == "Enemy")
        {
            Script.LookAtEnemy(other.gameObject);
        }
    }
}
