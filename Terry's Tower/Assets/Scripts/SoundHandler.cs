using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {
    public AudioClip[] music;
    public AudioClip[] soundEffect;

    public GameObject musicSourceHolder;
    public GameObject soundEffectSourceHolder;
    public AudioSource musicSource;
    public AudioSource soundEffectSource;
	// Use this for initialization
	void Start () {
        musicSource = musicSourceHolder.GetComponent<AudioSource>();
        soundEffectSource = soundEffectSourceHolder.GetComponent<AudioSource>();

        Globals.soundHandler = gameObject.GetComponent<SoundHandler>();
        Globals.soundEffectSource = soundEffectSource;
        Globals.soundEffectVolume = (int)(soundEffectSource.volume*10);
        Globals.musicSource = musicSource;
        Globals.musicVolume = (int)(musicSource.volume*10);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetMusic()
    {
        //float.TryParse(SceneManager.GetActiveScene().name, out Globals.currentFloor);
        // set song for floors
        Debug.Log("Changing Song");
        Debug.Log(Globals.currentMap);
        int songNumber = int.Parse(Globals.currentMap.Substring(0, 2));

        try
        {
            if (musicSource.clip != music[songNumber])
            {
                musicSource.Stop();
                musicSource.clip = music[songNumber];
                musicSource.Play();
            }
        }
        catch
        {
            Debug.Log("No Available music");
            musicSource.Stop();
            musicSource.clip = null;
        }
    }

    public void ForceSong(int songNumber)
    {
        try
        {
            if (musicSource.clip != music[songNumber])
            {
                musicSource.Stop();
                musicSource.clip = music[songNumber];
                musicSource.Play();
            }
        }
        catch
        {
            Debug.Log("No Available music");
            musicSource.Stop();
            musicSource.clip = null;
        }
    }
    public void PlayBossSong(int songNumber)
    {
        Debug.Log("PLay BoosSoooong");
            musicSource.clip = music[songNumber];
            musicSource.Play();
        
    }
}
