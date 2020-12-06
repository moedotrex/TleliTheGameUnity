using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaDinamica : MonoBehaviour
{
    public FMOD.Studio.EventInstance Music; //cambio de música
    public FMOD.Studio.EventInstance Ambiente;

    public int musicSelect;
    /*[FMODUnity.EventRef]
    public string inputAmbiente;
    [FMODUnity.EventRef]
    public string inputMusica;*/

    public bool tlelliEnCombate;
    public float VolumeMusic = 1f;

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/OverworldMusic"); //ADRIAN cambio a nombre de evento en FMOD respectivo
        Music.start();
        Music.setVolume(PlayerPrefs.GetFloat("GameVolume", VolumeMusic));

        Ambiente = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiente");
        Ambiente.start();
        
    }


    void Update()
    {

        if (tlelliEnCombate == true)
        {
            Music.setParameterByName("Music", 6);
           
        }

        if (tlelliEnCombate == false)
        {
            Music.setParameterByName("Music", 1);
            
        }

        switch (musicSelect)
        {
            case 0:
                Music.setParameterByName("Music", 1);
                break;
            case 1:
                Music.setParameterByName("Music", 2);
                break;
        }
        
    }

    
    
}
