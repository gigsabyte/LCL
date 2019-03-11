using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageMoths : MonoBehaviour
{
    [SerializeField]
    private float mothCount = 4;

    [SerializeField]
    private Text text;

    [SerializeField]
    private GameObject tilemap;

    private float cooldown = 3;

    private bool cdEnabled = false;

    [SerializeField]
    private Shader black;

    private Shader standard;

    private Renderer renderer;

    private Color bgcolor;

    private FollowerManager fm;

    // Start is called before the first frame update
    void Start()
    {
        renderer = tilemap.GetComponent<Renderer>();

        standard = renderer.material.shader;

        bgcolor = GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor;

        renderer.material.shader = black;
        renderer.material.SetColor("_Color", bgcolor);

        fm = gameObject.GetComponent<FollowerManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !cdEnabled)
        {
            fm.removeFollower();
            mothCount--;
            text.text = mothCount + " moths remaining";
            cdEnabled = true;

            renderer.material.shader = standard;
            renderer.material.SetColor("_Color", Color.white);
        }

        if (cdEnabled)
        {
            if(cooldown <= 0)
            {
                cdEnabled = false;
                cooldown = 3;
                tilemap.GetComponent<Renderer>().material.shader = black;
                renderer.material.SetColor("_Color", bgcolor);

                if(mothCount <= 0)
                {
                    gameObject.GetComponent<ChangeScene>().goToScene("Defeat");
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}
