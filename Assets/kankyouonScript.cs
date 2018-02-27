using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class kankyouonScript : MonoBehaviour
{

    public AudioClip  sound_1;
    public AudioClip sound_2;
    public AudioClip sound_3;
    public AudioClip sound_4;
   public int SoundRand_;
   public float SoundRand2_;
     private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        SoundRand_ = Random.Range(0, 4);
        SoundRand2_ = Random.Range(5, 10);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SoundRand2_ -= Time.deltaTime;
        if (SoundRand2_ <= 0)
        {
            if (SoundRand_ == 0)
            {
                audioSource.clip = sound_1;
                audioSource.Play();
                SoundRand_ = Random.Range(0, 4);
                SoundRand2_ = Random.Range(5, 10);
            }
            else if (SoundRand_ == 1)
            {
                audioSource.clip = sound_2;
                audioSource.Play();
                SoundRand_ = Random.Range(0, 4);
                SoundRand2_ = Random.Range(5, 10);
            }
            else if (SoundRand_ == 2)
            {
                audioSource.clip = sound_3;
                audioSource.Play();
                audioSource.Play();
                SoundRand_ = Random.Range(0, 4);
                SoundRand2_ = Random.Range(5, 10);
            }
            else if (SoundRand_ == 3)
            {
                audioSource.clip = sound_4;
                audioSource.Play();
                SoundRand_ = Random.Range(0, 4);
                SoundRand2_ = Random.Range(5, 10);
            }
        }
    }
}
