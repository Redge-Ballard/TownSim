using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingController : MonoBehaviour {

    public SpriteRenderer sprite;
    public Transform target;

	// Update is called once per frame
	void Update () {
        sprite.flipX = (target.position.x > transform.position.x);
	}
}
