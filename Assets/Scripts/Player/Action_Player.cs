using UnityEngine;

public class Action_Player : MonoBehaviour
{
    public GameObject prefab;
    Movement_Player Script;

    void Start()
    {
        Script = this.GetComponent<Movement_Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Script.StopMoving();
            GameObject senser = Instantiate(prefab,this.transform);
            Destroy(senser, 0.1f);
        }
        
    }
}
