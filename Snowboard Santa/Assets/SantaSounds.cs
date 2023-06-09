using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SantaSounds : MonoBehaviour
{
    private SantaMovement sm;
    private AudioSource audio;

    [SerializeField] private AudioClip chimneyJump;
    [SerializeField] private float chimneyJumpVolume;

    [SerializeField] private AudioClip landingSound;
    [Range(0f, 1f)]
    [SerializeField] private float landingSoundVolume;
    private float lastLandingSoundPlayed;

    [SerializeField] private AudioClip[] jumpSound;
    [SerializeField] private List<AudioClip> unusedJumpSounds = new List<AudioClip>();
    [Range(0f, 2f)]
    [SerializeField] private float jumpSoundVolume;

    [SerializeField] private AudioClip[] presentCrunch;


    private void OnEnable()
    {
        sm = GetComponent<SantaMovement>();
        audio = GetComponent<AudioSource>();

        sm.OnLand += PlaySledSound;
        sm.OnLand += PlayLandingSound;
        sm.OnJump += PlayJumpSound;
        GameStateHandler.OnGameOver += PlaySantaCrunch;
        sm.OnAirborne += StopSledSound;
        sm.OnChimneyJump += PlayChimneyJump;
    }

    private void OnDisable()
    {
        sm.OnLand -= PlaySledSound;
        sm.OnLand -= PlayLandingSound;
        sm.OnJump -= PlayJumpSound;
        GameStateHandler.OnGameOver -= PlaySantaCrunch;
        sm.OnAirborne -= StopSledSound;
        sm.OnChimneyJump -= PlayChimneyJump;
    }

    private void PlayJumpSound()
    {
        if (unusedJumpSounds.Count == 0)
        {
            foreach (AudioClip clip in jumpSound)
            {
                unusedJumpSounds.Add(clip);
            }
        }

        if (jumpSound.Any())
        {
            int index = Random.Range(0, unusedJumpSounds.Count);
            SoundPlayer.PlaySound(unusedJumpSounds[index], jumpSoundVolume);
            unusedJumpSounds.RemoveAt(index);
        }
    }

    private void PlayChimneyJump()
    {
        SoundPlayer.PlaySound(chimneyJump, chimneyJumpVolume);
    }
    
    private void PlaySantaCrunch()
    {
        foreach(var c in presentCrunch)
            SoundPlayer.PlaySound(c);
    }

    private void PlaySledSound()
    {
        if (audio != null)
        {
            audio.volume = 1f;
        }
    }

    private void StopSledSound()
    {
        if (audio != null)
        {
            audio.volume = 0f;
        }
    }

    private void PlayLandingSound()
    {
        if (Time.time - lastLandingSoundPlayed < 0.2f) return;
        SoundPlayer.PlaySound(landingSound, landingSoundVolume);
        lastLandingSoundPlayed = Time.time;
    }

}
