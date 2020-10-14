using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotNPC : MonoBehaviour

{
    public GameObject player;


    public void TurnNPC()

    {

        Vector3 playerPos = player.transform.position;

        Vector3 npcPos = gameObject.transform.position;

        Vector3 delta = new Vector3(playerPos.x - npcPos.x, 0.0f, playerPos.z - npcPos.z);

        Quaternion rotation = Quaternion.LookRotation(delta);

        gameObject.transform.rotation = rotation;

    }

}
