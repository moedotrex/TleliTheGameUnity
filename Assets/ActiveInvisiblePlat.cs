using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInvisiblePlat : MonoBehaviour
{
    // Start is called before the first frame update

    Renderer rend;
    Collider myCollider;
    TwinkyFollow detecter;

    void Start()
    {
        detecter = GameObject.FindGameObjectWithTag("Ikni").GetComponent<TwinkyFollow>();
        rend = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();
    }

    public void active()
    {
        rend.enabled = true;
        myCollider.enabled = true;
        gameObject.tag = "Untagged";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AOE_Slow"))
        {
            active();
            detecter.alertActive = false;
            detecter.alertIcon.SetActive(false);
        }
    }

}

