using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEvent : MonoBehaviour
{
    public string eventName;
    public Text tutorialBox;
    public string tutorialText;
    bool isTutorialTextShowing = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.eventName == "Tutorial_Jump"
            && isTutorialTextShowing)
        {
            tutorialBox.text = "";
            isTutorialTextShowing = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) &&
          this.eventName == "Tutorial_DoubleJump"
          && isTutorialTextShowing)
        {
            tutorialBox.text = "";
            isTutorialTextShowing = false;
        } else if (Input.GetKeyDown(KeyCode.Mouse0) &&
            this.eventName == "Tutorial_Attack"
            && isTutorialTextShowing)
        {
            tutorialBox.text = "";
            isTutorialTextShowing = false;
        } else if (Input.GetKeyDown(KeyCode.LeftShift) &&
            this.eventName == "Tutorial_Dash"
            && isTutorialTextShowing)
        {
            tutorialBox.text = "";
            isTutorialTextShowing = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collider thisCollider = this.GetComponent<Collider>();
            switch (this.eventName)
            {
                case "Tutorial_Jump":
                    thisCollider.enabled = false;

                    tutorialBox.text = tutorialText;
                    isTutorialTextShowing = true;
                    break;
                case "Tutorial_DoubleJump":
                    thisCollider.enabled = false;

                    tutorialBox.text = tutorialText;
                    isTutorialTextShowing = true;
                    break;
                case "Tutorial_Attack":
                    thisCollider.enabled = false;

                    tutorialBox.text = tutorialText;
                    isTutorialTextShowing = true;
                    break;
                case "Tutorial_Dash":
                    thisCollider.enabled = false;

                    tutorialBox.text = tutorialText;
                    isTutorialTextShowing = true;
                    break;
            }

        }
    }
}
