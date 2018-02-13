using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHealth : MonoBehaviour {

    [SerializeField]
    float defaultSize = 10;

    [SerializeField]
    GameObject playerObj;

    HealthSystem playerHealth;
    // Use this for initialization
    void Start()
    {
        playerHealth = playerObj.GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(playerHealth.getHealth() / playerHealth.getMaxHealth() * defaultSize, transform.localScale.y, transform.localScale.z);
    }
}
