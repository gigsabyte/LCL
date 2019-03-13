using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    public GameObject tracker = null;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = tracker.transform.position.x - tracker.transform.forward.normalized.x;
        float y = tracker.transform.position.y - tracker.transform.forward.normalized.y;

        x = x - transform.position.x;
        y = y - transform.position.y;

        Vector2 moveVec = new Vector2(x, y);
        transform.up = moveVec;

        if(moveVec.magnitude >= 1)
        {
            Vector3 newpos = transform.up.normalized * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position + newpos, transform.position, Time.deltaTime * speed);
            animator.SetBool("IsFlying", true);
        }
        else
        {
            animator.SetBool("IsFlying", false);
        }
    }
}
