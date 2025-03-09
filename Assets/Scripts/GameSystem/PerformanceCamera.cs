using UnityEngine;

public class PerformanceCamera : MonoBehaviour
{
    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        var stageInfo = UserData.I.GetCurrentStageInfo();
        camera.backgroundColor = stageInfo.ThemeColor;
    }
}