using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NameSound
{
    Coin,
    CaputureScreen,
    CarIdle,
    CarRun,
    StartRun,
}
public class ManangeAudio : MonoBehaviour
{
    public static ManangeAudio Instacne;
    
    public Queue< AudioSource> audioSources = new Queue<AudioSource>();
    public List< AudioSource> audioSourcesTmp;
    AudioSource CurAudioSouce;
  
    public List<AudioClip> Sounds;
    private void Awake()
    {
        if (Instacne == null)
        {
            Instacne = this;
        }
    }
    public void Start()
    {
        foreach(AudioSource i in audioSourcesTmp)
            {
                audioSources.Enqueue(i);
            }
    }
   
   
    public void PlaySound(NameSound id , bool loop = false)
    {
        CurAudioSouce = audioSources.Dequeue();
        CurAudioSouce.PlayOneShot(Sounds[(int)id]);
        audioSources.Enqueue(CurAudioSouce);
      
    }
    public AudioSource NewLoopAudio(NameSound id, bool loop, float volume = 1f)
    {
        var ob = Instantiate(audioSourcesTmp[0]);
        ob.clip = Sounds[(int)id];
        ob.volume = volume;
        ob.loop = loop;
        ob.Play();
        return ob;
    }
    public AudioSource LoopAudio(NameSound id,int idS,  bool loop = true, float volume = 1f)
    {
        var ob = (audioSourcesTmp[idS]);
        ob.clip = Sounds[(int)id];
        ob.volume = volume;
        ob.loop = loop;
        ob.Play();
        return ob;
    }
}
