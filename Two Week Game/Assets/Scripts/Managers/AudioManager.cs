using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ManagerBehaviour<AudioManager>
{
    /// <summary>
    /// Plays the specified audio, and looks it if specified
    /// </summary>
    public void PlayAudio(AudioClip clip, bool loop = false)
    {
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            if (audioSource.isPlaying)
            {
                continue;
            }
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
            return;
        }
        var newAudioSource = gameObject.AddComponent<AudioSource>();
        newAudioSource.clip = clip;
        newAudioSource.loop = loop;
        newAudioSource.Play();
    }

    /// <summary>
    /// Returns a list of all AudioClips currently being played
    /// </summary>
    public AudioClip[] GetPlayingClips()
    {
        var clips = new List<AudioClip>();
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            if (audioSource.isPlaying)
            {
                clips.Add(audioSource.clip);
            }
        }
        return clips.ToArray();
    }

    /// <summary>
    /// Stops all AudioClips which are currently being played
    /// </summary>
    public void StopAllAudio()
    {
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Stops all instances of a specific AudioClip which are being played
    /// </summary>
    public void StopAllAudio(AudioClip clip)
    {
        foreach (var audioSource in GetComponents<AudioSource>())
        {
            if (audioSource.clip == clip)
            {
                audioSource.Stop();
            }
        }
    }
}