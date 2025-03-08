using UnityEngine;

public class PerformanceCamera : MonoBehaviour
{
    Camera camera;

    public StageInfo stageInfo;

    void Start()
    {
        camera = GetComponent<Camera>();
        stageInfo = GameObject.FindWithTag("Stage").GetComponent<StageInfo>();
        camera.backgroundColor = stageInfo.ThemeColor;
    }
}