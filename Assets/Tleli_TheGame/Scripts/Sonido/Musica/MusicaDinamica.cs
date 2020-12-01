using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaDinamica : MonoBehaviour
{
    FMOD.Studio.EventInstance Music; //cambio de música
    FMOD.Studio.EventInstance Ambiente;

    /*[FMODUnity.EventRef]
    public string inputAmbiente;
    [FMODUnity.EventRef]
    public string inputMusica;*/

    public bool tlelliEnCombate;


    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/OverworldMusic"); //ADRIAN cambio a nombre de evento en FMOD respectivo
        Music.start();
        Ambiente = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiente");
        Ambiente.start();
    }


    void Update()
    {

        if (tlelliEnCombate == true)
        {
            Music.setParameterByName("Music", 2f);
           
        }

        if (tlelliEnCombate == false)
        {
            Music.setParameterByName("Music", 1f);
            
        }

    }
}
