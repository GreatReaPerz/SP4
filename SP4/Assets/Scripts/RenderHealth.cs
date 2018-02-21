using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderHealth : MonoBehaviour {
    
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
            createHealthobj(i);
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

            if (playerHealth.isHealthDecreased())
            {
                //deleting from the last health object to current
                for (int i = theParent.transform.childCount; i >= (int)playerHealth.getHealth(); --i)
                {
                    GameObject toBeDestroyed = GameObject.Find(playerObj.transform.name + "health " + i);
                    Destroy(toBeDestroyed);
                }
            }
            else
            {
                for (int i = theParent.transform.childCount; i < (int)playerHealth.getHealth(); ++i)
                {
                    createHealthobj(i);
                }
            }

            //float currPercentage = playerHealth.getHealth() / playerHealth.getMaxHealth();
            //Debug.Log("PrevScale:" + transform.localScale + ", PrevPos:" + transform.localPosition + ", prevScale:" + prevScale);
            //transform.localScale = new Vector3(currPercentage * defaultSize, transform.localScale.y, transform.localScale.z);
            ////transform.localPosition = new Vector3(transform.localPosition.x - (1 - (playerHealth.getHealth() / playerHealth.getMaxHealth())) * defaultSize, transform.localPosition.y, transform.localPosition.z);
            ////transform.localPosition = new Vector3(transform.localPosition.x - ((currPercentage) * prevScale.x * defaultSize), transform.localPosition.y, transform.localPosition.z);
            ////transform.Translate(new Vector3(0.5f*((playerHealth.getHealth() / playerHealth.getMaxHealth()-1) ), 0, 0));
            ////transform.Translate(new Vector3(((currPercentage - 1f)) * prevScale.x, 0, 0));
            //Debug.Log("NewScale:" + transform.localScale + ", NewPos:" + transform.localPosition);
            playerHealth.setHealthModifiedToFalse();
        }
    }

    void createHealthobj(int i)
    {
        GameObject healthobj = Instantiate(healthTexture, new Vector3(0, 0, 0), Quaternion.identity);     //Instantiating new object
        healthobj.transform.localScale = new Vector3(healthobj.transform.localScale.x / playerHealth.getMaxHealth(), healthobj.transform.localScale.y, healthobj.transform.localScale.z);   //calculate new scale to always fit default size
        Vector3 position = new Vector3(theParent.transform.position.x + i * healthobj.transform.localScale.x, theParent.transform.position.y, theParent.transform.position.z);              //calculate new position
        healthobj.transform.SetParent(theParent.transform);                                             //Set parent
        healthobj.transform.position = position;                                                        //Displacment
        healthobj.name = playerObj.transform.name + "health " + i;                                      //give each object unique names
    }
}
