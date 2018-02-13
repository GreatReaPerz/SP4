using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour {
    //void Start()
    //{
    //    Debug.Log("Hello");
    //}
    public void PauseMenuEnable(Animator _anim)
    {
        Debug.Log("Pausing");
        _anim.SetBool("PauseEnabled", true);
    }
    public void PauseMenuDisable(Animator _anim)
    {
        Debug.Log("unPausing");
        _anim.SetBool("PauseEnabled", false);
    }
    public void DebugLog(string text)
    {
        Debug.Log(text);
    }
}
