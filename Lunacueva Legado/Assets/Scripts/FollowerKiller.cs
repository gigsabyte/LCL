using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerKiller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        StartCoroutine(Die()); // moths die when they are killed
    }

    private IEnumerator Die()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
