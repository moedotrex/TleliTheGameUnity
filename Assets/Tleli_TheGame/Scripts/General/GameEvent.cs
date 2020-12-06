using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameEvent : MonoBehaviour
{
    public string eventName;
    public Text taskText;
    int llaves = 0;
    public GameObject ikniFollow;
    public float esperaLlave;
    public int ikniEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {
        print("Enemigos faltantes: " + ikniEnemies);
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider thisCollider = this.GetComponent<Collider>();
            switch (this.eventName)
            {
                case "TleliIntro":
                    ikniFollow.SetActive(true);
                    taskText.text = "Go into the cave and find Ikni";
                    thisCollider.enabled = false;
                    break;
                case "FindIkni":
                    ikniFollow.SetActive(true);
                    taskText.text = "Investigate the glowing weapon";
                    thisCollider.enabled = false;
                    break;
                case "GetDash":
                    taskText.text = "Find the cave's end";
                    thisCollider.enabled = false;
                    break;
                case "ReachHub":
                    taskText.text = "Jump and reach the settlement";
                    thisCollider.enabled = false;
                    break;
                case "TalkNPC":
                    taskText.text = "Talk to an inhabitant of the settlement";
                    thisCollider.enabled = false;
                    break;
                case "EnteredRuins":
                    taskText.text = "Get the two keys defeating the Miniboss - (0/2)";
                    thisCollider.enabled = false;
                    break;
                case "SpearRoom":
                    taskText.text = "Reach the weapon on top of the column";
                    thisCollider.enabled = false;
                    break;
                case "GetBreakWalls":
                    taskText.text = "Get the two keys defeating the Miniboss - (0/2)";
                    thisCollider.enabled = false;
                    break;
                case "GotKey":
                    if (llaves == 1)
                    {
                        taskText.text = "Return to the settlement - (2/2)";
                        llaves++;
                        thisCollider.enabled = false;
                    }
                    if (llaves == 0)
                    {
                        taskText.text = "Get the other key defeating the Miniboss - (1/2)";
                        llaves++;
                        thisCollider.enabled = false;
                    }
                    break;
                case "GoBossDoor":
                    if (llaves == 2)
                    {
                        taskText.text = "Go to the Door behind the town mount";
                        thisCollider.enabled = false;
                    }
                    break;
                case "BossRoom":
                    SceneManager.LoadScene("BossCutscene");
                    break;
            }
        }
    }
    */

    public void LlaveHeavyBoi()
    {
        switch (llaves)
        {
            case 0:
                taskText.text = "Get the other key defeating the Miniboss - (1/2)";
                llaves++;
                break;
            case 1:
                taskText.text = "Return to the settlement - (2/2)";
                llaves++;
                break;
        }
    }

    public void TutorialSaveIkni()
    {
        ikniEnemies--;
        print("Enemigos faltantes: "+ikniEnemies);
        if (ikniEnemies == (ikniEnemies - ikniEnemies))
        {
            print("SALVASTE A IKNI!!!");
        }
    }

    void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        if (this.eventName == "GetKeys" && llaves < 2)
        {
            taskText.text = "Go to the temple ruins past the mushrooms";
        }
    }
}
}
