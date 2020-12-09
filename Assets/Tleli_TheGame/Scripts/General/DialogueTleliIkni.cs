using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTleliIkni : MonoBehaviour
{
    public string[] dialogos;
    public string[] personaje;
    private int dialogosIndex = 0;
    bool TimeStarted = false;
    private float timer = 0f;
    public float pausaTime;

    public static int dialogoIkniSalvado = 0;
    public bool isDialogoIkni = false;

    public Text taskText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //TRIGGER DE LIBERAR A IKNI+OBTENER DASH
        if (isDialogoIkni == true && dialogoIkniSalvado == 1)
        {
            Collider thisCollider = this.GetComponent<Collider>();
            thisCollider.enabled = false;
            dialogoIkniSalvado = 2;

            //dashUnlock.enabled = true;
            changeDialogue();
        }

        //CONTADOR PARA ESPERAR CIERTO TIEMPO ENTRE DIÁLOGOS
        if (TimeStarted)
        {
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                dialogosIndex++;
                TimeStarted = false;
                timer = 0f;

                if (dialogosIndex == dialogos.Length)
                {

                    if (dialogoIkniSalvado == 2)
                    {
                        GameEvent.gotDash = true;
                        GameEvent.unlockedDash = true;
                        dialogoIkniSalvado = 3;
                    }

                    taskText.text = " ";
                } else if (dialogosIndex < dialogos.Length)
                {
                    changeDialogue();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && isDialogoIkni == false)
        {
            Collider thisCollider = this.GetComponent<Collider>();
            thisCollider.enabled = false;
            changeDialogue();
        }
    }

    public void changeDialogue()
    {
        taskText.text = dialogos[dialogosIndex];

        Color tleliTextColor = new Color(1f, 0.7098039f, 0.4980392f, 1f);
        Color ikniTextColor = new Color(0.4980392f, 0.8983116f, 1f, 1f);
        switch (personaje[dialogosIndex])
        {
            case "T":
                taskText.color = tleliTextColor;
                break;
            case "I":
                taskText.color = ikniTextColor;
                break;
        }
        if (dialogosIndex < dialogos.Length)
        {
            if (!TimeStarted)
            {
                TimeStarted = true;
            }
        }

    }
}