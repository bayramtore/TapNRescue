
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

public class AudioManager : Singleton<AudioManager>
{
    public Dictionary<string, Sound> sound_dic = new();
       
    public Sound[] sounds;
    public bool isSoundOn = true;
    public bool isBackgroundOn = true;
    public bool isHapticOn = true;
    readonly bool hapticSupported = DeviceCapabilities.isVersionSupported;


    protected override void OnAwake()
    {
        if (PlayerPrefs.GetInt("music", 0) == 1)
        {
            isBackgroundOn = true;
        }
        else
        {
            isBackgroundOn = false;
        }

        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            isSoundOn = true;
        }
        else
        {
            isSoundOn = false;
        }

        if (PlayerPrefs.GetInt("haptic", 1) == 1)
        {
            isHapticOn = true;
        }
        else
        {
            isHapticOn = false;
        }


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = s.playOnAwake;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.volume = s.volume;
            sound_dic.Add(s.name, s);
        }
    }

    public void SetAudioBoolen(string toggleId, bool on)
    {
        Play("click");
        switch (toggleId)
        {
            case "music":
                isBackgroundOn = on;
                break;

            case "sound":
                isSoundOn = on;
                break;

            case "haptic":
                isHapticOn = on;
                break;
        }

        PlayerPrefs.SetInt(toggleId, (on) ? 1 : 0);
    }



    public void PlayBackgroundMusic(string name, bool loop = true, float vol = 1, float pitch = 1)
    {
        if (isBackgroundOn)
        {
            //Sound s = Array.Find(sounds, sound => sound.name == name);

            Sound s = sound_dic[name];

            if (s == null)
            {
                Debug.LogWarning("No sound name " + name + " was found");
                return;
            }
            s.source.pitch = pitch;
            s.source.loop = loop;
            s.source.volume = vol;
            s.source.Play();

        }
    }

    IEnumerator changeValueCoroutine;
    IEnumerator ChangeValueCoroutine(string name, float v_end, float duration)
    {
        Sound s = sound_dic[name];
        if (v_end == 1)
        {
            PlayBackgroundMusic(name, vol: 0);
        }
        float v_start = s.volume;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            s.source.volume = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        s.source.volume = v_end;
        if (v_end == 0)
        {
            s.source.Stop();
        }
        
    }

    public void ChangeValue(string name, float v_end, float duration)
    {
        if (changeValueCoroutine != null)
        {
            StopCoroutine(changeValueCoroutine);
        }
        changeValueCoroutine = ChangeValueCoroutine(name, v_end, duration);
        StartCoroutine(changeValueCoroutine);
    }


    public void StopBackgroundMusic(string name)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s = sound_dic[name];
        if (s == null)
        {
            Debug.LogWarning("No sound name " + name + " was found");
            return;
        }
        s.source.Stop();
    }

    //bool isWoodExplosionPlaying = false;

    public void Play(string name, bool loop = false, float vol = 1, float pitch = 1)
    {

        switch (name)
        {
            case "click":
            case "popup":
            case "throw":
                HapticLight();
                break;

            case "error":
            case "lockOpen":

                HapticHeavy();
                break;
        }


        if (isSoundOn)
        {
            //Sound s = Array.Find(sounds, sound => sound.name == name);
            Sound s = sound_dic[name];
            if (s == null || s.source.isPlaying)
            {
                Debug.LogWarning("No sound name " + name + " was found");
                return;
            }
            s.source.pitch = pitch;
            s.source.loop = loop;
            s.volume = vol;
            s.source.Play();
        }
    }


    public void Stop(string name)
    {
        //Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s = sound_dic[name];
        if (s == null)
        {
            Debug.LogWarning("No sound name " + name + " was found");
            return;
        }
        s.source.Stop();

    }

    public void HapticLight()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }
    }

    public void HapticMedium()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
        }
    }


    public void HapticHeavy()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);

        }
    }

    public void HapticLost()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);
        }
    }

    public void HapticWon()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
        }
    }

    public void HapticWarning()
    {
        if (hapticSupported && isHapticOn)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
        }
    }
}

