using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //public AudioSource audioSource;
    public AudioClip bugAtk;
    public AudioClip wolfAtk;
    public AudioClip swordAtk1;
    public AudioClip swordAtk2;
    public AudioClip arrow;
    public AudioClip heal;
    public AudioClip atkbuff;
    public AudioClip revive;
    public IEnumerator bugAtkE(AudioSource sound)
    {
        sound.clip = bugAtk;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
    public IEnumerator wolfAtkE(AudioSource sound)
    {
        sound.clip = wolfAtk;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
    public IEnumerator swordAtk1E(AudioSource sound)
    {
        sound.clip = swordAtk1;
        yield return new WaitForSeconds(0.5f);
        sound.Play();
        yield return new WaitForSeconds(0.4f);
        sound.Stop();
    }
    public IEnumerator swordAtk2E(AudioSource sound)
    {
        sound.clip = swordAtk2;
        yield return new WaitForSeconds(0.5f);
        sound.Play();
        yield return new WaitForSeconds(0.4f);
        sound.Stop();
    }
    public IEnumerator arrowE(AudioSource sound)
    {
        sound.clip = arrow;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
    public IEnumerator healE(AudioSource sound)
    {
        sound.clip = heal;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
    public IEnumerator atkbuffE(AudioSource sound)
    {
        sound.clip = atkbuff;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
    public IEnumerator reviveE(AudioSource sound)
    {
        sound.clip = revive;
        sound.Play();
        yield return new WaitForSeconds(0.5f);
        sound.Stop();
    }
}
