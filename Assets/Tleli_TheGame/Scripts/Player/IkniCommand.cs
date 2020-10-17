using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkniCommand : MonoBehaviour
{

    TwinkyFollow ikniFollow;
    // Start is called before the first frame update
    void Start()
    {
        ikniFollow = GameObject.FindGameObjectWithTag("Ikni").GetComponent<TwinkyFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (ikniFollow.following == true)
            {
                ikniFollow.speed = 0f;
                ikniFollow.following = false;
                ikniFollow.Hold = true;
            }
            else if (ikniFollow.following == false)
            {
                ikniFollow.speed = 6f;
                ikniFollow.following = true;
                ikniFollow.Hold = false;
            }
        }
    }
}
