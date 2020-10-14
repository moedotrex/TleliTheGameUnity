using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    private float currentHealth;

   public  Color ogColor;

    void Start()
    {
        currentHealth = health;
    }


    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtEnemy(float damage)
    {
        //  GameObject.Instantiate(blood, transform.position, Quaternion.identity);

        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage.");
        StartCoroutine(HurtEnemyCoroutine());
    }

    IEnumerator HurtEnemyCoroutine()
    {
        Component[] rend = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Component layer in rend)
        {
            Renderer r = layer.GetComponent<Renderer>();
            r.material.color = Color.red;

        }

        yield return new WaitForSeconds(0.1f);

        foreach (Component layer in rend)
        {
            Renderer r = layer.GetComponent<Renderer>();
            r.material.color = ogColor;

        }
    }    
}
