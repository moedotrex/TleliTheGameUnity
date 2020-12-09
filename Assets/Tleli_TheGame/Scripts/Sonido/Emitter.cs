using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectSound;
    FMOD.Studio.EventInstance soundEvent;
    FMODUnity.StudioEventEmitter Sonido;

    public string SonidoLight;
   


    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);

        Sonido = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
        Sonido.Play();

    }
}
