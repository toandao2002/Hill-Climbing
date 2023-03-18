using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NameSound
{
    Coin,
    CaputureScreen,
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
 
}
