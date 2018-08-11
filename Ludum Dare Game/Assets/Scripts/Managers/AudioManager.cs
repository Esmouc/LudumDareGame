using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string musicName;
    public AudioClip musicClip;
    public bool musicEnable;

    [Range(0f, 1f)]
    public float musicVolume = 1.0f;
    [Range(0.5f, 1.5f)]
    public float musicPitch = 1.0f;

    [Range(0f, 0.5f)]
    public float musicRandomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float musicRandomPitch = 0.1f;

    public bool musicLoop = false;

    private AudioSource musicSource;

    public void SetMusicSource(AudioSource source)
    {
        musicSource = source;
        musicSource.clip = musicClip;
        musicSource.loop = musicLoop;
        musicSource.enabled = musicEnable;
    }

    public void MusicPlay()
    {
        musicSource.volume = musicVolume * (1 + Random.Range(-musicRandomVolume / 2f, musicRandomVolume / 2f));
        musicSource.pitch = musicPitch * (1 + Random.Range(-musicRandomPitch / 2f, musicRandomPitch / 2f));
        musicSource.Play();
    }

    public void MusicStop()
    {
        musicSource.Stop();
    }
}

[System.Serializable]
public class SFX
{
    public string sfxName;
    public AudioClip sfxClip;
    public bool sfxEnable;

    [Range(0f, 1f)]
    public float sfxVolume = 1.0f;
    [Range(0.5f, 1.5f)]
    public float sfxPitch = 1.0f;

    [Range(0f, 0.5f)]
    public float sfxRandomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float sfxRandomPitch = 0.1f;

    public bool sfxLoop = false;

    private AudioSource sfxSource;

    public void SetSFXSource(AudioSource source)
    {
        sfxSource = source;
        sfxSource.clip = sfxClip;
        sfxSource.loop = sfxLoop;
        sfxSource.enabled = sfxEnable;
    }

    public void SFXPlay()
    {
        sfxSource.volume = sfxVolume * (1 + Random.Range(-sfxRandomVolume / 2f, sfxRandomVolume / 2f));
        sfxSource.pitch = sfxPitch * (1 + Random.Range(-sfxRandomPitch / 2f, sfxRandomPitch / 2f));
        sfxSource.Play();
    }

    public void SFXStop()
    {
        sfxSource.Stop();
    }
}


public class AudioManager : MonoBehaviour {

  public static AudioManager instance;
  /*private static bool keepFadingIn;
  private static bool keepFadingOut;*/

  public static bool muteMusicAlive = false;
  public static bool muteSFXAlive = false;

  [SerializeField]
  Music[] musicArray;

  [SerializeField]
  SFX[] sfxs;

