using System.Collections;
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
    float flame;
    float flameIntensity;
      
    public float attack;      
    public float flameDamage;

    public TleliAnimationController tleliAnimationController;


    void Start()
    {
        flame = maxFlame;
        HP = maxHP;
        flameIntensity = Remap(flame, 0, maxFlame, flameMinIntensity, flameMaxIntensity);         //Hacer un "remap" de los valores de la vida de Tlelli (0-100) a los valores de flama (0-5)
   
    }


    void Update()
    {

        // Ejemplo para probar relación entre ataque y flama
        if (Input.GetKeyDown("x"))
        {
            SetHPDamage(attack);
        }  
    }

 

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            SetFlameDamage(flameDamage * Time.deltaTime);
            FlameUpdateMaterial();
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
    }

    
    public void SetHPDamage(float attackStrength)
    {
        // Fórmula para cálculo de daño, flama como armadura

        float damage = attackStrength * (100 / (100 + flame));
        damage = Mathf.Round(damage * 100f) / 100f;

        //-------

        HP -= damage;   
        if (HP < 0)
        {
            HP = 0;
        }

        Text damageTxt = GameObject.Find("ShowDamage").GetComponent<Text>();
        damageTxt.text = "- " + damage;

       
    }

    public void EnemyDistance(float d)
    {
        float dist = d;
        float dam;

        dam = (1 / dist) * flameDamage;
        SetFlameDamage(dam);

        //print("Distancia: " + dist + "  Daño: " + dam);
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
            tleliAnimationController.IsDeadTrigger();
        }
    }
}
