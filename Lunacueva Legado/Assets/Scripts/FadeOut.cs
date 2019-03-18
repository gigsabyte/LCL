using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private Animator fade;

    private ChangeScene cs;

    private Image img;

    [SerializeField]
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        fade = gameObject.GetComponent<Animator>();
        cs = gameObject.GetComponent<ChangeScene>();

        img = gameObject.GetComponent<Image>();
        img.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Transition()
    {
        img.enabled = true;
        fade.SetTrigger("FadeIn");
        StartCoroutine(WaitForFade());
    }

    private IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(0.9f);
        img.color = new Color(0, 5 / 255, 34 / 255, 1);
        cs.goToScene(sceneName);
    }
}
