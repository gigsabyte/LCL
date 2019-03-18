using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    [SerializeField]
    private FadeOut fadeout;

    [SerializeField]
    private string path;

    private StreamReader reader;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        path = "Assets/Misc/Prologue.txt";
        text = gameObject.GetComponent<Text>();

        reader = new StreamReader(path);
        StartCoroutine(ReadText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ReadText()
    {
        float diagnum = 1;
        text.text = "";
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        string nextline = reader.ReadLine();
        while(nextline != null)
        {
            if(nextline == "h")
            {
                yield return new WaitForSeconds(1f * diagnum);
                while(text.color.a > 0)
                {
                    text.color = new Color(text.color.r, text.color.g, 
                    text.color.b, text.color.a - 0.1f);
                    yield return new WaitForSeconds(0.1f);
                }
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                text.text = "";
                diagnum = 0;
                nextline = reader.ReadLine();
            }
            else
            {
                while (nextline != null && nextline != "h")
                {
                    text.text = text.text + nextline + "\n";
                    diagnum++;
                    nextline = reader.ReadLine();
                    Debug.Log(nextline);
                    if (nextline == "h") break;
                }
                if (text.color.a < 1)
                {
                    while (text.color.a < 1)
                    {
                        text.color = new Color(text.color.r, text.color.g,
                        text.color.b, text.color.a + 0.1f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                }
            }
        }
        yield return new WaitForSeconds(1f);
        fadeout.Transition();
    }
}
