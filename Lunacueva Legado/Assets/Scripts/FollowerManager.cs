using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    private List<GameObject> followers = new List<GameObject>();

    [SerializeField]
    private GameObject follower = null;

    [SerializeField]
    private float followercount = 4;

    void Awake()
    {
        Vector3 updatedpos = transform.position;
        for(int i = 0; i < followercount; i++)
        {
            updatedpos.y -= 1;
            followers.Add(Instantiate(follower, updatedpos, transform.rotation));
            if (i == 0)
            {
                followers[i].GetComponent<PlayerMovement>().tracker = this.gameObject;
            }
            else
            {
                followers[i].GetComponent<PlayerMovement>().tracker = followers[i-1];
            }
        }
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

        followers[followers.Count - 1].GetComponent<FollowerKiller>().Kill();
        followers.RemoveAt(followers.Count - 1);
    }
}
