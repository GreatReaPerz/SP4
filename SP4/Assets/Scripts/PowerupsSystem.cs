using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupsSystem : MonoBehaviour
{
    private GridSystem theGridSystem = null;
    private enemyGridSystem theEnemyGridSystem = null;

    [SerializeField]
    uint MaximumPowerUpsAmount = 10;

    [SerializeField]
    public Image SampleImage;

    [SerializeField]
    public bool PowerupsIsActive = false;

    //Sprites
    [SerializeField]
    public Sprite moveSpeedSprite;
    [SerializeField]
    public Sprite attackSpeedSprite;
    [SerializeField]
    public Sprite attackDamageSprite;

    public List<PowerUp> PlayerGridPowerups = new List<PowerUp>();
    public List<PowerUp> EnemyGridPowerups = new List<PowerUp>();

    public enum POWERUP_TYPE
    {
        POWERUP_MOVESPEED = 0,
        POWERUP_ATTACKSPEED,
        POWERUP_ATTACKDAMAGE,

        TOTAL_POWERUPS
    }
    
    // Use this for initialization
    void Start()
    {
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        theEnemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();

        Debug.Assert(theGridSystem != null);
        Debug.Assert(theEnemyGridSystem != null);
        SampleImage.enabled = false;
        
        CreatePowerups();
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public void CreatePowerups()
    {
        //Player Grid
        for (uint i = 0; i <= MaximumPowerUpsAmount; ++i)
        {
            //Add to the player grid powerups list
            PlayerGridPowerups.Add(new PowerUp(theGridSystem.grid[(int)Random.Range(0, theGridSystem.GridSize - 1)].transform.position));

            //Store the sprites into the object
            PlayerGridPowerups[(int)i].StoreSprites(moveSpeedSprite, attackSpeedSprite, attackDamageSprite);
            //Init the image object in powerup class
            PlayerGridPowerups[(int)i].PowerUpTexture = Instantiate(SampleImage);
            //Decides the powerup modifiers based on powertype
            PlayerGridPowerups[(int)i].AssignType();

            //Anchor to bottom
            PlayerGridPowerups[(int)i].PowerUpTexture.rectTransform.anchorMin = new Vector2(0.5f, 0);
            PlayerGridPowerups[(int)i].PowerUpTexture.rectTransform.anchorMax = new Vector2(0.5f, 0);
            PlayerGridPowerups[(int)i].PowerUpTexture.rectTransform.pivot = new Vector2(0.5f, 0.5f);

            //Set the parent to canvas
            PlayerGridPowerups[(int)i].PowerUpTexture.transform.SetParent(GameObject.Find("PowerUpSystem").transform, true);
            //Change the position of powerup
            PlayerGridPowerups[(int)i].ChangePosition();
        }

        //Enemy Grid
        for (uint i = 0; i <= MaximumPowerUpsAmount; ++i)
        {
            EnemyGridPowerups.Add(new PowerUp(theEnemyGridSystem.grid[(int)Random.Range(0, theEnemyGridSystem.GridSize - 1)].transform.position));

            //Store the sprites into the object
            EnemyGridPowerups[(int)i].StoreSprites(moveSpeedSprite, attackSpeedSprite, attackDamageSprite);
            //Init the image object in powerup class
            EnemyGridPowerups[(int)i].PowerUpTexture = Instantiate(SampleImage);
            //Decides the powerup modifiers based on powertype
            EnemyGridPowerups[(int)i].AssignType();

            //Anchor to top
            EnemyGridPowerups[(int)i].PowerUpTexture.rectTransform.anchorMin = new Vector2(0.5f, 1);
            EnemyGridPowerups[(int)i].PowerUpTexture.rectTransform.anchorMax = new Vector2(0.5f, 1);
            EnemyGridPowerups[(int)i].PowerUpTexture.rectTransform.pivot = new Vector2(0.5f, 0.5f);

            //Set the parent to canvas
            EnemyGridPowerups[(int)i].PowerUpTexture.transform.SetParent(GameObject.Find("PowerUpSystem").transform, true);
            //Change the position of powerup
            EnemyGridPowerups[(int)i].ChangePosition();
        }
    }

    public void PlacePowerUp(uint GridIndex, string WhichGrid = "Both", POWERUP_TYPE powerType = POWERUP_TYPE.TOTAL_POWERUPS)
    {
        if (WhichGrid == "Player" || WhichGrid == "Both")
        {
            PlayerGridPowerups.Add(new PowerUp(theGridSystem.grid[GridIndex].transform.position, powerType));

            //Store the sprites into the object
            PlayerGridPowerups[PlayerGridPowerups.Count - 1].StoreSprites(moveSpeedSprite, attackSpeedSprite, attackDamageSprite);
            //Init the image object in powerup class
            PlayerGridPowerups[PlayerGridPowerups.Count - 1].PowerUpTexture = Instantiate(SampleImage);
            //Decides the powerup modifiers based on powertype
            PlayerGridPowerups[PlayerGridPowerups.Count - 1].AssignType();
            //Change the position of powerup
            PlayerGridPowerups[PlayerGridPowerups.Count - 1].ChangePosition();
        }

        if (WhichGrid == "Enemy" || WhichGrid == "Both")
        {
            EnemyGridPowerups.Add(new PowerUp(theEnemyGridSystem.grid[GridIndex].transform.position, powerType));

            //Store the sprites into the object
            EnemyGridPowerups[EnemyGridPowerups.Count - 1].StoreSprites(moveSpeedSprite, attackSpeedSprite, attackDamageSprite);
            //Init the image object in powerup class
            EnemyGridPowerups[EnemyGridPowerups.Count - 1].PowerUpTexture = Instantiate(SampleImage);
            //Decides the powerup modifiers based on powertype
            EnemyGridPowerups[EnemyGridPowerups.Count - 1].AssignType();
            //Change the position of powerup
            EnemyGridPowerups[EnemyGridPowerups.Count - 1].ChangePosition();
        }
    }
}

public class PowerUp
{
    public PowerupsSystem.POWERUP_TYPE powerType;
    public Vector2 powerupPosition = new Vector2();
    public Image PowerUpTexture;
    
    public float AddedMoveSpeed = 0;
    public float AddedAttackSpeed = 0;
    public float AddedAttackDamage = 0;

    Sprite moveSpeedSprite;
    Sprite attackSpeedSprite;
    Sprite attackDamageSprite;

    public void StoreSprites(Sprite movespeed, Sprite attackspeed, Sprite attackdamage)
    {
        moveSpeedSprite = movespeed;
        attackSpeedSprite = attackspeed;
        attackDamageSprite = attackdamage;
    }

    public PowerUp(Vector2 NewPosition, PowerupsSystem.POWERUP_TYPE NewPowerType = PowerupsSystem.POWERUP_TYPE.TOTAL_POWERUPS)
    {
        if (NewPowerType == PowerupsSystem.POWERUP_TYPE.TOTAL_POWERUPS)
        {
            //If there is no specified power type passed in, a random powerup is assigned
            powerType = (PowerupsSystem.POWERUP_TYPE)Random.Range(0, (float)PowerupsSystem.POWERUP_TYPE.TOTAL_POWERUPS);
        }
        else
        {
            powerType = NewPowerType;
        }

        powerupPosition = NewPosition;
    }

    public void AssignType(PowerupsSystem.POWERUP_TYPE NewPowerType = PowerupsSystem.POWERUP_TYPE.TOTAL_POWERUPS)
    {
        //NOTE: PowerUpTexture must be initialised before this function can be used!

        if (NewPowerType != PowerupsSystem.POWERUP_TYPE.TOTAL_POWERUPS)
        {
            powerType = NewPowerType;
        }

        switch (powerType)
        {
            case PowerupsSystem.POWERUP_TYPE.POWERUP_MOVESPEED:
                {
                    //Unit stats modify
                    AddedMoveSpeed = 5;
                    PowerUpTexture.sprite = moveSpeedSprite;
                    break;
                }
            case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKSPEED:
                {
                    //Unit stats modify
                    AddedAttackSpeed = 0.5f;
                    PowerUpTexture.sprite = attackSpeedSprite;
                    break;
                }
            case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKDAMAGE:
                {
                    //Unit stats modify
                    AddedAttackDamage = 10;
                    PowerUpTexture.sprite = attackDamageSprite;
                    break;
                }
            default:
                break;
        }

        PowerUpTexture.enabled = true;

        Vector2 canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;
        PowerUpTexture.rectTransform.sizeDelta = new Vector2(GridSystem.tileWidth * canvasLocalScale.x, GridSystem.tileHeight * canvasLocalScale.y);
    }

    public void ChangePosition(Vector2 NewPosition)
    {
        powerupPosition = NewPosition;
        PowerUpTexture.transform.position = powerupPosition;
    }

    public void ChangePosition()
    {
        PowerUpTexture.transform.position = powerupPosition;
    }
}