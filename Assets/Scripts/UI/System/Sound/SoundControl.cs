using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SoundControl : MonoBehaviour
{
    public GameObject BGM;
    public GameObject SE;
    public Slider bgslider;
    public Slider seslider;
    float backVol = 1f;
    float seVol = 1f;
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backVol");
        seVol = PlayerPrefs.GetFloat("seVol");
        bgslider.value = backVol;   //사운드 조절
        seslider.value = seVol;
    }

    void Update()
    {
        Bgm_sound();
        Se_sound();
    }

    void Bgm_sound()
    {
        BGM.GetComponent<AudioSource>().volume = bgslider.value;
        backVol = bgslider.value;
        PlayerPrefs.SetFloat("backVol", backVol);
    }

    void Se_sound()
    {
        SE.GetComponent<AudioSource>().volume = seslider.value;
        seVol = seslider.value;
        PlayerPrefs.SetFloat("seVol", seVol);
    }
}
