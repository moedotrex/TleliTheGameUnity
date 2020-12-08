using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectSound;
    
    private FMOD.Studio.EventInstance soundEvent;

    FMODUnity.StudioEventEmitter Sonido1;
    
    public string SonidoAttack;
    public string SonidoSlam;
    public string SonidoNotice;
    public string SonidoDeath;


  

    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);
     
        
        Sonido1 = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
        

    }


    void Update()
    {
        //soundEvent.set3DAttributes(FMODUnity.RuntimeManager.To3DAttributes(gameObject));

    

    }
    // Update is called once per frame


    public void HeavyAttack()
    {
        
       Sonido1.Event = SonidoAttack;
         Sonido1.Play();

    }

    public void HeavySlam()
    {
      
        Sonido1.Event = SonidoSlam;
              Sonido1.Play();
    }

    public void ReactionHeavy()
    {
        
        Sonido1.Event = SonidoNotice;
        Sonido1.Play();
    }

    public void heavyDead()
    {
       
        Sonido1.Event = SonidoDeath;
        Sonido1.Play();
    }

}
