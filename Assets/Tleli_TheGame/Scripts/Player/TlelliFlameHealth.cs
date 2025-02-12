﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// FlamaTlelli
// Karime y Lucy
// Control de vida y flama de Tlelli
public class TlelliFlameHealth : MonoBehaviour

{
    public float maxHP = 100;        
    public float maxFlame = 100;     
    float flameMaxIntensity = 65;           //Valor máximo de la flama en shader (brightness)
    float flameMinIntensity = 35;

    public float HP;
    public float flame;
    float flameIntensity;
      
    public float attack;      
    public float flameDamage;

    public bool isBattling; //Added by Emil. Needed to zoom out camera when in battle.

    PlayerController invincibilityFrames;  //VACA
    float invincibilityLenght = 1f; //vaca duracion de invulnerabilidad despues de recibir daño
    float invincibilityCounter;

    TlelliSonido SendHurt; //ADRIAN llamar script para madarle el dolor

    public TleliAnimationController tleliAnimationController;


    void Start()
    {
        flame = maxFlame;
        HP = maxHP;
        flameIntensity = Remap(flame, 0, maxFlame, flameMinIntensity, flameMaxIntensity);         //Hacer un "remap" de los valores de la vida de Tlelli (0-100) a los valores de flama (0-5)
        getPCscritp(); //VACA obtener script de player controller
        isBattling = false;
        SendHurt = GetComponent<TlelliSonido>();
    }


    void Update()
    {
        // Ejemplo para probar relación entre ataque y flama
        /*if (Input.GetKeyDown("x"))
        {
         SetHPDamage(attack);
        }*/

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;
        }

    }

    //Colisiona tleli con enemigo
    //Si no estas frente a frente con enemigo no te quita vida
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SetFlameDamage(flameDamage * Time.deltaTime);
            FlameUpdateMaterial();
            
            //tleliAnimationController.IsHitTrigger();
            //agregra tiempo de recuperación
        }
    }

    

    /* private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("Enemy"))
         {
             SetHPDamage(attack);
         }
     }*/

    public void RecoverFlame(float recover)
    {
        flame += recover;

        if (flame > maxFlame)
        {
            flame = maxFlame;
        }
       
    }

    public void SetFlameDamage(float dam)
    {
        if (flame > 0)
        {
            flame -= dam * Time.deltaTime;
        }
        else
        {
            flame = 0;
        }
        
        FlameUpdateMaterial();
        isBattling = true;
    }

    
    public void SetHPDamage(float attackStrength)
    {
        if (invincibilityFrames.isDisplaced == false) {  // VACA si tleli esta dasheando no puede recibir daño (invincibilityFrames)

            if (invincibilityCounter <= 0) //VACA checa si tleli esta en tiempo de invulnerabilidad
            {
                invincibilityCounter = invincibilityLenght; //VACA empieza el timer de invulnerabilidad

                // Fórmula para cálculo de daño, flama como armadura
                
                float damage = attackStrength * (100 / (100 + flame));
                damage = Mathf.Round(damage * 100f) / 100f;
                tleliAnimationController.IsHitTrigger(); // VACA cambiar animacion cuando reciba daño y no cuando entre en trigger


                SendHurt.FlameIsDamaged = true;
                

                HP -= damage;

                
                //-------

                if (HP < 0)
            {
                HP = 0;
                    //tleliAnimationController.IsDeadTrigger(); This Trigger is now a Bool and it's controlled from the TleliDeath script. Changed by Emil.
                }

                Text damageTxt = GameObject.Find("ShowDamage").GetComponent<Text>();
        damageTxt.text = "- " + damage;
        }
        }

    }

    public void EnemyDistance(float d)
    {
        float dist = d;
        float dam;
        dam = (1 / dist) * flameDamage;
        SetFlameDamage(dam);
        //print("Distancia: " + dist + "  Daño: " + dam);
    }

    public void BattleMode(bool bm) //Added by Emil. Necessary for changing camera into Battle Mode.
    {
        isBattling = bm;
    }

    

    
    //Remapear rango de valores de flama a intensidad para el shader
    //------------------------------------------------------------------------
    public void FlameUpdateMaterial()
    {
        flameIntensity = Remap(flame, 0, maxFlame, flameMinIntensity, flameMaxIntensity);
    }

    private float Remap(float original, float originalMin, float originalMax, float newMin, float newMax)
    {
        return newMin + (original - originalMin) * (newMax - newMin) / (originalMax - originalMin);
    }


    // Regresar valores 
    //-------------------------------------
    public float GetFlameIntensity()
    {
        return flameIntensity;
    }

    public float GetFlame()
    {
        return flame;
    }

    public float GetHP()
    {
        return HP;

    }

     public void DeathTleli()
      {
          if (HP <= 0)
          {
              //tleliAnimationController.IsDeadTrigger(); Wasn't used. This Trigger is now a Bool and it's controlled from the TleliDeath script. Changed by Emil.
          }
      }

    //VACA
    public void getPCscritp()
    {
        invincibilityFrames = GetComponent<PlayerController>();
    }
}
