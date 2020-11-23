using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tleliKnockBack : MonoBehaviour
{

    PlayerController moveScript;
    public GameObject player;
    Vector3 moveDir;

    public float knockBackTime;
    


    void Start()
    {
        moveScript = GetComponent<PlayerController>();
    }

    public void startKnockBack(float force)
    {
        StartCoroutine(knockBack(force));
    }

    IEnumerator knockBack(float force)
    {
        float startTime = Time.time;
        while (Time.time < startTime + knockBackTime)
        {
            moveDir = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * Vector3.forward * -1f;
            moveScript.characterController.Move(moveDir * force * Time.deltaTime);
            moveScript.isDisplaced = true;
            yield return null;
            moveScript.isDisplaced = false;
        }
    }
}
