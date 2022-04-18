using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSound : MonoBehaviour
{
    public AudioClip drawAudioClip;
    public AudioClip shotAudioClip;


    IEnumerator playAudioOnce()
    {
        yield return new WaitForSeconds(0);
        gameObject.GetComponent<AudioSource>().Play();
    }
    public void DrawAudio()
    {

        gameObject.GetComponent<AudioSource>().clip = drawAudioClip;
        StartCoroutine(playAudioOnce());
    }
    public void StopAudioClip()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void ShotAudio()
    {
        gameObject.GetComponent<AudioSource>().clip = shotAudioClip;
        StartCoroutine(playAudioOnce());
    }
}
