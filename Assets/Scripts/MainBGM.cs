using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBGM : MonoBehaviour
{
    float seVol = 1f;
    AudioSource audiosource;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        seVol = PlayerPrefs.GetFloat("seVol");
    }

    // Update is called once per frame
    void Update()
    {
        audiosource.volume = seVol;
    }
}