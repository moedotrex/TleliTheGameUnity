using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Karime
// Visualizar vida en UI

public class HPFlameValuesUI: MonoBehaviour
{

    public Image FlameBar;
    public Image HPBar;
    // TlelliFlameHealth tlelli;
    TleliHealth tlelli;
    float FlameCurrentValue;
    float HPCurrentValue;
    Text HPtxt;
    Text flameTxt;




    void Start()   //Recuperar valor de vida de Tlelli
    {
       // tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TlelliFlameHealth>();
       tlelli = GameObject.FindGameObjectWithTag("Player").GetComponent<TleliHealth>();

        HPCurrentValue = tlelli.GetHP();
        FlameCurrentValue = tlelli.GetFlame();
        flameTxt = GameObject.Find("ShowFlame").GetComponent<Text>();
        HPtxt = GameObject.Find("ShowHP").GetComponent<Text>();
    }


    void Update()
    {
        if (FlameCurrentValue != tlelli.GetFlame()/100)   //Sólo actualizar barra si el valor de flama cambió
        {      
            FlameCurrentValue = tlelli.GetFlame()/100;
            FlameBar.fillAmount = FlameCurrentValue;
        }

        if (HPCurrentValue != tlelli.GetHP() / 2)    //Sólo actualizar barra si el valor de vida cambió
        {     
            HPCurrentValue = tlelli.GetHP() / 2;
            HPBar.fillAmount = HPCurrentValue;
        }

        flameTxt.text = "" + (int)tlelli.GetFlame();
        HPtxt.text = "" + (int)tlelli.GetHP();

    }
}
