using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;  //bgm音频组件

    private Dictionary<string, AudioClip> clips;    //音频缓存字典

    private bool isStop;    //是否静音

    public bool IsStop
    {
        get { return isStop; }
        set
        {
            isStop = value;
            if (isStop)
            {
                bgmSource.Pause();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;    //bgm音量大小

    public float BgmVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume; //音效大小

    public float EffectVolume
    {
        get { return effectVolume; }
        set
        {
            effectVolume = value;       
        }
    }

    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        IsStop = false;
        BgmVolume = 1.0f;
        EffectVolume = 1.0f;
    }

    public void PlayBGM(string res)
    {
        if (isStop)
        {
            return;
        }

        //如果没有缓存就加载
        if (!clips.ContainsKey(res))
        {
            AudioClip audioClip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res, audioClip);
        }
        bgmSource.clip = clips[res];
        bgmSource.Play();
    }

    public void PlayEffect(string name, Vector3 pos)
    {
        if (isStop)
        {
            return;
        }
        AudioClip clip = null;
        if (!clips.ContainsKey(name))
        {
            clip = Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name, clip);
        }
        AudioSource.PlayClipAtPoint(clips[name], pos);
    }
}
