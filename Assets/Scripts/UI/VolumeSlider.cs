using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public string VolumeParam;
    private Slider slider; 

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioManager.I.GetVolume(VolumeParam);
        Debug.Log(slider.value);
    }

    public void OnValueChanged()
    {
        AudioManager.I.SetVolume(VolumeParam, slider.value);
    }
}
