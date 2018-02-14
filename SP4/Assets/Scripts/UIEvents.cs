using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour {
    string boolToCheck;
    public void BoolToCheckEnable(Animator _anim)
    {
        _anim.SetBool(boolToCheck, true);
    }
    public void BoolToCheckDisable(Animator _anim)
    {
        _anim.SetBool(boolToCheck, false);
    }
    public void SetBoolToCheck(string boolName)
    {
        boolToCheck = boolName;
    }
    public void DebugLog(string text)
    {
        Debug.Log(text);
    }
}
