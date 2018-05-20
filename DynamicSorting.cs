using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSorting : MonoBehaviour {

    public int offset = 0;
    public bool checkParent = false;
    private SpriteRenderer spr;
    private SpriteRenderer parentSpr;

	// Use this for initialization
	void Start () {
        spr = GetComponent<SpriteRenderer>();
        if (checkParent){
            parentSpr = transform.parent.GetComponent<SpriteRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (checkParent)
        {
            spr.sortingOrder = parentSpr.sortingOrder + 3;
        }
        else
        {
            spr.sortingOrder = (int)(transform.position.y * -100) + offset;
        }
	}
}
