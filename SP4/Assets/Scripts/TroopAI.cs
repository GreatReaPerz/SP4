using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAI : MonoBehaviour {

    [SerializeField]
    Animator PauseAnimator;
    enum States
    {
        CHARGE,
        CHASE,
        ATTACK
    };
    //public bool active;
    public string type;
    public float health;
    public int _class;
    public float attckDmg;
    public float attckSpd;
    public float speed;
    public float range;
    public float vision;
    public uint targetIndex;
    public int team;
    public int state;
    public float prevhealth;
    public bool activ;
    public float aggrotimer;
    public float attacktimer;
    public float attackWidth;
    public string terrainName;
    private GameCode game = null;
    public Vector3 originPos;
    public Vector3 targetPos;
    GameObject nearest;
    private TroopAI nearestAI = null;

    private PowerupsSystem thePowerupsSystem = null;

    GameObject thePlayer;
    // Use this for initialization
    void Start()
    {
        thePowerupsSystem = GameObject.Find("PowerUpSystem").GetComponent<PowerupsSystem>();

        thePlayer = GameObject.Find("Player");
        game = GameObject.Find("EventSystem").GetComponent<GameCode>();
        originPos = transform.position;
        attackWidth = 50;
        //Debug.Log("Cavalry");
        if (type == "Cavalry")
        {
            health = 40;
            attckDmg = 15;
            //Debug.Log("Cavalry");
            //health = PlayerPrefs.GetFloat("cavalryHealth");
            //attckDmg = PlayerPrefs.GetFloat("cavalryDamage");
            //health = 
            attckSpd = 0.2f;
            speed = 175 * 0.016f;
            vision = 100;
            range = 100;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 3;
            if (terrainName == "Hills")
            {
                attckDmg -= (attckDmg * 0.15f);
                speed -= (speed * 0.25f);
                attckSpd -= (attckSpd * 0.15f);
            }
            else if (terrainName == "Forest")
            {
                attckDmg -= (attckDmg * 0.1f);
                speed -= (speed * 0.15f);
                attckSpd -= (attckSpd * 0.05f);
            }
            else if (terrainName == "River")
            {
                attckDmg += (attckDmg * 0.1f);
                speed -= (speed * 0.1f);
                attckSpd -= (attckSpd * 0.1f);
            }
            else if (terrainName == "Plains")
            {
                attckDmg += (attckDmg * 0.1f);
                speed += (speed * 0.15f);
                attckSpd += (attckSpd * 0.1f);
            }
        }
        else if (type == "Infantry")
        {
            //Debug.Log("Infantry");
            health = 50;
            attckDmg = 20;
            // health = PlayerPrefs.GetFloat("infantryHealth");
            //  attckDmg = PlayerPrefs.GetFloat("infantryDamage");
            attckSpd = 0.1f;
            speed = 150 * 0.016f;
            range = 100;
            vision = 100;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 1;
            if (terrainName == "Hills")
            {
                attckDmg -= (attckDmg * 0.1f);
                speed -= (speed * 0.15f);
                attckSpd -= (attckSpd * 0.1f);
            }
            else if (terrainName == "Forest")
            {
                attckDmg += (attckDmg * 0.1f);
                speed -= (speed * 0.1f);
                attckSpd -= (attckSpd * 0.05f);
            }
            else if (terrainName == "River")
            {
                attckDmg -= (attckDmg * 0.1f);
                speed -= (speed * 0.15f);
                attckSpd -= (attckSpd * 0.1f);
            }
            else if (terrainName == "Plains")
            {
                attckDmg += (attckDmg * 0.1f);
                speed += (speed * 0.2f);
                attckSpd += (attckSpd * 0.1f);
            }
        }
        else if (type == "Bowmen")
        {
            //Debug.Log("Bowmen");
            health = 30;
            attckDmg = 10;
            //health = PlayerPrefs.GetFloat("bowmenHealth");
            // attckDmg = PlayerPrefs.GetFloat("bowmenDamage");
            attckSpd = 0.2f;
            speed = 150 * 0.016f;
            range = 300;
            vision = 300;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 2;
            if (terrainName == "Hills")
            {
                attckDmg += (attckDmg * 0.1f);
                speed += (speed * 0.05f);
                attckSpd -= (attckSpd * 0.1f);
            }
            else if (terrainName == "Forest")
            {
                attckDmg -= (attckDmg * 0.15f);
                speed -= (speed * 0.1f);
                attckSpd -= (attckSpd * 0.15f);
            }
            else if (terrainName == "River")
            {
                attckDmg -= (attckDmg * 0.1f);
                speed -= (speed * 0.1f);
                attckSpd -= (attckSpd * 0.1f);
            }
            else if (terrainName == "Plains")
            {
                attckDmg += (attckDmg * 0.1f);
                speed += (speed * 0.1f);
                attckSpd += (attckSpd * 0.2f);
            }
        }
            //Debug.Log(transform.position);
            //targetPos = transform.position;
    }
        // Update is called once per frame
       void Update (){
            if (PauseAnimator.GetBool("PauseEnabled") == true)
                return;
            if (health <= 0)
            {
                //if (team == -1)
                //    thePlayer.GetComponent<InGameCash>().addAmount(10);
                activ = false;
            }
            if (activ)
            {
                if (state == (int)States.CHARGE)
                {
                    Vector3 hello = targetPos - transform.position;
                    //hello.x = 0;
                    //hello.y = team;
                    //hello.z = 0;
                    hello.Normalize();
                    bool collided = false;
                    for (int i = 0; i < game.objects.Count; ++i)
                    {
                        Vector3 nextPosition = transform.position + hello * speed;
                        if (!(game.objects[i].transform.position.x == transform.position.x && game.objects[i].transform.position.y == transform.position.y))
                        {
                            if (Collided(nextPosition, game.objects[i].transform.position))
                            {
                                collided = true;
                            }
                        }
                    }
                    if (!collided)
                    {
                        transform.position += hello * speed;
                    }

                    if (prevhealth != health)
                    {
                        // state = (int)States.CHASE;
                        aggrotimer = 0;
                    }
                    prevhealth = health;
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
                            if (game.objects[i].transform.position.x > transform.position.x - attackWidth && game.objects[i].transform.position.x < transform.position.x + attackWidth && (game.objects[i].transform.position - transform.position).magnitude < range + 20)
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
                if (state == (int)States.CHASE)
                {
                    aggrotimer += Time.deltaTime;
                    if (prevhealth == health && aggrotimer % 5 == 0 || aggrotimer % 5 == 0)
                    {
                        state = (int)States.CHARGE;
                    }
                    if (nearest == null)
                    {
                        state = (int)States.CHARGE;
                    }
                    else
                    {
                        if (nearestAI.activ)
                        {
                            float minNearest = 1000000;
                            for (int i = 0; i < game.objects.Count; ++i)
                            {
                                nearestAI = game.objects[i].GetComponent<TroopAI>();
                                if (nearestAI.activ && nearestAI.team != team)
                                {
                                    Vector2 hello1 = new Vector2(game.objects[i].transform.position.x - transform.position.x, game.objects[i].transform.position.y - transform.position.y);
                                    float dist = hello1.SqrMagnitude();
                                    if (dist <= vision * vision && dist < minNearest)
                                    {
                                        //Debug.Log(dist);
                                        minNearest = dist;
                                        nearest = game.objects[i];
                                    }
                                }
                            }
                            nearestAI = nearest.GetComponent<TroopAI>();
                            float Xdistance = Mathf.Abs(nearest.transform.position.x - transform.position.x);
                            float Ydistance = Mathf.Abs(nearest.transform.position.y - transform.position.y);
                            if (Xdistance >= Ydistance)
                            {
                                Vector3 hello2 = new Vector3(nearest.transform.position.x - transform.position.x, 0, 0);
                                hello2.Normalize();
                                bool collided = false;
                                for (int i = 0; i < game.objects.Count; ++i)
                                {
                                    Vector3 nextPosition = transform.position + hello2 * speed;
                                    if (game.objects[i].transform.position.x != transform.position.x || game.objects[i].transform.position.y != transform.position.y)
                                    {
                                        if (Collided(nextPosition, game.objects[i].transform.position))
                                        {
                                            collided = true;
                                        }
                                    }
                                }
                                if (!collided)
                                {
                                    transform.position += hello2 * speed;
                                }
                            }
                            else
                            {
                                Vector3 hello2 = new Vector3(0, nearest.transform.position.y - transform.position.y, 0);
                                hello2.Normalize();
                                bool collided = false;
                                for (int i = 0; i < game.objects.Count; ++i)
                                {
                                    Vector3 nextPosition = transform.position + hello2 * speed;
                                    if (game.objects[i].transform.position.x != transform.position.x || game.objects[i].transform.position.y != transform.position.y)
                                    {
                                        if (Collided(nextPosition, game.objects[i].transform.position))
                                        {
                                            collided = true;
                                        }
                                    }
                                }
                                if (!collided)
                                {
                                    transform.position += hello2 * speed;
                                }
                            }
                            Vector3 hello3 = new Vector3(nearest.transform.position.x - transform.position.x, nearest.transform.position.y - transform.position.y, 0);
                            float dis = hello3.sqrMagnitude;
                            if (dis <= range * range)
                            {
                                state = (int)States.ATTACK;
                                // attacktimer = 0.0f;
                            }
                        }
                        else
                        {
                            state = (int)States.CHARGE;
                        }
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
                                    nearestAI.health -= 100;
                                }
                                if (_class == 1 && nearestAI._class == 3)
                                {
                                    nearestAI.health -= 100 * 1.2f;
                                }
                                if (_class == 1 && nearestAI._class == 2)
                                {
                                    nearestAI.health -= 100 * 1.2f;
                                }
                                if (_class == 2 && nearestAI._class == 1)
                                {
                                    nearestAI.health -= 100 * 1.2f;
                                }
                                if (_class == 2 && nearestAI._class == 3)
                                {
                                    nearestAI.health -= 100 * 1.2f;
                                }
                                if (_class == 3 && nearestAI._class == 1)
                                {
                                    nearestAI.health -= 100 * 1.2f;
                                }
                                if (_class == 3 && nearestAI._class == 2)
                                {
                                    nearestAI.health -= 100 * 1.2f;
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
            if (thePowerupsSystem)
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
                                        attckSpd += thePowerupsSystem.PlayerGridPowerups[i].AddedAttackSpeed;
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


        }

    }
    bool Collided(Vector3 firstTroop, Vector3 secondTroop)
    {
        if (firstTroop.x - (99 * 0.5f) < secondTroop.x + (99 * 0.5f) 
            &&  firstTroop.x + (99 * 0.5f) > secondTroop.x - (99 * 0.5f)
            && firstTroop.y - (99 * 0.5f) < secondTroop.y + (99 * 0.5f)
            && (99 * 0.5f) + firstTroop.y > secondTroop.y - (99 * 0.5f))
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
