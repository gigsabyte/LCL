﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    private List<GameObject> followers = new List<GameObject>();

    [SerializeField]
    private GameObject follower = null;

    [SerializeField]
    private int followercount = 4;

    [SerializeField]
    private SoundManager sm;

    void Awake()
    {
        Vector3 updatedpos = transform.position;
        for(int i = 0; i < followercount; i++)
        {
            updatedpos.y -= 1;
            followers.Add(Instantiate(follower, updatedpos, transform.rotation));
            if (i == 0)
            {
                followers[i].GetComponent<FollowerMovement>().tracker = this.gameObject;
            }
            else
            {
                followers[i].GetComponent<FollowerMovement>().tracker = followers[i-1];
            }
        }
        gameObject.GetComponent<ParticleSystem>().Stop();
        followers[followercount - 1].GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createFollower()
    {
        Vector3 updatedpos = transform.position;

        updatedpos.y -= (followers.Count + 1);

        followers.Add(Instantiate(follower, updatedpos, transform.rotation));
        if (followers.Count == 0)
        {
            followers[0].GetComponent<PlayerMovement>().tracker = this.gameObject;
        }
        else
        {
            followers[followers.Count-1].GetComponent<PlayerMovement>().tracker = followers[followers.Count - 2];
        }
    }

    public void removeFollower()
    {
        if (followers.Count == 0) return;

        followers[followers.Count - 1].GetComponent<ParticleSystem>().Stop();

        followers[followers.Count - 1].GetComponent<FollowerKiller>().Kill();
        followers.RemoveAt(followers.Count - 1);

        if(followers.Count > 0)
        {
            followers[followers.Count - 1].GetComponent<ParticleSystem>().Play();
            sm.DimNextTrack();
        }
        else
        {
            gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}
