using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountObjects : MonoBehaviour
{
    public string nextLevel;
    public GameObject objToDestroy;
    GameObject objUI;
    // Start is called before the first frame update
    void Start()
    {
        objUI = GameObject.Find("ObjectNum");
    }

    // Update is called once per frame
    void Update()
    {
        objUI.GetComponent<Text>().text = ObjectCollect.objects.ToString();
        if (ObjectCollect.objects == 0 )
        {
            //Application.LoadLevel("nextLevel");
            //Destroy(objToDestroy);
            objUI.GetComponent<Text>().text = "All Objects Collected";
        }
    }
}
