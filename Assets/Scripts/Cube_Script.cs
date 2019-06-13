using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Script : MonoBehaviour {

    public bool alive = false;
    public bool next_Alive = false;
	
	void FixedUpdate () {
        GetComponent<Renderer>().enabled = alive;
	}
}