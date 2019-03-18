using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private List<AudioSource> tracks;
    private List<int> fades;

    private int trackIndex;

    private bool isFading;

    private AudioSource lastFaded = null;

    // Start is called before the first frame update
    void Start()
    {

        tracks = new List<AudioSource>();
        fades = new List<int>();

        Component[] audiosources;

        audiosources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource track in audiosources)
        {
            tracks.Add(track);
            fades.Add(0);
        }



        trackIndex = tracks.Count;
        isFading = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DimNextTrack(int count)
    {
        if (count >= tracks.Count) return;

        StartCoroutine(FadeOut(tracks[count]));
        lastFaded = tracks[count];

    }

    public void RestoreLastTrack(int count)
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

    public void ReviveTrack(AudioSource aud)
    {
        StartCoroutine(FadeIn(aud));
    }
}
