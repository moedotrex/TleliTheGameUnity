using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIFlameHP : MonoBehaviour
{
    public Material UIFlame;
    public Material UIHealth;

    TleliHealth tleli;

    float flame;
    float health;


    void Start()
    {

        tleli = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();

        flame = tleli.GetFlame();
        health = tleli.GetHP();

        UIFlame.SetFloat("_LevelUI", flame);
        UIHealth.SetFloat("_LevelUI", health);
    }


    void FixedUpdate()
    {

        flame = tleli.GetFlame();
        health = tleli.GetHP();

        health = Remap(health, 0, tleli.maxHP, 20, 80);

        UIFlame.SetFloat("_LevelUI", flame);
        UIHealth.SetFloat("_LevelUI", health);

    }

    private float Remap(float original, float originalMin, float originalMax, float newMin, float newMax)
    {
        return newMin + (original - originalMin) * (newMax - newMin) / (originalMax - originalMin);
    }
}
