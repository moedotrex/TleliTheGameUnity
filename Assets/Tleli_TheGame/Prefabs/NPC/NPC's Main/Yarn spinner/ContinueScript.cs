using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    //Almacena valor del componente UI del dialogo para manejar las opciones
    public Yarn.Unity.DialogueUI dialogueUI;
    //Almacena valor de la opcion actualmente seleccionada
    private int currentOption = 0;
    //Almacena la cantidad de opciones actualmente disponibles
    private int numOptions;
    //Almacena referencias a los botones de las cuatro opciones
    public List<UnityEngine.UI.Button> options;

    void Update()
    {
        //Si se presiona F
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Se continua el dialogo
            dialogueUI.MarkLineComplete();
        }

        //Si se presiona arriba o izquierda
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Si se esta en la primer opcion...
            if (currentOption == 1)
            {
                //Se debe marcar la ultima
                currentOption = numOptions;
            }
            //Si no...
            else
            {
                //Se debe marcar la anterior
                currentOption--;

            }
            //Se lleva a cabo la seleccion en la interfaz
            options[currentOption - 1].Select();
        }
        //Si se presiona abajo o derecha
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Si se esta en la ultima opcion
            if (currentOption == numOptions)
            {
                //Se debe marcar la primera
                currentOption = 1;
            }
            //Si no...
            else
            {
                //Se debe marcar la siguiente
                currentOption++;

            }
            //Se lleva a cabo la seleccion en la interfaz
            options[currentOption - 1].Select();
        }
    }

    //Funcion que prepara todo para hacer la seleccion de acciones con las flechas
    //Se manda llamar desde el DialogueRunner del evento OnOptionsStart()
    public void setUpOptions()
    {
        //Se debe marcar la opcion 1 como la actual
        currentOption = 1;
        //Se lleva a cabo la seleccion de la opcion 1 en la interfaz
        options[0].Select();
        //Si la opcion 4 esta activa...
        if (options[3].gameObject.activeSelf)
        {
            //Quiere decir que hay cuatro opciones disponibles
            numOptions = 4;
        }
        //Si no...
        else
        {
            //Si la opcion 3 esta activa...
            if (options[2].gameObject.activeSelf)
            {
                //Quiere decir que hay tres opciones disponibles
                numOptions = 3;
            }
            //Si no...
            else
            {
                //Quiere decir que hay dos opciones disponibles
                numOptions = 2;
            }
        }
    }


}
