using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] sounds = null;
    public static AudioManager instance = null;
    [HideInInspector]
    public bool isThemePlaying = false;

    private void Awake()
    {
        if (MakeSingleton())
        {
            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.pitch = sound.pitch;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;
            }
        }
    }
    private bool MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }
    public void Play(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.name.Equals(name)) continue;
            sound.source.Play();
            return;
        }
        Debug.LogWarning("Couldn't find sound with name " + name);
    }

    public void Play(string name, float secs)
    {
        IEnumerator WaitForSecsPlayTheme()
        {
            yield return new WaitForSeconds(secs);
            Play(name);
        }
        StartCoroutine(WaitForSecsPlayTheme());
    }

    public void Stop(string name)
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.name.Equals(name)) continue;
            sound.source.Stop();
            return;
        }
        Debug.LogWarning("Couldn't find sound with name " + name);
    }
}
