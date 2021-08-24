using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEAudioSource : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPlayYes()
    {
        source.clip = clips[0];
        source.Play();
    }
    public void OnStageStart()
    {
        source.clip = clips[1];
        source.Play();
    }

    public void OnPlayNo()
    {
        source.clip = clips[2];
        source.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
