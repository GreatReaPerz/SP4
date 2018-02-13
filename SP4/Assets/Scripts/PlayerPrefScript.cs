using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefScript : MonoBehaviour {

    private float health = 100.0f;

    private int gold = 0;

    private float volume = 10.0f;

	// Use this for initialization
	void Start () {
        health = PlayerPrefs.GetFloat("Health", health);
        volume = PlayerPrefs.GetFloat("Volume", volume);
        gold = PlayerPrefs.GetInt("Gold", gold);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
