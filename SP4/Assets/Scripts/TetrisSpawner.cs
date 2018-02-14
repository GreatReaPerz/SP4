using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TetrisSpawner : MonoBehaviour{

	[SerializeField]
	GameObject[] TetrisTypes;


	public TetrisCube[] tetrisList = new TetrisCube[3]; 
	float timer = 0;
	int numSpawned = 0;

  	// Use this for initialization
	void Start () {
		
		numSpawned = Spawn4x4Cube (numSpawned);
		numSpawned = Spawn4x4Cube (numSpawned);
	}
	
	// Update is called once per frame
	void Update () {
		//Used to test spawning (dont spawn ontop of each other, will kinda bug out
		/*timer += Time.deltaTime;
		if (timer > 3) {
			Spawn4x4Cube ();
			timer = 0;
		}*/

	}
    
	int Spawn4x4Cube (int key)
	{
		GameObject newCube = Instantiate (TetrisTypes [0], transform.position, Quaternion.identity);
		newCube.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, true);
		TetrisCube theCube = new TetrisCube ();

		tetrisList [key] = theCube;
		++key;
		//Set up the 4 cubes based on newCube's child
		theCube.setTheCubes (newCube.transform.Find ("BtmLeft").GetComponent<Rigidbody2D> (), newCube.transform.Find ("BtmRight").GetComponent<Rigidbody2D> (), newCube.transform.Find ("TopLeft").GetComponent<Rigidbody2D> (), newCube.transform.Find ("TopRight").GetComponent<Rigidbody2D> ());

		//Could use raycast instead 
		//Also cause the only thing changing is the movement function, could try to make a switch instead
		//Trigger and entry for bottom left 
		EventTrigger BtmLTrig= newCube.transform.Find("BtmLeft").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmLEntry = new EventTrigger.Entry ();
		BtmLEntry.eventID = EventTriggerType.Drag;
		BtmLEntry.callback.AddListener ((data) => {
			theCube.DragbtmLeft ();
		});
		BtmLTrig.triggers.Add (BtmLEntry);

		//Trigger and entry for bottom Right 
		EventTrigger BtmRTrig = newCube.transform.Find("BtmRight").GetComponent<EventTrigger> ();
		EventTrigger.Entry BtmREntry = new EventTrigger.Entry ();
		BtmREntry.eventID = EventTriggerType.Drag;
		BtmREntry.callback.AddListener ((data) => {
			theCube.DragbtmRight ();
		});
		BtmRTrig.triggers.Add (BtmREntry);

		//Trigger and entry for top Left 
		EventTrigger TopLTrig = newCube.transform.Find("TopLeft").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopLEntry = new EventTrigger.Entry ();
		TopLEntry.eventID = EventTriggerType.Drag;
		TopLEntry.callback.AddListener ((data) => {
			theCube.DragtopLeft ();
		});
		TopLTrig.triggers.Add (TopLEntry);

		//Trigger and entry for top Right 
		EventTrigger TopRTrig = newCube.transform.Find("TopRight").GetComponent<EventTrigger> ();
		EventTrigger.Entry TopREntry = new EventTrigger.Entry ();
		TopREntry.eventID = EventTriggerType.Drag;
		TopREntry.callback.AddListener ((data) => {
			theCube.DragtopRight ();
		});
		TopRTrig.triggers.Add (TopREntry);
		return key;
	}
}
