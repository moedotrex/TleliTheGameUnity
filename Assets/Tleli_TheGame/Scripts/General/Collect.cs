using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    //TlelliFlameHealth tlelliFlame;
    TleliHealth tlelliFlame;
    PlayerController playercontroller;
    PlayerDash playerDash;
    LightCombo charged;
    public int recoverAmount;
    // Start is called before the first frame update
    void Start()
    {
        //tlelliFlame = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        tlelliFlame = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();
        playercontroller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerDash = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDash>();
        charged = GetComponent<LightCombo>();
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

        if (other.CompareTag("ChargedLA"))
        {
            charged.gotCharged = true;
            Destroy(other.gameObject);
        }
    }
}
