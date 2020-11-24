using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTleliIkni : MonoBehaviour
{
    public string[] dialogos;
    private int dialogosIndex = 0;
    bool TimeStarted = false;
    private float timer = 0f;
    public float pausaTime;

    public string eventName;
    public Text taskText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TimeStarted)
        {
            timer += Time.deltaTime;

            if (timer >= pausaTime)
            {
                dialogosIndex++;
                TimeStarted = false;
                timer = 0f;
                if (dialogosIndex < dialogos.Length)
                {
                    changeDialogue();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider thisCollider = this.GetComponent<Collider>();
            switch (this.eventName)
            {
                case "TleliIntro":
                    thisCollider.enabled = false;
                    changeDialogue();
                    break;
                case "Dialogue_TleliIkni1":
                    thisCollider.enabled = false;
                    changeDialogue();
                    break;
                case "Dialogue_TleliIkni2":
                    thisCollider.enabled = false;
                    changeDialogue();
                    break;
                case "Dialogue_Ikni":
                    thisCollider.enabled = false;
                    changeDialogue();
                    break;
            }

        }
    }

    void changeDialogue()
    {
        taskText.text = dialogos[dialogosIndex];
        if (dialogosIndex < dialogos.Length)
        {
            if (!TimeStarted)
            {
                TimeStarted = true;
            }
        }

    }
}