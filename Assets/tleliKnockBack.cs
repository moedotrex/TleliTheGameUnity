using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tleliKnockBack : MonoBehaviour
{

    PlayerController moveScript;
    public GameObject player;
    Vector3 moveDir;

    public float knockBackTime;
    public float knockBackForce;


    void Start()
    {
        moveScript = GetComponent<PlayerController>();
    }

    public void startKnockBack()
    {
        StartCoroutine(knockBack());
    }

    IEnumerator knockBack()
    {
        float startTime = Time.time;
        while (Time.time < startTime + knockBackTime)
        {
            moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward * -1f;
            moveScript.characterController.Move(moveDir * knockBackForce * Time.deltaTime);
            moveScript.isDisplaced = true;
            yield return null;
            moveScript.isDisplaced = false;
        }
    }
}
