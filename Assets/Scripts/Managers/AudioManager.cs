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
    // Start is called before the first frame update

    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(SoundsFx soundsFx)
    {
        //get neccessary sounds clip
        // set clip and play
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
    }

    public void SetMusic(bool isOn)
    {
        StartCoroutine(MusicFadeOut(2f));
        //_musicSource.mute = isOn;
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
    //Game
    Booster,
    Collect,
    ColorBomb,
    ReachedGoal,
    //UI
    Button,
    Lose,
    Win
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
