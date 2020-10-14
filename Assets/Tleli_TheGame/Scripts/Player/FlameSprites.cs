using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Karime y Lucy
// Mostrar flama con sprites

public class FlameSprites : MonoBehaviour
{
    
    public Sprite[] flameSprites;     
    Image img;                        

    int spriteIndex;
    int nSprites;

    float maxFlame = 100;

    TlelliFlameHealth tlelli;
    float flameCurrentValue;


    void Start()   
    {
        nSprites = flameSprites.Length;
        img = gameObject.GetComponent<Image>();

        tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
        flameCurrentValue = tlelli.GetFlame();

        
    }


    void Update()
    {
        // Dividir sprites dependiendo el rango en el que se encuentre la flama en relación con el valor máximo

        if (flameCurrentValue != tlelli.GetFlame())   
        {
            flameCurrentValue = tlelli.GetFlame();
        }

        if (flameCurrentValue >= 0 && flameCurrentValue <= (maxFlame / nSprites))
        {
            spriteIndex = 0;
        }

        if (flameCurrentValue >= (maxFlame / nSprites) && flameCurrentValue <= (maxFlame / nSprites * 2))
        {
            spriteIndex = 1;
        }

        if (flameCurrentValue >= (maxFlame / nSprites * 2) && flameCurrentValue <= maxFlame)
        {
            spriteIndex = 2;
        }

        ChangeSprites(spriteIndex);

    }

    void ChangeSprites(int spr)
    {
       img.sprite = flameSprites[spr];
    }
}