  private void Awake()
  {
    if (instance == null){
      instance = this;
    }
    else if (instance != this){
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  void Start()
  {
    for (int i = 0; i < sfxs.Length; i++)
    {
      GameObject sfxGameObject = new GameObject("SFX_" + i + "_" + sfxs[i].sfxName);
      sfxGameObject.transform.SetParent(this.transform);
      sfxs[i].SetSFXSource(sfxGameObject.AddComponent<AudioSource>());
    }

    for (int i = 0; i < musicArray.Length; i++)
    {
      GameObject musicGameObject = new GameObject("Music_" + i + "_" + musicArray[i].musicName);
      musicGameObject.transform.SetParent(this.transform);
      musicArray[i].SetMusicSource(musicGameObject.AddComponent<AudioSource>());
    }
  }

  void UpdateAudioSourcesVolume()
  {
    for (int i = 0; i < musicArray.Length; i++)
    {
      AudioSource musicToFade = GameObject.Find("Music_" + i + "_" + musicArray[i].musicName).GetComponent<AudioSource>();
      musicToFade.volume = musicArray[i].musicVolume;
      musicToFade.enabled = musicArray[i].musicEnable;

      if (musicArray[i].musicVolume <= -0.02f)
      {
        StopMusic(musicArray[i].musicName);
        musicArray[i].musicEnable = false;
      }
    }
  }

  public void Update()
  {
    UpdateAudioSourcesVolume();
  }

  public void PlayMusic(string music_name)
  {
    for (int i = 0; i < musicArray.Length; i++)
    {
      if (musicArray[i].musicName == music_name){
        musicArray[i].MusicPlay();
        return;
      }
    }

    //Dont find the SFX
    Debug.LogWarning("AudioManager: Sound not found in the array: " + music_name);
  }

  public void PlaySFX(string sfx_name)
  {
    for (int i = 0; i < sfxs.Length; i++)
    {
      if (sfxs[i].sfxName == sfx_name){
        sfxs[i].SFXPlay();
        return;
      }
    }

    //Dont find the SFX
    Debug.LogWarning("AudioManager: Sound not found in the array: " + sfx_name);
  }

  public void StopMusic(string music_name)
  {
    for (int i = 0; i < musicArray.Length; i++)
    {
      if (musicArray[i].musicName == music_name){
          musicArray[i].MusicStop();
          return;
      }
    }

    //Dont find the SFX
    Debug.LogWarning("AudioManager: Sound not found in the array: " + music_name);
  }

  public void StopSFX(string sfx_name)
  {
    for (int i = 0; i < sfxs.Length; i++)
    {
      if (sfxs[i].sfxName == sfx_name) {
        sfxs[i].SFXStop();
        return;
      }
    }

    //Dont find the SFX
    Debug.LogWarning("AudioManager: Sound not found in the array: " + sfx_name);
  }


  public void MuteMusic()
  {
    muteMusicAlive = true;
    for (int i = 0; i < musicArray.Length; i++){
        musicArray[i].musicVolume = 0;
    }
  }

  public void DesMuteMusic()
  {
    muteMusicAlive = false;
    for (int i = 0; i < musicArray.Length; i++){
        musicArray[i].musicVolume = 1;
    }
  }

  public void MuteSFX()
  {
    muteSFXAlive = true;
    for (int i = 0; i < sfxs.Length; i++){
        sfxs[i].sfxVolume = 0;
    }
  }

  public void DesMuteSFX()
  {
    muteSFXAlive = false;
    for (int i = 0; i < sfxs.Length; i++){
        sfxs[i].sfxVolume = 1;
    }
  }
  
    /*
    //CALLERS

    public  void FadeInCaller(string name, float speed, float maxVolume)
    {
        instance.StartCoroutine(FadeIn(name, speed, maxVolume));
    }

    public  void FadeOutCaller(string name, float speed)
    {
        instance.StartCoroutine(FadeOut(name, speed));
    }


    //COROUTINES

     IEnumerator FadeIn (string name, float speed, float maxVolume)
    {
        keepFadingIn = true;
        keepFadingOut = false;

        for (int i = 0; i < musicArray.Length; i++)
        {
            if (musicArray[i].musicName == name)
            {
                musicArray[i].musicVolume = 0;
                float audioVolume = musicArray[i].musicVolume;

                while (musicArray[i].musicVolume < maxVolume && keepFadingIn)
                {
                    
                    audioVolume += speed;
                    musicArray[i].musicVolume = audioVolume;
                    yield return new WaitForSeconds(0.1f);
                }
            }

        }
    }


    IEnumerator FadeOut(string name, float speed)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        for (int i = 0; i < musicArray.Length; i++)
        {
            if (musicArray[i].musicName == name)
            {
                float audioVolume = musicArray[i].musicVolume;

                while (musicArray[i].musicVolume > -0.3f && keepFadingOut)
                {
                    audioVolume -= speed;
                    musicArray[i].musicVolume = audioVolume;
                    Debug.Log(musicArray[i].musicVolume);
                    yield return new WaitForSeconds(0.1f);
                }
            }

        }
    }*/
}
