using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour {
    
    public void IncrementHealth(GameObject _go)
    {
        HealthSystem objHealth = _go.GetComponent<HealthSystem>();
        objHealth.addHealth(-10);
    }
}
