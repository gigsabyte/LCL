using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    void Update() {

        float x = tracker.transform.position.x - transform.position.x;
        float y = tracker.transform.position.y - transform.position.y;
        transform.up = new Vector2(x,y);

        if(Input.GetMouseButton(0)){
            Vector3 newpos = transform.up.normalized * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position + newpos, transform.position, Time.deltaTime * speed);
            animator.SetBool("IsFlying", true);
        }
        else {
            animator.SetBool("IsFlying", false);
        }

    }
}
