using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    public Material[] materials;

	// Use this for initialization
	void Start () {
        int rando = Random.Range(0, materials.Length);
        GetComponent<Renderer>().material = materials[rando];
	}
	
}
