using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TroopAI : MonoBehaviour {

    [SerializeField]
    Animator PauseAnimator;

    [SerializeField]
    GameObject projectileOBJ;


    //[SerializeField]
    //GameObject parenting;

    enum States
    {
        CHARGE,
        CHASE,
        ATTACK
    };
    //public bool active;
    public string type;
    //public float health;
    public HealthSystem health;
    public int _class;
    public float attckDmg;
    public float attckSpd;
    public float speed;
    public float range;
    public float vision;
    public uint targetIndex;
    public int team;
    public int state;
    //public float prevhealth;
    public bool activ;
    public bool fireProj= false;
    public float aggrotimer;
    public float attacktimer;
    public float attackWidth;
    public float attackHeight;
    public string terrainName;
    private GameCode game = null;
    public Vector3 originPos;
    private Vector3 targetPos;
    public Vector3 Pos;
    GameObject nearest;
    private TroopAI nearestAI = null;

    private Projectile theProjectile = null;

    private PowerupsSystem thePowerupsSystem = null;

    private TrapSystem theTrapSystem = null;

    private GridSystem theGridSystem = null;
    private enemyGridSystem enemyGridSystem = null;

    Vector2 canvasLocalScale; 


    GameObject thePlayer;
    // Use this for initialization
    void Start()
    {
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        thePowerupsSystem = GameObject.Find("PowerUpSystem").GetComponent<PowerupsSystem>();
        if(!enemyGridSystem.multi)
        {
            theTrapSystem = GameObject.Find("TrapSystem").GetComponent<TrapSystem>();
        }


        thePlayer = GameObject.Find("Player");
        game = GameObject.Find("EventSystem").GetComponent<GameCode>();

        health = this.gameObject.AddComponent<HealthSystem>();

        attackHeight = 40;
        originPos = transform.position;
        Pos = originPos;
        canvasLocalScale = GameObject.FindGameObjectWithTag("Canvas").transform.localScale;


        //Attack adjacent tiles
        attackWidth = 150;



        //Debug.Log("Cavalry");
        if (type == "Cavalry")
        {
            health.InitHealth(PlayerPrefs.GetFloat("calvaryHP"));
            attckDmg = PlayerPrefs.GetFloat("calvaryAtt");
            attckSpd = PlayerPrefs.GetFloat("calvaryAttSpd");
            speed = PlayerPrefs.GetFloat("calvarySpd");

            vision = 100;
            range = 100;
            state = (int)States.CHARGE;
            //prevhealth = health;
            activ = true;
            _class = 3;
            if (terrainName == "Hills")
            {
                attckDmg = (attckDmg * game.TMV_Cavalry.attackDamage);
                speed = (speed * game.TMV_Cavalry.speed);
                attckSpd = (attckSpd * game.TMV_Cavalry.attackSpeed);
            }
            else if (terrainName == "Forest")
            {
                attckDmg = (attckDmg * game.TMV_Cavalry.attackDamage);
                speed = (speed * game.TMV_Cavalry.speed);
                attckSpd = (attckSpd * game.TMV_Cavalry.attackSpeed);
            }
            else if (terrainName == "River")
            {
                attckDmg = (attckDmg * game.TMV_Cavalry.attackDamage);
                speed = (speed * game.TMV_Cavalry.speed);
                attckSpd = (attckSpd * game.TMV_Cavalry.attackSpeed);
            }
            else if (terrainName == "Plains")
            {
                attckDmg = (attckDmg * game.TMV_Cavalry.attackDamage);
                speed = (speed * game.TMV_Cavalry.speed);
                attckSpd = (attckSpd * game.TMV_Cavalry.attackSpeed);
            }
        }
        else if (type == "Infantry")
        {
           // Debug.Log("Infantry");
            health.InitHealth(PlayerPrefs.GetFloat("infantryHP"));
            attckDmg = PlayerPrefs.GetFloat("infantryAtt", attckDmg);
            attckSpd = PlayerPrefs.GetFloat("infantryAttSpd");
            speed = PlayerPrefs.GetFloat("infantrySpd");
            range = 100;

            vision = 100;
            state = (int)States.CHARGE;
            //prevhealth = health;
            activ = true;
            _class = 1;
            if (terrainName == "Hills")
            {
                attckDmg = (attckDmg * game.TMV_Infantry.attackDamage);
                speed = (speed * game.TMV_Infantry.speed);
                attckSpd = (attckSpd * game.TMV_Infantry.attackSpeed);
            }
            else if (terrainName == "Forest")
            {
                attckDmg = (attckDmg * game.TMV_Infantry.attackDamage);
                speed = (speed * game.TMV_Infantry.speed);
                attckSpd = (attckSpd * game.TMV_Infantry.attackSpeed);
            }
            else if (terrainName == "River")
            {
                attckDmg = (attckDmg * game.TMV_Infantry.attackDamage);
                speed = (speed * game.TMV_Infantry.speed);
                attckSpd = (attckSpd * game.TMV_Infantry.attackSpeed);
            }
            else if (terrainName == "Plains")
            {
                attckDmg = (attckDmg * game.TMV_Infantry.attackDamage);
                speed = (speed * game.TMV_Infantry.speed);
                attckSpd = (attckSpd * game.TMV_Infantry.attackSpeed);
            }
        }
        else if (type == "Bowmen")
        {
           // Debug.Log("Bowmen");
            health.InitHealth(PlayerPrefs.GetFloat("bowmenHP"));
            attckDmg = PlayerPrefs.GetFloat("bowmenAtt", attckDmg);
            attckSpd = PlayerPrefs.GetFloat("bowmenAttSpd");
            speed = PlayerPrefs.GetFloat("bowmenSpd");
            theProjectile = GetComponent<Projectile>();
            range = 300;
            vision = 300;
            state = (int)States.CHARGE;
            //prevhealth = health;
            activ = true;
            _class = 2;
            if (terrainName == "Hills")
            {
                attckDmg = (attckDmg * game.TMV_Bowmen.attackDamage);
                speed = (speed * game.TMV_Bowmen.speed);
                attckSpd = (attckSpd * game.TMV_Bowmen.attackSpeed);
            }
            else if (terrainName == "Forest")
            {
                attckDmg = (attckDmg * game.TMV_Bowmen.attackDamage);
                speed = (speed * game.TMV_Bowmen.speed);
                attckSpd = (attckSpd * game.TMV_Bowmen.attackSpeed);
            }
            else if (terrainName == "River")
            {
                attckDmg = (attckDmg * game.TMV_Bowmen.attackDamage);
                speed = (speed * game.TMV_Bowmen.speed);
                attckSpd = (attckSpd * game.TMV_Bowmen.attackSpeed);
            }
            else if (terrainName == "Plains")
            {
                attckDmg = (attckDmg * game.TMV_Bowmen.attackDamage);
                speed = (speed * game.TMV_Bowmen.speed);
                attckSpd = (attckSpd * game.TMV_Bowmen.attackSpeed);
            }
        }
            //Debug.Log(transform.position
    }
    // Update is called once per frame
    public void SetTarget(Vector3 hi)
    {
        targetPos = hi;
    }
    public Vector3 GetTarget()
    {
        return targetPos;
    }
    void Update (){
            if (PauseAnimator.GetBool("PauseEnabled") == true)
                return;
            if (health.getHealth() <= 0)
            {
            if (team == -1)
                thePlayer.GetComponent<InGameCash>().addAmount(10);
            activ = false;
            }
            if (activ)
            {
                if (state == (int)States.CHARGE)
                {
                float dist = 0;
                if(team == 1)
                {
                    for (uint j = 0; j < enemyGridSystem.GridSize; ++j)
                    {
                        float yDist = enemyGridSystem.grid[j].transform.position.y - originPos.y;
                        //Debug.Log("TEST");

                        if (Mathf.Abs(enemyGridSystem.grid[j].transform.position.x - originPos.x) < 20 && !enemyGridSystem.IsGreyedOut(j) && Mathf.Abs(yDist) > dist)
                        {
                            //Debug.Log("TEST");
                            dist = Mathf.Abs(yDist);
                            targetPos = enemyGridSystem.grid[j].transform.position;
                            targetIndex = j;
                        }
                    }
                }
                else if(team == -1)
                {
                    for (uint j = 0; j < theGridSystem.GridSize; ++j)
                    {
                        float yDist = theGridSystem.grid[j].transform.position.y - originPos.y;
                        if (Mathf.Abs(theGridSystem.grid[j].transform.position.x - originPos.x) < 20 && !theGridSystem.IsGreyedOut(j) && Mathf.Abs(yDist) > dist)
                        {
                            dist = Mathf.Abs(yDist);
                            targetPos = theGridSystem.grid[j].transform.position;
                            targetIndex = j;
                        }
                    }
                }


                Vector3 hello = targetPos - Pos;
                    //hello.x = 0;
                    //hello.y = team;
                    //hello.z = 0;
                    hello.Normalize();
                    bool collided = false;
                    for (int i = 0; i < game.objects.Count; ++i)
                    {
                        Vector3 nextPosition = Pos + hello * speed;
                        if (!(Mathf.Abs(game.objects[i].transform.position.x - Pos.x) < 10 * canvasLocalScale.x && Mathf.Abs(game.objects[i].transform.position.y - Pos.y) < 10 * canvasLocalScale.y))
                        {
                            if (Collided(nextPosition, game.objects[i].transform.position))
                            {
                                collided = true;
                            }
                        }
                    }
                    if (!collided)
                    {
                    Pos += hello * speed;

                    }
            
                transform.position = Pos;
                    if (/*prevhealth != health*/health.isHealthModified())
                    {
                        // state = (int)States.CHASE;
                        aggrotimer = 0;
                    }
                    //prevhealth = health;
                    health.setHealthModifiedToFalse();
                    float minNearest = 1000000;
                    nearest = null;
                    for (int i = 0; i < game.objects.Count; ++i)
                    {
                        nearestAI = game.objects[i].GetComponent<TroopAI>();
                        if (nearestAI.activ && nearestAI.team != team)
                        {
                            //Vector2 hello1 = new Vector2(game.objects[i].transform.position.x - transform.position.x, game.objects[i].transform.position.y - transform.position.y);
                            //float dist = hello1.SqrMagnitude();
                            //if (dist <= vision * vision && dist < minNearest)
                            //{
                            //    minNearest = dist;
                            //    nearest = game.objects[i];
                            //}
                            if (game.objects[i].transform.position.x > transform.position.x - attackWidth && game.objects[i].transform.position.x < transform.position.x + attackWidth && (game.objects[i].transform.position - transform.position).magnitude < range + 10  * canvasLocalScale.x && Mathf.Abs(game.objects[i].transform.position.y - transform.position.y) < attackHeight + 10 * canvasLocalScale.y)
                            {
                                nearest = game.objects[i];
                            }
                        }
                    }
                    if (nearest != null)
                    {
                        state = (int)States.ATTACK;
                    }
                }

                if (state == (int)States.ATTACK)
                {
                    attacktimer += Time.deltaTime;
                    if (nearest != null)
                    {
                        nearestAI = nearest.GetComponent<TroopAI>();
                        if (nearestAI.activ)
                        {
                            if (attacktimer > (attckSpd))
                            {
                           
                                //Class 1 = Infantry, Class 2 = Bowmen, Class 3 = Cavalry
                                attacktimer = 0;
                                if (_class == nearestAI._class)
                                {
                                if (!fireProj && _class == 2 && nearestAI)                                
                                    theProjectile.CreateProjectile(projectileOBJ,this, nearestAI, attckDmg);                              
                                else if(_class != 2)
                                    nearestAI.health.addHealth(-attckDmg );
                                }
                                if (_class == 1 && nearestAI._class == 3)
                                {
                                    nearestAI.health.addHealth(-attckDmg * 3);
                                }
                                if (_class == 1 && nearestAI._class == 2)
                                {
                                    nearestAI.health.addHealth(-attckDmg);
                                }
                                if (_class == 2 && nearestAI._class == 1)
                                {
                                if (!fireProj && nearestAI)
                                    theProjectile.CreateProjectile(projectileOBJ, this, nearestAI, attckDmg + 10);
                                else
                                    nearestAI.health.addHealth(-attckDmg * 5);
                                }
                                if (_class == 2 && nearestAI._class == 3)
                                {
                                if (!fireProj && nearestAI)
                                    theProjectile.CreateProjectile(projectileOBJ, this, nearestAI, attckDmg);
                                else
                                    nearestAI.health.addHealth(-attckDmg);
                                }
                                if (_class == 3 && nearestAI._class == 1)
                                {
                                    nearestAI.health.addHealth(-attckDmg);
                                }
                                if (_class == 3 && nearestAI._class == 2)
                                {
                                    nearestAI.health.addHealth(-attckDmg * 3);
                                }
                                attacktimer = 0;
                            }
                        }
                        else
                        {
                            state = (int)States.CHARGE;
                        }
                    }
                    else
                    {
                        state = (int)States.CHARGE;
                    }
                }

            //Powerups
            if (thePowerupsSystem && thePowerupsSystem.PowerupsIsActive == true)
            {
                //Search player powerups list
                if (team == 1)
                {
                    for (int i = 0; i < thePowerupsSystem.PlayerGridPowerups.Count; ++i)
                    {
                        if (Collided(thePowerupsSystem.PlayerGridPowerups[i].powerupPosition, transform.position))
                        {
                            switch (thePowerupsSystem.PlayerGridPowerups[i].powerType)
                            {
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKDAMAGE:
                                    {
                                        attckDmg += thePowerupsSystem.PlayerGridPowerups[i].AddedAttackDamage;
                                        break;
                                    }
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKSPEED:
                                    {
                                        attckSpd -= thePowerupsSystem.PlayerGridPowerups[i].AddedAttackSpeed;
                                        break;
                                    }
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_MOVESPEED:
                                    {
                                        speed += thePowerupsSystem.PlayerGridPowerups[i].AddedMoveSpeed;
                                        break;
                                    }
                                default:
                                    break;
                            }

                            Destroy(thePowerupsSystem.PlayerGridPowerups[i].PowerUpTexture);
                            thePowerupsSystem.PlayerGridPowerups.RemoveAt(i);
                        }
                    }

                }
                else  //Search enemy player grid list
                {
                    for (int i = 0; i < thePowerupsSystem.EnemyGridPowerups.Count; ++i)
                    {
                        if (Collided(thePowerupsSystem.EnemyGridPowerups[i].powerupPosition, transform.position))
                        {
                            switch (thePowerupsSystem.EnemyGridPowerups[i].powerType)
                            {
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKDAMAGE:
                                    {
                                        attckDmg += thePowerupsSystem.EnemyGridPowerups[i].AddedAttackDamage;
                                        break;
                                    }
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_ATTACKSPEED:
                                    {
                                        attckSpd += thePowerupsSystem.EnemyGridPowerups[i].AddedAttackSpeed;
                                        break;
                                    }
                                case PowerupsSystem.POWERUP_TYPE.POWERUP_MOVESPEED:
                                    {
                                        speed += thePowerupsSystem.EnemyGridPowerups[i].AddedMoveSpeed;
                                        break;
                                    }
                                default:
                                    break;
                            }

                            Destroy(thePowerupsSystem.EnemyGridPowerups[i].PowerUpTexture);
                            thePowerupsSystem.EnemyGridPowerups.RemoveAt(i);
                        }
                    }
                }

            }
            if (theTrapSystem && theTrapSystem.trapSystemActive)
            {
                foreach (GameObject go in theTrapSystem.myTraps)
                {
                    Trap theTrap = go.GetComponent<Trap>();
                    if (!theTrap.isactive)
                        continue;
                    if (theTrap.team == team)
                        continue;
                    if (Collided(go.transform.position, transform.position))
                    {
                        theTrap.activateTrap(this);
                        //Destroy(theTrap);
                        theTrap.isactive = false;
                        go.SetActive(false);
                    }
                }
                theTrapSystem.cleanUpTraps();
            }

        }

    }
    bool Collided(Vector3 firstTroop, Vector3 secondTroop)
    {
        if (firstTroop.x - (90 * 0.5f * canvasLocalScale.x) < secondTroop.x + (90 * 0.5f * canvasLocalScale.x) 
            &&  firstTroop.x + (90 * 0.5f * canvasLocalScale.x) > secondTroop.x - (90 * 0.5f * canvasLocalScale.x)
            && firstTroop.y - (90 * 0.5f * canvasLocalScale.x) < secondTroop.y + (90 * 0.5f * canvasLocalScale.x)
            && (90 * 0.5f * canvasLocalScale.x) + firstTroop.y > secondTroop.y - (90 * 0.5f * canvasLocalScale.x))
        {
            //Debug.Log("collided");
            return true;
        }
        return false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(thePowerupsSystem == null)
    //    {
    //        return;
    //    }

    //    if(collision.gameObject.name == "PowerupSampleImage")
    //    {
    //        Destroy(collision.gameObject);
            
    //        //if(team == 1)
    //        //{
    //        //    thePowerupsSystem.PlayerGridPowerups.Remove(collision.gameObject);
    //        //}
    //    }
    //}
}
