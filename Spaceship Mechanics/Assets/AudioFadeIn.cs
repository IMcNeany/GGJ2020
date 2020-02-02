using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeIn : MonoBehaviour
{
    public AudioSource audio;
    private float max_volume = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        max_volume = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (audio.volume < max_volume)
        {


            audio.volume += 0.05f * Time.deltaTime;
        }
        
    }
}
