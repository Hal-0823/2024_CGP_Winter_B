using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public string VolumeParam;
    
    void Start()
    {
        this.GetComponent<Slider>().value = AudioManager.I.audioMixer.GetFloat(VolumeParam, out float value) ? value : 0;
    }
    void OnDestroy()
    {
        Debug.Log(this.GetComponent<Slider>().value);
        AudioManager.I.audioMixer.SetFloat(VolumeParam, this.GetComponent<Slider>().value);
    }

    // Update is called once per frame
    void Update()
    {
         AudioManager.I.audioMixer.SetFloat(VolumeParam, this.GetComponent<Slider>().value);
        
    }
}
