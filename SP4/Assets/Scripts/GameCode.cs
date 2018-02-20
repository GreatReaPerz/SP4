using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCode : MonoBehaviour {

    [SerializeField]
    Canvas Ui;


    [SerializeField]
    GameObject Terrain;
    [SerializeField]
    GameObject Player1;
    [SerializeField]
    GameObject Player2;
    enum GameState
    {
        PLANNING,
        ATTACK
    };
    private float timer;
    private bool destroyed;
    private TetrisSpawner theTetrisSpawner = null;
    private GridSystem theGridSystem = null;
    private enemyTetrisSpawner enemyTetrisSpawner = null;
    private enemyGridSystem enemyGridSystem = null;
    private TroopAI troop = null;
    private string TerrainName;
    int state;
    public List<GameObject> objects;
    // Use this for initialization
    void Start () {
        theTetrisSpawner = GameObject.Find("Spawner").GetComponent<TetrisSpawner>();
        theGridSystem = GameObject.Find("PlayerTetrisGrid").GetComponent<GridSystem>();
        enemyTetrisSpawner = GameObject.Find("enemySpawner").GetComponent<enemyTetrisSpawner>();
        enemyGridSystem = GameObject.Find("EnemyTetrisGrid").GetComponent<enemyGridSystem>();
        timer = 0;
        destroyed = false;
        state = (int)GameState.PLANNING;
        TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
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
                    Vector3 hello = theTetrisSpawner.tetrisList[i].partOne.transform.position;
                    hello.z = 100;
                    Debug.Log(theTetrisSpawner.tetrisList[i].troopName);
                    newObj = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj.transform.parent = Ui.transform;
                    troop = newObj.GetComponent<TroopAI>();
                    troop.team = 1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj);

                    GameObject newObj1;
                    hello = theTetrisSpawner.tetrisList[i].partTwo.transform.position;
                    hello.z = 100;
                    newObj1 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj1.transform.parent = Ui.transform;
                    troop = newObj1.GetComponent<TroopAI>();
                    troop.team = 1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj1);

                    GameObject newObj2;
                    hello = theTetrisSpawner.tetrisList[i].partThree.transform.position;
                    hello.z = 100;
                    newObj2 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj2.transform.parent = Ui.transform;
                    troop = newObj2.GetComponent<TroopAI>();
                    troop.team = 1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj2);

                    GameObject newObj3;
                    hello = theTetrisSpawner.tetrisList[i].partFour.transform.position;
                    hello.z = 100;
                    newObj3 = (GameObject)Instantiate(GameObject.Find(theTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj3.transform.parent = Ui.transform;
                    troop = newObj3.GetComponent<TroopAI>();
                    troop.team = 1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj3);
                }
            }
            for (int i = 0; i < 3; ++i)
            {
                if (enemyTetrisSpawner.tetrisList[i].sav)
                {
                    GameObject newObj;
                    Vector3 hello = enemyTetrisSpawner.tetrisList[i].partOne.transform.position;
                    hello.z = 100;
                    newObj = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj.transform.parent = Ui.transform;
                    troop = newObj.GetComponent<TroopAI>();
                    troop.team = -1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj);

                    GameObject newObj1;
                    hello = enemyTetrisSpawner.tetrisList[i].partTwo.transform.position;
                    hello.z = 100;
                    newObj1 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj1.transform.parent = Ui.transform;
                    troop = newObj1.GetComponent<TroopAI>();
                    troop.team = -1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj1);

                    GameObject newObj2;
                    hello = enemyTetrisSpawner.tetrisList[i].partThree.transform.position;
                    hello.z = 100;
                    newObj2 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj2.transform.parent = Ui.transform;
                    troop = newObj2.GetComponent<TroopAI>();
                    troop.team = -1;
                    troop.terrainName = TerrainName;
                    objects.Add(newObj2);

                    GameObject newObj3;
                    hello = enemyTetrisSpawner.tetrisList[i].partFour.transform.position;
                    hello.z = 100;
                    newObj3 = (GameObject)Instantiate(GameObject.Find(enemyTetrisSpawner.tetrisList[i].troopName), hello, Quaternion.identity);
                    newObj3.transform.parent = Ui.transform;
                    troop = newObj3.GetComponent<TroopAI>();
                    troop.team = -1;
                    troop.terrainName = TerrainName;
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
            Terrain.GetComponent<MainGame>().Start();
            TerrainName = Terrain.GetComponent<MainGame>().NeutralZoneTerrainType;
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
                for (int i = 0; i < objects.Count; ++ i)
                {
                    troop = objects[i].GetComponent<TroopAI>();
                    if (troop.activ)
                    {
                        if(troop.team == 1)
                        {
                            if(objects[i].transform.position.y > 2000)
                            {
                                troop.activ = false;
                                Player2.GetComponent<HealthSystem>().addHealth(-1);
                            }
                        }
                        if (troop.team == -1)
                        {
                            if (objects[i].transform.position.y < 100)
                            {
                                troop.activ = false;
                                Player1.GetComponent<HealthSystem>().addHealth(-1);
                            }
                        }
                    }
                }
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
