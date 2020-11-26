using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    CountObjects counter;
    public static int objects = 0;
    // Start is called before the first frame update
    void Awake ()
    {
        objects++;
        counter = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CountObjects>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
            //objects--;
            counter.UpisCount += 1;
        gameObject.SetActive(false);
    }
}
