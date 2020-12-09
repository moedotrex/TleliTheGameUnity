using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTleli : MonoBehaviour
{
    PlayerController player;
    LightCombo combo;
 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        combo = GameObject.FindGameObjectWithTag("Player").GetComponent<LightCombo>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        player.isAnimating = false;
        combo.isAnimating = false;
    }

    public void Deactivate()
    {
        player.isAnimating = true;
        combo.isAnimating = true;
    }
}
