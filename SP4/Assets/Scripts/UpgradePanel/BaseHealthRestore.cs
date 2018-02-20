using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthRestore : UpgradesButton {
    [SerializeField]
    GameObject ThePlayer;

    public override int getCostOfUpgrade()
    {
        return 50;
    }
    public override void DoUpgradeStuff()
    {
        if (ThePlayer.GetComponent<InGameCash>().addAmount(-getCostOfUpgrade()))
        {
            Debug.Log("Increasing Health");
            ThePlayer.GetComponent<HealthSystem>().addHealth(1);
        }
    }
}
