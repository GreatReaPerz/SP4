using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSystem : MonoBehaviour {

    int gold = 7000;
	// Use this for initialization
	void Start () {
        gold = PlayerPrefs.GetInt("Gold", gold);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addGold(int _amount)
    {
        gold += _amount;
        PlayerPrefs.SetInt("Gold", gold);
        PlayerPrefs.Save();
    }
    public int getGold()
    {
        return gold;
    }
}
