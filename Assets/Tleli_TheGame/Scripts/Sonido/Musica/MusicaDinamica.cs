using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaDinamica : MonoBehaviour
{
    public FMOD.Studio.EventInstance Music; //cambio de música
    public FMOD.Studio.EventInstance Ambiente;

    /*[FMODUnity.EventRef]
    public string inputAmbiente;
    [FMODUnity.EventRef]
    public string inputMusica;*/

    public bool tlelliEnCombate;


    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/OverworldMusic"); //ADRIAN cambio a nombre de evento en FMOD respectivo
        Music.start();
        Music.setVolume(PlayerPrefs.GetFloat("GameVolume", 1f));

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
