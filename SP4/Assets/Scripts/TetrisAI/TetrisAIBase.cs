using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TetrisAIBase{


	public TetrisAIBase(){}
	public string stateID;
	public string GetStateID(){
		return stateID;
	} 
	abstract public void Enter ();
	abstract public void Update ();
	abstract public void Exit ();
}
