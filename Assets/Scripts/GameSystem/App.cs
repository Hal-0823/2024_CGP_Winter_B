using UnityEngine;

public class App : MonoBehaviour
{
    public static App I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
