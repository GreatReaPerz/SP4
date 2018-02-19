using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

class TetrisMove: TetrisAIBase{

	//
	TetrisCube theObject = new TetrisCube();
	Vector2 moveSpeed = new Vector2();
	public TetrisMove(string _stateID,TetrisCube _object)
	{
		stateID = _stateID;
		theObject = _object;
		moveSpeed.Set (0, 100);
	}

	// Use this for initialization
	public override void Enter () {

	}

	// Update is called once per frame
	public override void Update () {
		theObject.btmLeft.MovePosition(theObject.btmLeft.position + moveSpeed * Time.deltaTime);
		theObject.btmRight.MovePosition(theObject.btmRight.position + moveSpeed * Time.deltaTime);
		theObject.topLeft.MovePosition(theObject.topLeft.position + moveSpeed * Time.deltaTime);
		theObject.topRight.MovePosition(theObject.topRight.position + moveSpeed * Time.deltaTime);

	}
	public override void Exit(){

	}
}
