using UnityEngine.Audio;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] musicBox;
    public AudioMixer audoMixer;
    public AudioMixerGroup music;
    public AudioMixerGroup soundFX;
    public static AudioManager instance;
    int currentTrack;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.name = s.name;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = soundFX;
            // mixerGroup.audioMixer = audioMixer;
        }
        foreach (Sound s in musicBox)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.name = s.name;
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = music;
            // mixerGroup.audioMixer = audioMixer;
        }
    }
    public void SetLevelMaster(float sliderVal)
    {
        audoMixer.SetFloat("Master", Mathf.Log10(sliderVal) * 20);
    }
    public void SetLevelMusic(float sliderVal)
    {
        audoMixer.SetFloat("Music", Mathf.Log10(sliderVal) * 20);
    }
    public void SetLevelSFX(float sliderVal)
    {
        audoMixer.SetFloat("SoundFX", Mathf.Log10(sliderVal) * 20);
    }
    void Start()
    {
        currentTrack = UnityEngine.Random.Range(0, musicBox.Length);
        PlayNextSong();
    }
    public void PlayNextSong()
    {
        currentTrack++;
        if (currentTrack > musicBox.Length - 1) currentTrack = 0;
        musicBox[currentTrack].source.Play();
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("sound: " + name + "not found");
            return;
        }
        s.source.Play();
    }
    private void Update()
    {
        if (!musicBox[currentTrack].source.isPlaying) PlayNextSong();
    }

}
