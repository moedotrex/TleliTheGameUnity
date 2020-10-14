using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSprites : MonoBehaviour
{

    public Sprite[] HPSprites;
    Image img;

    int spriteIndex;
    int nSprites;

    float maxHP = 100;

    TlelliFlameHealth tlelli;
    float flameCurrentValue;


    void Start()
    {
        nSprites = HPSprites.Length;
        img = gameObject.GetComponent<Image>();

        tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        flameCurrentValue = tlelli.GetHP();


    }


    void Update()
    {
        // Dividir sprites dependiendo el rango en el que se encuentre la flama en relación con el valor máximo

        if (flameCurrentValue != tlelli.GetHP())
        {
            flameCurrentValue = tlelli.GetHP();
        }

        if (flameCurrentValue >= 0 && flameCurrentValue <= (maxHP / nSprites))
        {
            spriteIndex = 0;
        }

        if (flameCurrentValue >= (maxHP / nSprites) && flameCurrentValue <= (maxHP / nSprites * 2))
        {
            spriteIndex = 1;
        }

        if (flameCurrentValue >= (maxHP / nSprites * 2) && flameCurrentValue <= (maxHP / nSprites * 3))
        {
            spriteIndex = 2;
        }

        if (flameCurrentValue >= (maxHP / nSprites * 3) && flameCurrentValue <= (maxHP / nSprites * 4))
        {
            spriteIndex = 3;
        }

        if (flameCurrentValue >= (maxHP / nSprites * 4) && flameCurrentValue <= maxHP)
        {
            spriteIndex = 4;
        }

        ChangeSprites(spriteIndex);

    }

    void ChangeSprites(int spr)
    {
        img.sprite = HPSprites[spr];
    }
}
