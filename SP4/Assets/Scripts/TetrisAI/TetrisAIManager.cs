using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisAIManager {

	Hashtable stateMap = new Hashtable();
	TetrisAIBase currState ;
	TetrisAIBase nextState ;

	// Use this for initialization
	public void Start () { 

	}
	
	// Update is called once per frame
	public void Update () {
		//If currState != nextState, clean up currState and assign nextState to currState
		if (currState != nextState) {
			currState.Exit ();
			currState = nextState;
			currState.Enter ();
		}
		currState.Update();
	}

	public void AddState(TetrisAIBase newState)
	{
		//If the state already exists, ignore it 
		if (stateMap.Contains (newState.GetStateID ()))
			return;
		//If this is the first state added, assign it to currState and nextState to ensure it is not null
		else if (currState == null)
			currState = nextState = newState;
		//Then add the state to the stateMap
		stateMap.Add (newState.GetStateID (), newState);
		Debug.Log ("Manager Add");
	}

	public void SetNextState(string nextStateID)
	{
		//Searches stateMap to find nextStateID and assign to nextState
		foreach (string stateID in stateMap.Keys) {
			if (stateID == nextStateID) {
				nextState = (TetrisAIBase) stateMap [nextStateID];
			}
		}
	}
}
