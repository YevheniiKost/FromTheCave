using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soundFxSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private List<SoundFxData> _soundsFXList = new List<SoundFxData>();
    [SerializeField] private List<MusicData> _musicList = new List<MusicData>();

    public static AudioManager Instance;

    public float MusicVolume = 1;
    public float SoundsFXVolume = 1;

    public bool IsMusicOff;
    public bool IsSoundsOff;

    private void Awake()
    {
        DontDestroyOnLoad(this);

       if (Instance != null)
           Destroy(this.gameObject);
       else
        Instance = this;
    }

    private void Start()
    {
        MusicVolume = _musicSource.volume;
        SoundsFXVolume = _soundFxSource.volume;
        IsMusicOff = _musicSource.mute;
        IsSoundsOff = _soundFxSource.mute;
    }


    public void PlaySFX(SoundsFx soundsFx)
    {
        var clip = GetSoundFXClip(soundsFx);
        _soundFxSource.PlayOneShot(clip);
    }

    public void PlayMusic(MusicType music)
    {
        _musicSource.clip = GetMusicClip(music);
        _musicSource.Play();
    }

    public void SetSoundFX(bool isOn)
    {
        _soundFxSource.mute = isOn;
        IsSoundsOff = isOn;
    }

    public void SetMusic(bool isOn)
    {
        //StartCoroutine(MusicFadeOut(2f));
        _musicSource.mute = isOn;
        IsMusicOff = isOn;
    }

    public void SetMusicVolume(float value)
    {
        _musicSource.volume = value;
        MusicVolume = value;
    }

    public void SetFXVolume(float value)
    {
        _soundFxSource.volume = value;
        SoundsFXVolume = value;
    }

    private AudioClip GetSoundFXClip(SoundsFx soundsFx)
    {
        var soundData = _soundsFXList.Find(sfxData => sfxData.SoundFx == soundsFx);
        return soundData?.Clip;
    }

    private AudioClip GetMusicClip(MusicType music)
    {
        var musicData = _musicList.Find(musicfx => musicfx.Music == music);
        return musicData?.Clip;
    }

    private IEnumerator MusicFadeOut(float duration = 0.5f)
    {
        var endValue = 0f;
        var startValue = _musicSource.volume;
        var timeCounter = 0f;
        while (timeCounter < duration)
        {
            var normalizedTime = timeCounter / duration;

            _musicSource.volume = Mathf.Lerp(startValue, endValue, normalizedTime);
            timeCounter += Time.deltaTime;
            yield return null;
        }

        _musicSource.volume = 0;
    }

}

public enum SoundsFx
{
    //UI
    ButtonPointerEnter,
    ButtonPressed,
    //Game
    CharacterDead,
    WinGame,
    SaveGame,
    CollectCoin,
    FallingStone,
    GetWeapon, 
    SwordStrike,
    Throw,
    Kick,
    ShieldHit,
    Footstep,
    Jump,
    AxeImpact,
    PlayerGetHit,
    EnemyStrike,
    HealthPotion
}

public enum MusicType
{
    MainMenu,
    Game
}

[Serializable]
public class SoundFxData
{
    public SoundsFx SoundFx;
    public AudioClip Clip;
}


[Serializable]
public class MusicData
{
    public MusicType Music;
    public AudioClip Clip;
}
