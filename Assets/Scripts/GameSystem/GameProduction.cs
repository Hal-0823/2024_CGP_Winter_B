using UnityEngine;

public class GameProduction : MonoBehaviour
{
    Camera cam;
    Vector3 defaultCameraPosition;
    Quaternion defaultCameraRotation;
    float defaultCameraSize;
    GameObject player;
    bool isJustEscape = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        defaultCameraPosition = cam.transform.position;
        defaultCameraRotation = cam.transform.rotation;
        defaultCameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartJustEscape()
    {
        // カメラを移動させる
        isJustEscape = true;
        player = GameObject.FindWithTag("Player");
        cam.orthographicSize = 4.0f;
        cam.transform.position = player.transform.position + new Vector3(0, 6, -10);
        cam.transform.rotation = Quaternion.Euler(30, 0, 0);
        Time.timeScale = 0.3f;
        AudioManager.I.audioMixer.SetFloat("BGM", -10);
        
    }
    public void EndJustEscape()
    {
        // カメラを元に戻す
        cam.orthographicSize = defaultCameraSize;
        cam.transform.position = defaultCameraPosition;
        cam.transform.rotation = defaultCameraRotation;
        Time.timeScale = 1.0f;
        if(isJustEscape)
        {
            AudioManager.I.audioMixer.SetFloat("BGM", 0);
            isJustEscape=false;
        }
        
    }
}
