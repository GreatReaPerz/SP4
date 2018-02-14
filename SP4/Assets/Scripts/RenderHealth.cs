using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHealth : MonoBehaviour {
    
    float defaultSize = 10;

    [SerializeField]
    GameObject playerObj;

    HealthSystem playerHealth;
    Vector3 prevScale;
    // Use this for initialization
    void Start()
    {
        playerHealth = playerObj.GetComponent<HealthSystem>();
        prevScale = transform.localScale;
        defaultSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isHealthModified())
        {
            float currPercentage = playerHealth.getHealth() / playerHealth.getMaxHealth();
            Debug.Log("PrevScale:" + transform.localScale + ", PrevPos:" + transform.localPosition + ", prevScale:" + prevScale);
            transform.localScale = new Vector3(currPercentage * defaultSize, transform.localScale.y, transform.localScale.z);
            //transform.localPosition = new Vector3(transform.localPosition.x - (1 - (playerHealth.getHealth() / playerHealth.getMaxHealth())) * defaultSize, transform.localPosition.y, transform.localPosition.z);
            //transform.localPosition = new Vector3(transform.localPosition.x - ((currPercentage) * prevScale.x * defaultSize), transform.localPosition.y, transform.localPosition.z);
            //transform.Translate(new Vector3(0.5f*((playerHealth.getHealth() / playerHealth.getMaxHealth()-1) ), 0, 0));
            //transform.Translate(new Vector3(((currPercentage - 1f)) * prevScale.x, 0, 0));
            Debug.Log( "NewScale:" + transform.localScale + ", NewPos:" + transform.localPosition);
            playerHealth.setHealthModifiedToFalse();
        }
    }
}
