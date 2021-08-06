using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall_Dash : MonoBehaviour
{
    public PlayerDash Tleli;

    void Update()
    {

        if(Tleli.gotDash == true)
        {
            Destroy(gameObject);
        }

    }
}
