using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    TlelliFlameHealth tlelliFlame;
    PlayerController playercontroller;
    PlayerDash playerDash;
    public int recoverAmount;
    // Start is called before the first frame update
    void Start()
    {
        tlelliFlame = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerDash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDash>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blaze"))
        {
            tlelliFlame.RecoverFlame(recoverAmount);
            tlelliFlame.FlameUpdateMaterial();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Dash"))
        {
            playerDash.gotDash = true;
            Destroy(other.gameObject);
        }
    }
}
