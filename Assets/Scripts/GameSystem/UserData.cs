using UnityEngine;

public class UserData : MonoBehaviour
{
    public int STAR=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddStar(int value)
    {
        STAR += value;
    }
}
