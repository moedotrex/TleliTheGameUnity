using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUnlock : MonoBehaviour
{
    //public Image breakWallsUnlock;
    //public bool unlockedBreakWalls = false;
    //public LightCombo tleliLA;

    //private float timer = 0f;
    //public float pausaTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (unlockedBreakWalls)
        {
            breakWallsUnlock.enabled = true;
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                timer = 0f;
                breakWallsUnlock.enabled = false;
                unlockedBreakWalls = false;
            }
        }
        */
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvent.gotBreakWalls = true;
            GameEvent.unlockedBreakWalls = true;
        }
    }
}
