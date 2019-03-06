using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private GameObject tracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        /*float step = speed * Time.deltaTime;

        float relativeMouseX = transform.position.x + Input.mousePosition.x / Screen.width;
        float relativeMouseY = transform.position.y + Input.mousePosition.y / Screen.height;
        Debug.Log(relativeMouseX + " " + relativeMouseY);
        Vector3 rotation = new Vector3(relativeMouseX, relativeMouseY, transform.position.z);*/

        //Vector3 rotation = new Vector3(tracker.transform.position, rotation, step, 4.0f);
        float x = tracker.transform.position.x - transform.position.x;
        float y = tracker.transform.position.y - transform.position.y;
        transform.up = new Vector2(x,y);

        if(Input.GetKey(KeyCode.Z))
        {
            Vector3 newpos = transform.up.normalized * speed * Time.deltaTime;
            transform.position = transform.position + newpos;
        }

    }
}
