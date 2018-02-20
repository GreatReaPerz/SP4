using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradesButton : MonoBehaviour {
    public abstract void DoUpgradeStuff();
    public abstract int getCostOfUpgrade();
}
