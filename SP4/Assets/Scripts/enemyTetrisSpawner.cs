﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class enemyTetrisSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] TetrisTypes;


    public TetrisCube[] tetrisList = new TetrisCube[3];
    float timer = 0;
    int numSpawned = 0;
    private Vector3 pil;
    private Vector3 pil1;
    public bool SomethingIsMoving = false;
    public uint IndexofMovingObject = 0;

    // Use this for initialization
    public void Start()
    {

        numSpawned = 0;
        numSpawned = Spawn4x4Cube(numSpawned);
        numSpawned = Spawn4x4Cube(numSpawned);
        numSpawned = Spawn4x4Cube(numSpawned);
        //numSpawned = SpawnLShape (numSpawned);
        //numSpawned = SpawnZShape(numSpawned);
    }

    // Update is called once per frame
    void Update()
    {
        //Used to test spawning (dont spawn ontop of each other, will kinda bug out
        /*timer += Time.deltaTime;
		if (timer > 3) {
			Spawn4x4Cube ();
			timer = 0;
		}*/

        for (uint i = 0; i < 3; ++i)
        {
            SomethingIsMoving = false;

            if (tetrisList[i] == null)
            {
                continue;
            }

            if (tetrisList[i].isMoving)
            {
                SomethingIsMoving = true;
                IndexofMovingObject = i;
                break;
            }

            tetrisList[i].isMoving = false;
        }
    }

    int Spawn4x4Cube(int key)
    {
        TetrisCube theCube = new TetrisCube();
        theCube.parentCube = Instantiate(TetrisTypes[0], transform.position, Quaternion.identity);
        theCube.parentCube.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
        pil.Set(-300 + (key * 300), -300, -100);
        pil1 = theCube.parentCube.transform.position + pil;
        theCube.origin.Set(pil1.x,1920, pil1.z);
        theCube.parentCube.transform.position = theCube.origin;

		//Set up the 4 cubes based on theCube.parentCube's child
		theCube.setTheCubes (theCube.parentCube.transform.Find ("partOne").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partTwo").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partThree").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partFour").GetComponent<Rigidbody2D> ());
		theCube.setTheObjectType (TetrisCube.objectType.TETRIS_4X4); 

		//Could use raycast instead 
		//Also cause the only thing changing is the movement function, could try to make a switch instead
		//Trigger and entry for bottom left 
		EventTrigger BtmLTrig= theCube.parentCube.transform.Find("partOne").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmLEntry = new EventTrigger.Entry ();
		BtmLEntry.eventID = EventTriggerType.Drag;
		BtmLEntry.callback.AddListener ((data) => {
			theCube.DragObject (theCube.partOne);
		});
		BtmLTrig.triggers.Add (BtmLEntry);
		theCube.origin = theCube.partOne.position;

		//Trigger and entry for bottom Right 
		EventTrigger BtmRTrig = theCube.parentCube.transform.Find("partTwo").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmREntry = new EventTrigger.Entry ();
		BtmREntry.eventID = EventTriggerType.Drag;
		BtmREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partTwo);
		});
		BtmRTrig.triggers.Add (BtmREntry);

		//Trigger and entry for top Left 
		EventTrigger TopLTrig = theCube.parentCube.transform.Find("partThree").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopLEntry = new EventTrigger.Entry ();
		TopLEntry.eventID = EventTriggerType.Drag;
		TopLEntry.callback.AddListener ((data) => {
			theCube.DragObject (theCube.partThree);
		});
		TopLTrig.triggers.Add (TopLEntry);

		//Trigger and entry for top Right 
		EventTrigger TopRTrig = theCube.parentCube.transform.Find("partFour").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopREntry = new EventTrigger.Entry ();
		TopREntry.eventID = EventTriggerType.Drag;
		TopREntry.callback.AddListener ((data) => {
			theCube.DragObject (theCube.partFour);
		});
		TopRTrig.triggers.Add (TopREntry);

		tetrisList [key] = theCube;
		++key;
		return key;
    }

    public int SpawnTShape(int key)
    {
        TetrisCube theCube = new TetrisCube();
        theCube.parentCube = Instantiate(TetrisTypes[2], transform.position, Quaternion.identity);
        theCube.parentCube.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);


		//Set up the 4 cubes based on theCube.parentCube's child
		theCube.setTheCubes (theCube.parentCube.transform.Find ("partOne").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partTwo").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partThree").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partFour").GetComponent<Rigidbody2D> ());
		theCube.setTheObjectType (TetrisCube.objectType.TETRIS_T);
		//Could use raycast instead 
		//Also cause the only thing changing is the movement function, could try to make a switch instead

		//Trigger and entry for bottom left 
		EventTrigger BtmLTrig= theCube.parentCube.transform.Find("partOne").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmLEntry = new EventTrigger.Entry ();
		BtmLEntry.eventID = EventTriggerType.Drag;
		BtmLEntry.callback.AddListener ((data) => {
			theCube.DragObject (theCube.partOne);
		});
		BtmLTrig.triggers.Add (BtmLEntry);
		theCube.origin = theCube.partOne.position;
		//Trigger and entry for bottom Right 
		EventTrigger BtmRTrig = theCube.parentCube.transform.Find("partTwo").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmREntry = new EventTrigger.Entry ();
		BtmREntry.eventID = EventTriggerType.Drag;
		BtmREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partTwo);
		});
		BtmRTrig.triggers.Add (BtmREntry);

		//Trigger and entry for top Left 
		EventTrigger TopLTrig = theCube.parentCube.transform.Find("partThree").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopLEntry = new EventTrigger.Entry ();
		TopLEntry.eventID = EventTriggerType.Drag;
		TopLEntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partThree);
		});
		TopLTrig.triggers.Add (TopLEntry);

		//Trigger and entry for top Right 
		EventTrigger TopRTrig = theCube.parentCube.transform.Find("partFour").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopREntry = new EventTrigger.Entry ();
		TopREntry.eventID = EventTriggerType.Drag;
		TopREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partFour);
		});
		TopRTrig.triggers.Add (TopREntry);

		tetrisList [key] = theCube;
		++key;

		return key;
    }

    public int SpawnLShape(int key)
    {
        TetrisCube theCube = new TetrisCube();
        theCube.parentCube = Instantiate(TetrisTypes[1], transform.position, Quaternion.identity);
        theCube.parentCube.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);


		//Set up the 4 cubes based on theCube.parentCube's child
		theCube.setTheCubes (theCube.parentCube.transform.Find ("partOne").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partTwo").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partThree").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partFour").GetComponent<Rigidbody2D> ());
		theCube.setTheObjectType (TetrisCube.objectType.TETRIS_L);

		//Could use raycast instead 
		//Also cause the only thing changing is the movement function, could try to make a switch instead
		//Trigger and entry for bottom left 
		EventTrigger BtmLTrig= theCube.parentCube.transform.Find("partOne").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmLEntry = new EventTrigger.Entry ();
		BtmLEntry.eventID = EventTriggerType.Drag;
		BtmLEntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partOne);
		});
		BtmLTrig.triggers.Add (BtmLEntry);
		theCube.origin = theCube.partOne.position;
		//Trigger and entry for bottom Right 
		EventTrigger BtmRTrig = theCube.parentCube.transform.Find("partTwo").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmREntry = new EventTrigger.Entry ();
		BtmREntry.eventID = EventTriggerType.Drag;
		BtmREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partTwo);
		});
		BtmRTrig.triggers.Add (BtmREntry);

		//Trigger and entry for top Left 
		EventTrigger TopLTrig = theCube.parentCube.transform.Find("partThree").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopLEntry = new EventTrigger.Entry ();
		TopLEntry.eventID = EventTriggerType.Drag;
		TopLEntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partThree);
		});
		TopLTrig.triggers.Add (TopLEntry);

		//Trigger and entry for top Right 
		EventTrigger TopRTrig = theCube.parentCube.transform.Find("partFour").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopREntry = new EventTrigger.Entry ();
		TopREntry.eventID = EventTriggerType.Drag;
		TopREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partFour);
		});
		TopRTrig.triggers.Add (TopREntry);

		tetrisList [key] = theCube;
		++key;

		return key;
    }

    public int SpawnZShape(int key)
    {
        TetrisCube theCube = new TetrisCube();

        theCube.parentCube = Instantiate(TetrisTypes[3], transform.position, Quaternion.identity);
        theCube.parentCube.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);

		//Set up the 4 cubes based on theCube.parentCube's child
		theCube.setTheCubes (theCube.parentCube.transform.Find ("partOne").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partTwo").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partThree").GetComponent<Rigidbody2D> (), theCube.parentCube.transform.Find ("partFour").GetComponent<Rigidbody2D> ());
		theCube.setTheObjectType (TetrisCube.objectType.TETRIS_Z);
		//Could use raycast instead 
		//Also cause the only thing changing is the movement function, could try to make a switch instead
		//Trigger and entry for bottom left 
		EventTrigger BtmLTrig= theCube.parentCube.transform.Find("partOne").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmLEntry = new EventTrigger.Entry ();
		BtmLEntry.eventID = EventTriggerType.Drag;
		BtmLEntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partOne);
		});
		BtmLTrig.triggers.Add (BtmLEntry);
		theCube.origin = theCube.partOne.position;
		//Trigger and entry for bottom Right 
		EventTrigger BtmRTrig = theCube.parentCube.transform.Find("partTwo").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmREntry = new EventTrigger.Entry ();
		BtmREntry.eventID = EventTriggerType.Drag;
		BtmREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partTwo);
		});
		BtmRTrig.triggers.Add (BtmREntry);

		//Trigger and entry for top Left 
		EventTrigger TopLTrig = theCube.parentCube.transform.Find("partThree").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopLEntry = new EventTrigger.Entry ();
		TopLEntry.eventID = EventTriggerType.Drag;
		TopLEntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partThree);
		});
		TopLTrig.triggers.Add (TopLEntry);

		//Trigger and entry for top Right 
		EventTrigger TopRTrig = theCube.parentCube.transform.Find("partFour").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopREntry = new EventTrigger.Entry ();
		TopREntry.eventID = EventTriggerType.Drag;
		TopREntry.callback.AddListener ((data) => {
			theCube.DragObject(theCube.partFour);
		});
		TopRTrig.triggers.Add (TopREntry);

		tetrisList [key] = theCube;
		++key;

		return key;
    }
}

