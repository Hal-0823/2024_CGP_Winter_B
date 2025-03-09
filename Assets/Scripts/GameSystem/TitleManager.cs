using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.I.PlayBGM(BGM.Name.Title);
    }
    public void OnClick()
    {
        AudioManager.I.StopBGM();
        AudioManager.I.PlaySE(SE.Name.Click);
        FadeManager.I.LoadSceneWithFade("StageSelect", Color.black, 1f);
    }
}
