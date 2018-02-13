using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour {

	[SerializeField]
	private Rigidbody2D tetrisPiece;
	bool stopDrag = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (stopDrag)
			Debug.Log ("YES");
	}

	public void DragObject()
	{
		tetrisPiece.MovePosition (Input.mousePosition);
	}
}
