using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupsSystem : MonoBehaviour {

    private GridSystem theGridSystem = null;

    [SerializeField]
    uint MaximumPowerUpsAmount = 10;

    List<Image> PlayerGridPowerups = new List<Image>();
    List<Image> EnemyGridPowerups = new List<Image>();

    public enum POWERUP_TYPE
    {
       TOTAL_POWERUPS
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

    public void PlacePowerUp(Vector2 positon, POWERUP_TYPE powerType = POWERUP_TYPE.TOTAL_POWERUPS)
    {

    }

    public void PlacePowerUp(GameObject theGrid, uint GridIndex, POWERUP_TYPE powerType = POWERUP_TYPE.TOTAL_POWERUPS)
    {

    }
}

class PowerUp
{
    PowerupsSystem.POWERUP_TYPE powerType;
    Vector2 powerupPosition = new Vector2();


}
