using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsSystem : MonoBehaviour {

    private GridSystem theGridSystem = null;

    [SerializeField]
    uint MaximumPowerUpsAmount = 10;

    public enum POWERUP_TYPE
    {
       
    }

    // Use this for initialization
    void Start () {
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatePowerups()
    {
        for(uint i = 0; i <= MaximumPowerUpsAmount; ++i)
        {
            //theGridSystem.grid[Random.Range(0, theGridSystem.GridSize)].transform.position;
        }
    }

    public void PlacePowerUp(Vector2 positon, POWERUP_TYPE powerType)
    {

    }
}

class PowerUp
{
    //PowerupsSystem.POWERUP_TYPE

}
