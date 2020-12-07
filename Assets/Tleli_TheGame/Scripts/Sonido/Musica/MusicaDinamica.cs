using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicaDinamica : MonoBehaviour
{
    public FMOD.Studio.EventInstance Music; //cambio de música
    public FMOD.Studio.EventInstance Ambiente;
    public int MusicDef;
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
        
        Ambiente = FMODUnity.RuntimeManager.CreateInstance("event:/AmbientesSelect");
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
            Music.setParameterByName("Music", MusicDef);

        }

        switch(MusicDef){
            case 0:
                 Music.setParameterByName("Music", 0);
                break;

            case 1:
                Music.setParameterByName("Music", 1);
                break;
            case 3:
                Music.setParameterByName("Music", 3);
                break;
            case 4:
                Music.setParameterByName("Music", 4);
                break;

            case 5:
                Music.setParameterByName("Music", 5);
                break;

        }

        }

    
    
}
