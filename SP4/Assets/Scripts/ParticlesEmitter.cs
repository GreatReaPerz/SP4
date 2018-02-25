using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEmitter : MonoBehaviour {

    [SerializeField]
    private ParticleSystem theParticles;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A pressed");
            theParticles = Instantiate(theParticles, transform.position, Quaternion.identity);
            theParticles.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            theParticles.Play();
        }
        if(theParticles.isPlaying)
        {
            Debug.Log("Playing");
        }
    }
}
