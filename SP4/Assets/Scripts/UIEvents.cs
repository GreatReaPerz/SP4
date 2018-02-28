using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour {
    string boolToCheck;

    private void Awake()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(540, 960, false);
        }
    }

    //Used for animation
    //Sets the boolean(assigned through SetBoolToCheck(string) function) to true
    public void BoolToCheckEnable(Animator _anim)
    {
        _anim.SetBool(boolToCheck, true);
    }

    //Used for animation
    //Sets the boolean(assigned through SetBoolToCheck(string) function) to false
    public void BoolToCheckDisable(Animator _anim)
    {
        _anim.SetBool(boolToCheck, false);
    }

    //Used for animation
    //Changes the name of the boolean to be modified
    public void SetBoolToCheck(string boolName)
    {
        boolToCheck = boolName;
    }

    //Used for buttons to Debug
    public void DebugLog(string text)
    {
        Debug.Log(text);
    }


    public void BuyUpgrade(GameObject theButton)
    {
        //int cost = this.gameObject.GetComponent<UpgradesButton>().costOfUpgrade;
        //thePlayer.GetComponent<InGameCash>().addAmount(-cost);

        //if (thePlayer.GetComponent<InGameCash>().addAmount(this.GetComponentInParent<UpgradesButton>().getCostOfUpgrade()))
        theButton.GetComponent<UpgradesButton>().DoUpgradeStuff();
    }
    public void addDialog(DialogBox _db)
    {
        _db.addDialog("Hello");
    }
}
