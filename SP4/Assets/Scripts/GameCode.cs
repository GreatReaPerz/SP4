using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCode : MonoBehaviour {

    [SerializeField]
    Canvas Ui;


    private float timer;
    private bool destroyed;
    private TetrisSpawner theTetrisSpawner = null;
    private GridSystem theGridSystem = null;
    private enemyTetrisSpawner enemyTetrisSpawner = null;
    private enemyGridSystem enemyGridSystem = null;
    private TroopAI troop = null;
    public List<GameObject> objects;
    // Use this for initialization
    void Start () {
        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner>();
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        enemyTetrisSpawner = GameObject.Find("enemySpawner").GetComponent<enemyTetrisSpawner>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        timer = 0;
        destroyed = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(timer > 5.0f && !destroyed)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (theTetrisSpawner.tetrisList[i].sav)
                {
                    GameObject newObj;
                    Vector3 hello = theTetrisSpawner.tetrisList[i].btmLeft.transform.position;
                    hello.z = 100;
                    newObj = (GameObject)Instantiate(GameObject.Find("Bowmen"), hello, Quaternion.identity);
                    newObj.transform.parent = Ui.transform;
                    troop = newObj.GetComponent<TroopAI>();
                    troop.team = 1;
                    objects.Add(newObj);

                    GameObject newObj1;
                    hello = theTetrisSpawner.tetrisList[i].btmRight.transform.position;
                    hello.z = 100;
                    newObj1 = (GameObject)Instantiate(GameObject.Find("Bowmen"), hello, Quaternion.identity);
                    newObj1.transform.parent = Ui.transform;
                    troop = newObj1.GetComponent<TroopAI>();
                    troop.team = 1;
                    objects.Add(newObj1);

                    GameObject newObj2;
                    hello = theTetrisSpawner.tetrisList[i].topLeft.transform.position;
                    hello.z = 100;
                    newObj2 = (GameObject)Instantiate(GameObject.Find("Bowmen"), hello, Quaternion.identity);
                    newObj2.transform.parent = Ui.transform;
                    troop = newObj2.GetComponent<TroopAI>();
                    troop.team = 1;
                    objects.Add(newObj2);

                    GameObject newObj3;
                    hello = theTetrisSpawner.tetrisList[i].topRight.transform.position;
                    hello.z = 100;
                    newObj3 = (GameObject)Instantiate(GameObject.Find("Bowmen"), hello, Quaternion.identity);
                    newObj3.transform.parent = Ui.transform;
                    troop = newObj3.GetComponent<TroopAI>();
                    troop.team = 1;
                    objects.Add(newObj3);
                }
            }
            for (int i = 0; i < 3; ++i)
            {
                if (enemyTetrisSpawner.tetrisList[i].sav)
                {
                    GameObject newObj;
                    Vector3 hello = enemyTetrisSpawner.tetrisList[i].btmLeft.transform.position;
                    hello.z = 100;
                    newObj = (GameObject)Instantiate(GameObject.Find("Infantry"), hello, Quaternion.identity);
                    newObj.transform.parent = Ui.transform;
                    troop = newObj.GetComponent<TroopAI>();
                    troop.team = -1;
                    objects.Add(newObj);

                    GameObject newObj1;
                    hello = enemyTetrisSpawner.tetrisList[i].btmRight.transform.position;
                    hello.z = 100;
                    newObj1 = (GameObject)Instantiate(GameObject.Find("Infantry"), hello, Quaternion.identity);
                    newObj1.transform.parent = Ui.transform;
                    troop = newObj1.GetComponent<TroopAI>();
                    troop.team = -1;
                    objects.Add(newObj1);

                    GameObject newObj2;
                    hello = enemyTetrisSpawner.tetrisList[i].topLeft.transform.position;
                    hello.z = 100;
                    newObj2 = (GameObject)Instantiate(GameObject.Find("Infantry"), hello, Quaternion.identity);
                    newObj2.transform.parent = Ui.transform;
                    troop = newObj2.GetComponent<TroopAI>();
                    troop.team = -1;
                    objects.Add(newObj2);

                    GameObject newObj3;
                    hello = enemyTetrisSpawner.tetrisList[i].topRight.transform.position;
                    hello.z = 100;
                    newObj3 = (GameObject)Instantiate(GameObject.Find("Infantry"), hello, Quaternion.identity);
                    newObj3.transform.parent = Ui.transform;
                    troop = newObj3.GetComponent<TroopAI>();
                    troop.team = -1;
                    objects.Add(newObj3);
                }
            }

            for (int i = 0; i < 3; ++i)
            {
                Destroy(theTetrisSpawner.tetrisList[i].parentCube);
                Destroy(enemyTetrisSpawner.tetrisList[i].parentCube);
                destroyed = true;
            }

                Destroy(theGridSystem);
                Destroy(enemyGridSystem);

            timer = 0.0f;
        }
        if (objects.Count <= 0 && destroyed)
        {
            theTetrisSpawner.Start();
            theGridSystem.Start();
            enemyTetrisSpawner.Start();
            enemyGridSystem.Start();
            destroyed = false;
            timer = 0.0f;
        }

        if (!destroyed)
        {
            theGridSystem.GameUpdate();
            enemyGridSystem.GameUpdate();
        }
        if (destroyed)
        {
            if(objects.Count > 0)
            {
                for (int i = 0; i < objects.Count;)
                {
                    troop = objects[i].GetComponent<TroopAI>();
                    if(troop.activ)
                    {
                        ++i;
                    }
                    else
                    {
                        Destroy(objects[i]);
                        objects.RemoveAt(i);
                    }
                }
            }
        }
        timer += 1.0f / 60.0f;
	}
}
