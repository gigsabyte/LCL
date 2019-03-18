using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSlowdown : MonoBehaviour
{
    private Animation circle;

    [SerializeField]
    private float speed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        circle = gameObject.GetComponent<Animation>();

        circle["Circle"].speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
