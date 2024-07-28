using UnityEngine.Audio;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static float masterVolume = 1;
    public static float musicVolume = 1;
    public static float sfxVolume = 1;
    
    private static List<AudioSource> musicSources = new List<AudioSource>();
    private static List<AudioSource> sfxSources = new List<AudioSource>();

    public static void Play(GameObject obj, string resourceName, bool isMusic = false, float pitchVariance = 0) {
        AudioSource src = CreateSource(obj, isMusic);
        src.clip = LoadResource(resourceName);
        src.pitch += Random.Range(1 - pitchVariance, 1 + pitchVariance);
        src.Play();
        Destroy(src, src.clip.length);
    }
    
    public static void Play(GameObject obj, string[] resourceNames, bool isMusic = false, float pitchVariance = 0) {
        AudioSource src = CreateSource(obj, isMusic);
        src.clip = LoadResource(resourceNames[Random.Range(0, resourceNames.Length - 1)]);
        src.pitch += Random.Range(1 - pitchVariance, 1 + pitchVariance);
        src.Play();
        Destroy(src, src.clip.length);
    }

    public static void PlayAtVolume(GameObject obj, AudioClip clip, float volume) {
        AudioSource src = CreateSource(obj, false);
        src.volume = volume;
        src.clip = clip;
        src.Play();
        Destroy(src, clip.length);
    }

    public static void SetVolume(float newVolume, int type) {
        if (type == 0) {
            masterVolume = newVolume;
            foreach (AudioSource src in musicSources) {
                src.volume = masterVolume * musicVolume;
            }
            foreach (AudioSource src in sfxSources) {
                src.volume = masterVolume * sfxVolume;
            }
        }
        else if (type == 1) {
            sfxVolume = newVolume;
            foreach (AudioSource src in sfxSources) {
                src.volume = masterVolume * sfxVolume;
            }
        }
        else if (type == 2) {
            musicVolume = newVolume;
            foreach (AudioSource src in musicSources) {
                src.volume = masterVolume * musicVolume;
            }
        }
        
    }

    private static AudioSource CreateSource(GameObject obj, bool isMusic) {
        AudioSource src = obj.AddComponent<AudioSource>();
        if (isMusic) {
            src.volume = masterVolume * musicVolume;
            musicSources.Add(src);
        }
        else {
            src.volume = masterVolume * sfxVolume;
            sfxSources.Add(src);
        }
        return src;
    }

    private static AudioClip LoadResource(string path) {
        return Resources.Load<AudioClip>("Sounds/" + path);
    }
}
