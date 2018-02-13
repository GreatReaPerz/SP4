using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    int health/* = 100*/;
    [SerializeField]
    int maxHealth = 100;
	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addHealth(int _amount)
    {
        health += _amount;
    }
    public int getHealth()
    {
        return health;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
}
