﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    float health/* = 100*/;
    float previousHealth;
    [SerializeField]
    float maxHealth = 100f;
	// Use this for initialization
	void Start () {
        health = maxHealth;
        previousHealth = health;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addHealth(int _amount)
    {
        Debug.Log("Health:" + health + ",Amount:" + _amount);
        if (health + _amount <=0)
        {
            health = 0;
            return;
        }
        previousHealth = health;
        health += _amount;
        if (health > maxHealth)
            health = maxHealth;
    }
    public float getHealth()
    {
        return health;
    }
    public float getPreviousHealth()
    {
        return previousHealth;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }
    public bool isHealthModified()
    {
        return previousHealth != health;
    }
    public bool isHealthDecreased()
    {
        return previousHealth > health;
    }
    public void setHealthModifiedToFalse()
    {
        previousHealth = health;
    }
}
