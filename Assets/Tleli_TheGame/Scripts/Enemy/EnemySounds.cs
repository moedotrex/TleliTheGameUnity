using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySounds : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectSound;
    FMOD.Studio.EventInstance soundEvent;

    
    // Start is called before the first frame update
    void Start()
    {
        soundEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);
        soundEvent.start();

    }

    // Update is called once per frame
    void Update()
    {
        // FMODUnity.RuntimeManager.AttachInstanceToGameObject(GetComponent<Transform>();
        HeavyAttack();

    }

    public void HeavyAttack()
    {

        // FMODUnity.RuntimeManager.PlayOneShot("event:/HeavyEnemy/HeavyEnemyAttack");
        soundEvent.start();
       
    }

   public void HeavySlam()
    {
        Debug.Log("SonidoSlam");
        FMODUnity.RuntimeManager.PlayOneShot("event:/HeavyEnemy/HeavyEnemySpecialAttack");
    }

    public void ReactionHeavy()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HeavyEnemy/HeavyEnemyNoticeTleli");
    }

    public void heavyDead()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/HeavyEnemy/HeavyEnemyDeath");
    }

}
