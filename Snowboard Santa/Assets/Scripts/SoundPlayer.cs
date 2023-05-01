using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static void PlaySound(AudioClip clip, float volume = 1f)
    {
        if(clip == null)
        {
            Debug.LogWarning("Trying to play a sound that doesn't exist!");
            return;
        }
        AudioSource src = new GameObject(clip.name, typeof(AudioSource)).GetComponent<AudioSource>();
        src.clip = clip;
        src.volume = volume;
        src.Play();
        Destroy(src.gameObject, clip.length);
    }
}
