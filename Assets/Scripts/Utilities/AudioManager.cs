using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource SESource;
    public AudioSource BGMSource;

    [SerializeField] List<SE> seList;
    [SerializeField] List<BGM> bgmList;
    [SerializeField] Slider seSlider;
    [SerializeField] Slider bgmSlider;

    public static AudioManager I;

    public enum VolumeParam
    {
        SE,
        BGM
    }

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
        // Volumeを初期化　初プレイ時は音量3
        foreach(var param in Enum.GetNames(typeof(VolumeParam)))
        {
            //SetVolume(param, PlayerPrefs.GetFloat(param.ToString() + "SliderValue", 0));
            //SetVolume(param, 3);
        }
        //seSlider.value = 3;
        //bgmSlider.value = 3;
    }

    // --------------------------------------------------

    public void PlaySE(SE.Name seName, AudioSource audioSource = null)
    {
        // 引数を指定しなかったら、sESourceから出力
        if(audioSource == null)
        {
            audioSource = SESource;
        }

        var data = seList.Find(data => data.seName == seName);
        audioSource.PlayOneShot(data.sEClip);
    }

    public void PlayBGM(BGM.Name bgmName)
    {
        var data = bgmList.Find(data => data.bgmName == bgmName);
        BGMSource.clip = data.bGMClip;
        BGMSource.Play();
    }

    public void StopBGM()
    {
        BGMSource.Stop();
    }

    public void SetVolume(string volumeParam, float sliderValue)
    {
        sliderValue /= 5;
        float volumeValue =  20 * Mathf.Log10(sliderValue) - 20; // 最大で0dBに調整
        volumeValue = Mathf.Max(-80, volumeValue); // 最低で-100dBに設定
        audioMixer.SetFloat(volumeParam, volumeValue);

        // sliderValueを保存　volumeValueじゃないよ
        //PlayerPrefs.SetFloat(volumeParam + "SliderValue", sliderValue);
    }

    public void SetSEVolume()
    {
        SetVolume("SE", seSlider.value*2);
    }

    public void SetBGMVolume()
    {
        SetVolume("BGM", bgmSlider.value*2);
    }
}

[System.Serializable]
public class SE
{
    public enum Name
    {
        Hit,
        Explosion,
        Click,
        ScoreAdd,
        Rolling,
        Jump,
        BadReaction,
        NiceReaction,
        GreatReaction,
        ExcellentReaction,
        Start,
        Slide,
        Finish,
        
    }

    public Name seName;
    public AudioClip sEClip;
}

[System.Serializable]
public class BGM
{
    public enum Name
    {
        Stage_1,
        Stage_2,
        Stage_3,
        Stage_4,
        Stage_5,
        Title,
        StageSelect,
        Result,
        GameOver,
        Sllow,
    }

    public Name bgmName;
    public AudioClip bGMClip;
}

