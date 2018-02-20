using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCash : MonoBehaviour {
    public int amount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int getAmount()
    {
        return amount;
    }

    public bool addAmount(int _amount)
    {
        Debug.Log("Amount:" + amount + ",_Amount:" + _amount);
        if (_amount < 0 && amount == 0)
            return false;
        if(amount + _amount < 0)
        {
            amount = 0;
            return false;
        }
        amount += _amount;
        return true;
    }

    //To be used at the end of each game
    //cashes out remaining unused cash earned in game into current gold balance
    public void cashoutToGold()
    {
        this.gameObject.GetComponent<GoldSystem>().addGold(amount);
    }

}
