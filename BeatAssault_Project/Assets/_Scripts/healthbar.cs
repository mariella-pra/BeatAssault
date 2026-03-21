using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider healthbarSlider;
    public float maxHealth = 100f;
    public float health;
    public Slider easeHealthbar;
    private float lerpSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (healthbarSlider.maxValue != maxHealth)
        {
            healthbarSlider.maxValue = maxHealth; //linkeeen das man es nur einmal eingeben muss
        }

        if (healthbarSlider.value != health)
        {
            healthbarSlider.value = health; //same thing linkeeen
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }

        if (healthbarSlider.value != easeHealthbar.value) //easehealthbar
        {
            easeHealthbar.value = Mathf.Lerp(easeHealthbar.value, health, lerpSpeed);
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;   
    }
}
