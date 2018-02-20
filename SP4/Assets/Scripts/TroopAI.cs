using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopAI : MonoBehaviour {

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
    public int targetIndex;
    public int team;
    public int state;
    public float prevhealth;
    public bool activ;
    public float aggrotimer;
    public float attacktimer;
    private GameCode game = null;
    Vector3 pos;
    GameObject nearest;
    private TroopAI nearestAI = null;
    // Use this for initialization
    void Start () {

        game = GameObject.Find("EventSystem").GetComponent<GameCode>();
        if (type == "Cavalry")
        {
            Debug.Log("Cavalry");
            health = 40;
            attckDmg = 15;
            attckSpd = 0.2f;
            speed = 3;
            vision = 200;
            range = 100;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 3;
        }
        else if (type == "Infantry")
        {
            Debug.Log("Infantry");
            health = 50;
            attckDmg = 20;
            attckSpd = 0.1f;
            speed = 2;
            range = 100;
            vision = 100;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 1;
        }
        else if (type == "Bowmen")
        {
            Debug.Log("Bowmen");
            health = 30;
            attckDmg = 10;
            attckSpd = 0.2f;
            speed = 2;
            range = 300;
            vision = 300;
            state = (int)States.CHARGE;
            prevhealth = health;
            activ = true;
            _class = 2;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(health <= 0)
        {
            activ = false;
        }
        if (activ)
        {
            if (state == (int)States.CHARGE)
            {
                Vector3 hello = transform.position;
                hello.x = 0;
                hello.y = team;
                hello.z = 0;
                transform.position += hello * speed;

                if (prevhealth != health)
                {
                    state = (int)States.CHASE;
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
                        Vector2 hello1 = new Vector2(game.objects[i].transform.position.x - transform.position.x, game.objects[i].transform.position.y - transform.position.y);
                        float dist = hello1.SqrMagnitude();
                        if (dist <= vision * vision && dist < minNearest)
                        {
                            minNearest = dist;
                            nearest = game.objects[i];
                        }
                    }
                }
                if (nearest != null)
                {
                    state = (int)States.CHASE;
                }
            }
            if (state == (int)States.CHASE)
            {
                aggrotimer += 1 / 60;
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
                                    Debug.Log(dist);
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
                            transform.position += hello2 * speed;
                        }
                        else
                        {
                            Vector3 hello2 = new Vector3(0, nearest.transform.position.y - transform.position.y, 0);
                            hello2.Normalize();
                            transform.position += hello2 * speed;
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
                attacktimer += 1 / 60;
                if (nearest != null)
                {
                    if (nearestAI.activ)
                    {
                        if (attacktimer % (1.0f / attckSpd) == 0)
                        {
                            if (_class == nearestAI._class)
                            {
                                nearestAI.health -= attckDmg;
                            }
                            if (_class == 1 && nearestAI._class == 2)
                            {
                                nearestAI.health -= attckDmg * 1.2f;
                            }
                            if (_class == 2 && nearestAI._class == 3)
                            {
                                nearestAI.health -= attckDmg * 1.2f;
                            }
                            if (_class == 3 && nearestAI._class == 1)
                            {
                                nearestAI.health -= attckDmg * 1.2f;
                            }
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
        }

	}
}
