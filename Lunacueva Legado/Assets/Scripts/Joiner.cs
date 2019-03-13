using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joiner : MonoBehaviour
{
    [SerializeField]
    FollowerManager fm;

    [SerializeField]
    private bool destroyMe;

    // Start is called before the first frame update
    void Start()
    {
        destroyMe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyMe) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag == "Player")
        {
            fm.addFollower(transform.parent.gameObject);

            destroyMe = true;
            //foreach (Transform child in collision.gameObject.transform.parent)
            //{
            //    GameObject.Destroy(child.gameObject);
            //}
        }
    }
}
