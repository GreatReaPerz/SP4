using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TetrisSpawner : MonoBehaviour {

	[SerializeField]
	GameObject[] TetrisList;

  	// Use this for initialization
	void Start () {
		GameObject newCube = Instantiate (TetrisList [0], transform.position, Quaternion.identity);
		newCube.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform, true);
		/*TetrisCube cube = new TetrisCube ();
		cube.AddTetrisCube (1, newCube);
		newCube =  (GameObject)cube.GetTetrisCube (1);
		EventTrigger trigger = newCube.transform.Find("BtmLeft").GetComponent<EventTrigger> ();
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener ((data) => {
			cube.DragbtmLeft ();
		});
		trigger.triggers.Add (entry);*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
