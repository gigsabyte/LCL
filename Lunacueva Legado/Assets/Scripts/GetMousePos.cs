using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePos : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject moth;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        //pos.z = 1;
        transform.position = Camera.main.ScreenToWorldPoint(pos);
        pos = moth.transform.position;
        pos.z = -10;
        cam.gameObject.transform.position = Vector3.Lerp(cam.gameObject.transform.position, pos, Time.deltaTime);
    }
}
