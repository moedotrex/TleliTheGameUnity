using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsDebug : MonoBehaviour
{

    public PlayerController playerController;

    //Se establecen los valores a mostrar como atributos de la clase
    [DebugGUIGraph(r: 1, g: 0, b: 0, group: 1)]
    float verticalInput;

    //Se establecen los valores a mostrar como atributos de la clase
    [DebugGUIGraph(r: 0, g: 1, b: 0, group: 1)]
    float horizontalInput;

    [DebugGUIPrint, DebugGUIGraph(r: 1, g: 1, b: 1, group: 1)]
    float forwardSpeedTleli;

    [DebugGUIGraph(r: 0, g: 1, b: 0, group: 2)]
    float jumpInput;

    [DebugGUIGraph(r: 1, g: 1, b: 0, group: 2)]
    float JumpVelocity;

    //Pueden establecerse valores para mostrar en ventana de debug y como grafica
    [DebugGUIPrint, DebugGUIGraph(group: 3, r: 1, g: 0.3f, b: 0.3f)]
    float mouseX;
    [DebugGUIPrint, DebugGUIGraph(group: 3, r: 0, g: 1, b: 0)]
    float mouseY;

    // En el awake establecemos ciertos valores iniciales para mostrar en nuestra grafica como una funcion
    void Awake()
    {
        DebugGUI.SetGraphProperties("smoothFrameRate", "SmoothFPS", 0, 200, 0, new Color(0, 1, 1), false);
        DebugGUI.SetGraphProperties("frameRate", "FPS", 0, 200, 0, new Color(1, 0.5f, 1), false);

    }

    // En el update actualizaremos los datos a mostrar
    void Update()
    {
        //Actualizamos la informacion del input vertical y horizontal
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Obtiene con una funcion la velocidad de tleli
        forwardSpeedTleli = playerController.GetVelocity();

        //Obtiene la informacion del input de salto y la convierte en un float para la grafica
        jumpInput = Input.GetButtonDown("Jump") ? 1f : 0f;

        //Obtiene la velocidad del salto
        JumpVelocity = playerController.GetJumpVelocity();

        //Actualiza la informacion del mouse
        mouseX = Input.mousePosition.x / Screen.width;
        mouseY = Input.mousePosition.y / Screen.height;

        //Muestra un log en la ventana de debug cuando se hace un clic, asi como su ubicacion relativa (de 0 a 1.0) en pantalla
        if (Input.GetMouseButtonDown(0))
        {
            DebugGUI.Log(string.Format(
                "Mouse clicked! ({0}, {1})",
                mouseX.ToString("F3"),
                mouseY.ToString("F3")
            ));
        }

        //Muestra el framerate actual en la ventana de debug
        DebugGUI.LogPersistent("smoothFrameRate", "SmoothFPS: " + (1 / Time.deltaTime).ToString("F3"));
        DebugGUI.LogPersistent("frameRate", "FPS: " + (1 / Time.smoothDeltaTime).ToString("F3"));

        //Actualiza en la grafica el framerate
        if (Time.smoothDeltaTime != 0)
            DebugGUI.Graph("smoothFrameRate", 1 / Time.smoothDeltaTime);
        if (Time.deltaTime != 0)
            DebugGUI.Graph("frameRate", 1 / Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Aqui puedes colocar la informacion que necesites mostrar en el fixed update
    }

    void OnDestroy()
    {
        // Aqui se limpia la memoria cuando se destruya el objeto
        DebugGUI.RemoveGraph("frameRate");
        DebugGUI.RemoveGraph("fixedFrameRateSin");

        DebugGUI.RemovePersistent("frameRate");
    }
}
