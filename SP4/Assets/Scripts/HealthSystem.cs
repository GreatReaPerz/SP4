using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    public float health/* = 100*/;
    private float previousHealth;
    [SerializeField]
    float maxHealth = 100f;

    private bool updateHealthRender = false;
	// Use this for initialization
	void Start () {
        InitHealth(maxHealth);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addHealth(float _amount)
    {
        if (_amount == 0)
            return;
        setUpdateHealthRender(true);
        previousHealth = health;
        health += _amount;
        if (health > maxHealth)
            health = maxHealth;
        else if (health < 0)
            health = 0;
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
    public void InitHealth(float _maxHealth)
    {
        health = _maxHealth;
        maxHealth = _maxHealth;
        previousHealth = health;
    }
    public bool getUpdateHealthRender()
    {
        return updateHealthRender;
    }
    public void setUpdateHealthRender(bool _newState)
    {
        updateHealthRender = _newState;
    }
}
