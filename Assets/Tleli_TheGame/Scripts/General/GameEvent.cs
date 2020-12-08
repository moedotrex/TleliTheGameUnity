using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameEvent : MonoBehaviour
{
    public string eventName;

    public Text taskText;
    public static int llaves = 0;
    public static bool gotLlave = false;
    public float esperaLlave;

    public static int ikniEnemies = 3;
    public static bool ikniSalvado = false;
    public TwinkyFollow ikniFollow;
    public static bool gotDash = false;
    public PlayerDash tleli;

    public LightCombo tleliLA;
    public static bool gotBreakWalls = false;
    public Image breakWallsUnlock;
    public static bool unlockedBreakWalls = false;
    private float timer = 0f;
    public float pausaTime;

    // Start is called before the first frame update
    void Start()
    {
        print("Enemigos faltantes: " + ikniEnemies);

        ikniFollow.speed = 0f;
        ikniFollow.Hold = true;
        ikniFollow.following = false;
        ikniFollow.tleliCloseness = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ikniEnemies < 3 && ikniSalvado==false)
        {
            TutorialSaveIkni();
        }
        if (gotDash)
        {
            tleli.gotDash = true;
            gotDash = false;
        }

        if (gotLlave)
        {
            LlaveHeavyBoi();
        }
        if (gotBreakWalls)
        {
            tleliLA.gotCharged = true;
        }

        //GOT CHARGE ATTACK
        if (unlockedBreakWalls)
        {
            print("Break Walls obtenido. Mostrar imagen dash now");
            breakWallsUnlock.enabled = true;
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                timer = 0f;
                breakWallsUnlock.enabled = false;
                unlockedBreakWalls = false;
            }
        }
    }

    /*

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChargedLA"))
        {
            print("Break Walls obtenido. Mostrar imagen dash now");
            tleliLA.gotCharged = true;
            ShowUnlock.gotBreakWalls = true;
        }
        */
            /*
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
            */
        //}

    public void LlaveHeavyBoi()
    {
        llaves++;
        switch (llaves)
        {
            case 1:
                taskText.text = "Get the other key defeating the Miniboss - (1/2)";
                break;
            case 2:
                taskText.text = "Return to the settlement - (2/2)";
                print("BOSS DOOR ABIERTA");
                break;
        }
        gotLlave = false;
    }

    public void TutorialSaveIkni()
    {
        //ikniEnemies--;
        print("Enemigos faltantes: "+ikniEnemies);
        if (ikniEnemies == (ikniEnemies - ikniEnemies))
        {
            print("SALVASTE A IKNI!!!");

            ikniFollow.speed = 6f;
            ikniFollow.Hold = false;
            ikniFollow.following = true;
            ikniFollow.tleliCloseness = 15f;

            ikniSalvado = true;

            DialogueTleliIkni.dialogoIkniSalvado++;
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
