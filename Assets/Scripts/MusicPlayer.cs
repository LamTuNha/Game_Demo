using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{   

    public AudioSource loopSoure;
    // Start is called before the first frame update
    void Start()
    {
        loopSoure.PlayScheduled(AudioSettings.dspTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
