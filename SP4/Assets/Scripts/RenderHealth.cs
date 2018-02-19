using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHealth : MonoBehaviour {
    
    float defaultSize = 10;

    [SerializeField]
    GameObject playerObj;
    [SerializeField]
    GameObject theParent;
    [SerializeField]
    GameObject healthTexture;

    HealthSystem playerHealth;
    //Vector3 prevScale;
    // Use this for initialization
    void Start()
    {
        playerHealth = playerObj.GetComponent<HealthSystem>();
        for (int i = 0; i < playerHealth.getMaxHealth(); ++i)
        {
            GameObject healthobj = Instantiate(healthTexture, new Vector3(0,0,0), Quaternion.identity);
            healthobj.transform.localScale = new Vector3(healthobj.transform.localScale.x / playerHealth.getMaxHealth(), healthobj.transform.localScale.y, healthobj.transform.localScale.z);
            Vector3 position = new Vector3(theParent.transform.position.x + i * healthobj.transform.localScale.x, theParent.transform.position.y, theParent.transform.position.z);
            healthobj.transform.SetParent(theParent.transform);
            healthobj.transform.position = position;
            healthobj.name = playerObj.transform.name + "health " + i;
            Debug.Log(healthobj.name);
        }
        //prevScale = transform.localScale;
        //defaultSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isHealthModified())
        {
            //theParent.transform.GetChild((int)playerHealth.getHealth() + 1);
            for (int i = theParent.transform.childCount; i >= (int)playerHealth.getHealth() ; --i)
            {
                Debug.Log(playerObj.transform.name + "health " + i);
                GameObject toBeDestroyed = GameObject.Find(playerObj.transform.name + "health " + i);
                Destroy(toBeDestroyed);
            }

            //float currPercentage = playerHealth.getHealth() / playerHealth.getMaxHealth();
            //Debug.Log("PrevScale:" + transform.localScale + ", PrevPos:" + transform.localPosition + ", prevScale:" + prevScale);
            //transform.localScale = new Vector3(currPercentage * defaultSize, transform.localScale.y, transform.localScale.z);
            ////transform.localPosition = new Vector3(transform.localPosition.x - (1 - (playerHealth.getHealth() / playerHealth.getMaxHealth())) * defaultSize, transform.localPosition.y, transform.localPosition.z);
            ////transform.localPosition = new Vector3(transform.localPosition.x - ((currPercentage) * prevScale.x * defaultSize), transform.localPosition.y, transform.localPosition.z);
            ////transform.Translate(new Vector3(0.5f*((playerHealth.getHealth() / playerHealth.getMaxHealth()-1) ), 0, 0));
            ////transform.Translate(new Vector3(((currPercentage - 1f)) * prevScale.x, 0, 0));
            //Debug.Log("NewScale:" + transform.localScale + ", NewPos:" + transform.localPosition);
            //playerHealth.setHealthModifiedToFalse();
        }
    }
}
