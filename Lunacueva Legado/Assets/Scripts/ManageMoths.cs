using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageMoths : MonoBehaviour
{
    [SerializeField]
    private float mothCount = 4;

    [SerializeField]
    private GameObject tilemap1;

    [SerializeField]
    private GameObject tilemap2;

    private float cooldown = 10;

    private bool cdEnabled = false;

    [SerializeField]
    private Shader black;

    private Shader[] standard = new Shader[2];

    private Renderer[] renderer = new Renderer[2];

    private Color bgcolor;
    
    [SerializeField]
    private Color scolor = new Color(.14f, .14f, .14f, 1f);

    private FollowerManager fm = null;

    // Start is called before the first frame update
    void Start()
    {
        renderer[0] = tilemap1.GetComponent<Renderer>();
        renderer[1] = tilemap2.GetComponent<Renderer>();

        standard[0] = renderer[0].material.shader;
        standard[1] = renderer[1].material.shader;

        bgcolor = GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor;

        for(int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.shader = black;
            renderer[i].material.SetColor("_Color", bgcolor);
        }
        

        fm = gameObject.GetComponent<FollowerManager>();


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !cdEnabled)
        {
            fm.removeFollower();
            mothCount--;
            cdEnabled = true;

            renderer[0].material.shader = standard[0];
            renderer[0].material.SetColor("_Color", scolor);

            renderer[1].material.shader = standard[1];
            renderer[1].material.SetColor("_Color", Color.white);
        }

        if (cdEnabled)
        {
            if(cooldown <= 0)
            {
                cdEnabled = false;
                cooldown = 10;
                for (int i = 0; i < renderer.Length; i++)
                {
                    renderer[i].material.shader = black;
                    renderer[i].material.SetColor("_Color", bgcolor);
                }

                if (mothCount <= 0)
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
