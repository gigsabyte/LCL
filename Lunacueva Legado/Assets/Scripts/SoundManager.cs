using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> tracks;
    private int trackIndex;

    // Start is called before the first frame update
    void Start()
    {

        tracks = new List<AudioSource>();

        Component[] audiosources;

        audiosources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource track in audiosources)
        {
            tracks.Add(track);
        }

        trackIndex = tracks.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DimNextTrack()
    {
        trackIndex--;
        if(trackIndex < 0)
        {
            trackIndex = 0;
            return;
        }
        StartCoroutine(FadeOut(tracks[trackIndex]));

    }

    private IEnumerator FadeOut(AudioSource aud)
    {
        while(aud.volume > 0)
        {
            aud.volume -= .1f;
            yield return new WaitForSeconds(.1f);
        }

        aud.volume = 0;
    }

    private IEnumerator FadeIn(AudioSource aud)
    {
        while (aud.volume < 1)
        {
            aud.volume += .1f;
            yield return new WaitForSeconds(.1f);
        }

        aud.volume = 1;
    }
}
