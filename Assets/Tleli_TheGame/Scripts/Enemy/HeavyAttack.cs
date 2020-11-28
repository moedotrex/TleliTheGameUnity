using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeavyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 3f;
    public float attackDamage = 1;
    public float attackDamageSlam = 1;
    public float slamCounter = 20f;
    public float speed;
    public float jumpSpeed;
    public float jumpRadio;
    public bool isAnimating = false;
    

    private Vector3 slamLand;
    private Transform playerPos;

    Transform target;

    GameObject player;
    //TlelliFlameHealth TlelliHealth;
    TleliHealth TlelliHealth;


    tleliKnockBack playerKnockback;



    HeavyBoiAnimationController heavyBoiAnimationController; //moe 

    bool playerInRange;
    float timer;
    public bool isDisplaced;
    //crear evento para detectar tiempo de anim gethit y death

    void Start()
    {

        target = PlayerManager.instance.player.transform;
        heavyBoiAnimationController = GetComponentInChildren<HeavyBoiAnimationController>(); //moe

        player = GameObject.FindGameObjectWithTag("Player");
        // TlelliHealth = player.GetComponent<TlelliFlameHealth>();
        TlelliHealth = player.GetComponent<TleliHealth>();

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
       

        playerKnockback = player.GetComponent<tleliKnockBack>();

    }


    void Update()
    {
        if (isAnimating == false)
        {
            timer += Time.deltaTime;
        }

        if (timer >= timeBetweenAttacks && playerInRange && isDisplaced == false)

        {
            int randomNum = Random.Range(1, 100);

            if (randomNum <= 66)
            {
                Debug.Log("nojala");

                isAnimating = true;
                timer = 0f;
                heavyBoiAnimationController.LightAttackTrigger(); //moe
                

                

            }

            if (randomNum > 66)
            {
                //heavyBoiAnimationController.LightAttackTrigger(); //moe
                //timer = 0f;

            }
        }

        float distance = Vector3.Distance(target.position, transform.position);
        {
            if (distance <= jumpRadio && timer >= slamCounter && isDisplaced == true)
            {
                isAnimating = true;
                timer = 0f;
                heavyBoiAnimationController.JumpTrigger(); //moe 
                JumpSlamp();
            }
        }
    }

         public void Attack()
        {
            timer = 0f;

            if (TlelliHealth.flame > 0 && playerInRange)
            {
                
                TlelliHealth.HurtFlame(attackDamage);
                playerKnockback.startKnockBack(10f);
            }

            if (TlelliHealth.flame <= 0 && playerInRange)
            {
                TlelliHealth.SetHPDamage(1);
            }
            /* if (TlelliHealth.HP < 0)
             {
            Poner una barrera para que deje de atacar
             }*/
        }

        public void AttackSlam()
        {
            timer = 0f;

            if (TlelliHealth.flame > 0 && playerInRange)
            {
                

                TlelliHealth.HurtFlame(attackDamage);
                playerKnockback.startKnockBack(15f);
            }

            if (TlelliHealth.flame <= 0 && playerInRange)
            {
                TlelliHealth.SetHPDamage(1);
            }
        }

        public void JumpSlamp()
        {

        int randomNum = Random.Range(1, 100);

        if (TlelliHealth.HP > 0 && randomNum >= 80)
        {

            slamLand = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
            transform.position = Vector3.MoveTowards(transform.position, slamLand, jumpSpeed);

            if (playerInRange)
            {

                TlelliHealth.HurtFlame(attackDamage);
                playerKnockback.startKnockBack(5f);
            }


            if (TlelliHealth.flame <= 0)
            {
                TlelliHealth.SetHPDamage(1);
            }


        }
        

           
        }

        //¿jugador en rango de ataque?
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }

        //jugador fuera de rango
        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }

        }

        void AnimationOff()
    {
        isAnimating = false;
    }
    
}
