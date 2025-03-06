using UnityEngine;

public class UserData : MonoBehaviour
{
    public int STAR=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UserData I;

    void Awake()
    {
        if(I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AddStar(int value)
    {
        STAR += value;
    }

    public int GetStar()
    {
        return STAR;
    }
    
}
