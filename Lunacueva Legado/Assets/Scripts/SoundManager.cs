using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> tracks;
    private int trackIndex;

    private bool isFading;

    private AudioSource lastFaded = null;

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
        isFading = false;
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
        lastFaded = tracks[trackIndex];

    }

    public void RestoreLastTrack()
    {
        trackIndex = tracks.IndexOf(lastFaded);
        if (trackIndex < 0)
        {
            trackIndex = 0;
            return;
        } else if (trackIndex >= tracks.Count -1)
        {
            trackIndex = tracks.Count - 1;
        }
        StartCoroutine(FadeIn(lastFaded));
    }

    private IEnumerator FadeOut(AudioSource aud)
    {
        isFading = true;
        while(aud.volume > 0)
        {
            aud.volume -= .1f;
            yield return new WaitForSeconds(.1f);
        }

        aud.volume = 0;
        isFading = false;
    }

    private IEnumerator FadeIn(AudioSource aud)
    {
        if (isFading) yield return new WaitForSeconds(1f);
        while (aud.volume < 1)
        {
            aud.volume += .1f;
            yield return new WaitForSeconds(.1f);
        }

        aud.volume = 1;
    }
}
