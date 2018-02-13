using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour {

    int gold = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void addGold(int _amount)
    {
        gold += _amount;
    }
    int getGold()
    {
        return gold;
    }
}
