using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trap : MonoBehaviour {
    [SerializeField]
    string TrapName;                //Name of trap being created
    
    /*********************Trap Behaviours***************************************/
    [SerializeField]
    bool instantKill = false;       //Instant kills any soldier colliding onto it
    [SerializeField]
    bool dealsDamage = false;       //Deals damge on collision
    [SerializeField]
    float damageAmount = 0.0f;      //Amount of damage incurred
    /***************************************************************************/

    List<GameCode.TrapTypes> typesOfTraps;

    // Use this for initialization
    void Start()
    {
        typesOfTraps = GameObject.Find("EventSystem").GetComponent<GameCode>().typesOfTraps;    //Gets list from GameCode script
        foreach (GameCode.TrapTypes existingTraps in typesOfTraps)
        {
            if(existingTraps.name == TrapName)                                                  //Validating Trap name against list of possible traps
            {
                Image myImage = this.gameObject.AddComponent<Image>();                          //Creates Image script to object
                myImage.sprite = existingTraps.texture;                                         //Assigns texture
                return;                                                                         //ends function when done
            }
        }
        Debug.Log("Trap ["+TrapName+"] does not exist and it will be destroyed");               //Debug infomation of trap that is going to be deleted
        Destroy(this.gameObject);                                                               //Deleting trap with name that does not exist in typesOfTraps
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject.GetComponent<Image>().sprite.name);
    }

    //Does stuff based on the bool that are set to true, takes in the affected gameobject as parameter
    public void activateTrap(GameObject go)
    {
        if (instantKill)
        {
            //kills/destroy go
            Destroy(go);
            return;
        }
        if(dealsDamage)
        {
            //Deals set damage to gameobject
            go.GetComponent<HealthSystem>().addHealth(-damageAmount);
        }
    }
}
