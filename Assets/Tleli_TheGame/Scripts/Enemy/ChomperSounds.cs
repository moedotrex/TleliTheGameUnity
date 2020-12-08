using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChomperSounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectSound;
    FMOD.Studio.EventInstance soundEvent;
    FMODUnity.StudioEventEmitter Sonido1;
    public string SonidoLight;
    public string SonidoCharged;
    public string SonidoNotice;
    public string SonidoDeath;


    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);
        soundEvent.start();
        Sonido1 = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();

    }

    // Update is called once per frame
    void Update()
    {
        // FMODUnity.RuntimeManager.AttachInstanceToGameObject(GetComponent<Transform>();
        //HeavyAttack();

    }

    public void Chompligth()
    {

        
        Sonido1.Event = SonidoLight;
        Sonido1.Play();

    }

    public void ChompCharged()
    {
        
        Sonido1.Event = SonidoCharged;
        Sonido1.Play();
    }

    public void ReactionChomp()
    {
        
        Sonido1.Event = SonidoNotice;
        Sonido1.Play();
    }

    public void ChompDead()
    {
        
        Sonido1.Event = SonidoDeath;
        Sonido1.Play();
    }

}
