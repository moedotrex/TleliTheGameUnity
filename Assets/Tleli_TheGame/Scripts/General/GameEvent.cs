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
    //public static bool gotLlave = false;
    public Image llaveUnlock;
    public static bool unlockedLlave = false;
    public static bool isGettingKey = false;

    public static int ikniEnemies = 3;
    public static bool ikniSalvado = false;
    public TwinkyFollow ikniFollow;

    public LightCombo tleliLA;
    public static bool gotBreakWalls = false;
    public Image breakWallsUnlock;
    public static bool unlockedBreakWalls = false;
    private float timer = 0f;
    public float pausaTime;

    public PlayerDash tleli;
    public static bool gotDash = false;
    public Image dashUnlock;
    public static bool unlockedDash = false;

    // Start is called before the first frame update
    void Start()
    {
        print("Enemigos faltantes: " + ikniEnemies);

        ikniFollow.speed = 0f;
        ikniFollow.Hold = true;
        ikniFollow.following = false;
        ikniFollow.tleliCloseness = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ikniEnemies < 3 && ikniSalvado == false)
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
            isGettingKey = true;
        }
        if (gotBreakWalls)
        {
            tleliLA.gotCharged = true;
        }

        //GOT CHARGE ATTACK
        if (unlockedBreakWalls)
        {
            print("Break Walls obtenido. Mostrar imagen break walls now");
            breakWallsUnlock.enabled = true;
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                timer = 0f;
                breakWallsUnlock.enabled = false;
                unlockedBreakWalls = false;
                gotDash = false;
            }
        }

        //GOT DASH
        if (unlockedDash)
        {
            print("Dash obtenido. Mostrar imagen dash now");
            dashUnlock.enabled = true;
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                timer = 0f;
                dashUnlock.enabled = false;
                unlockedDash = false;
            }
        }

        //GOT KEY
        if (unlockedLlave)
        {
            print("Llave obtenida. Mostrar imagen llave now");
            llaveUnlock.enabled = true;
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                timer = 0f;
                llaveUnlock.enabled = false;
                unlockedLlave = false;
            }
        }
    }

    public void LlaveHeavyBoi()
    {
        llaves++;
        unlockedLlave = true;
        gotLlave = false;
        /*
        switch (llaves)
        {
            case 1:
                unlockedLlave = true;
                DialogueTleliIkni.cualLlave = 1;
                //taskText.text = "Get the other key defeating the Miniboss - (1/2)";
                break;
            case 2:
                unlockedLlave = true;
                DialogueTleliIkni.cualLlave = 2;
                //taskText.text = "Return to the settlement - (2/2)";
                print("BOSS DOOR ABIERTA");
                break;
        }
        */
        //gotLlave = false;
    }

    public void TutorialSaveIkni()
    {
        //ikniEnemies--;
        print("Enemigos faltantes: " + ikniEnemies);
        if (ikniEnemies == (ikniEnemies - ikniEnemies))
        {
            print("SALVASTE A IKNI!!!");

            ikniFollow.speed = 5f;
            ikniFollow.Hold = false;
            ikniFollow.following = true;
            ikniFollow.tleliCloseness = 15f;

            ikniSalvado = true;

            DialogueTleliIkni.dialogoIkniSalvado++;
        }
    }
}
