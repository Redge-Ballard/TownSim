using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StaticSorting : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -100);
	}
	
}
