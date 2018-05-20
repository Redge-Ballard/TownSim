using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestController : MonoBehaviour {

    public float speed;
    public GameObject destA;
    public GameObject destinB;

    private Animator anim;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(GoToPoint(destA, destinB));
	}

    private void CheckDirection(Transform dest){
        sprite.flipX = (dest.localPosition.x > transform.localPosition.x);
    }

    IEnumerator GoToPoint(GameObject dest, GameObject destB){
        float time = speed;
        float elapsedTime = 0;
        float startX = transform.localPosition.x;
        float startY = transform.localPosition.y;
        CheckDirection(dest.transform);
        while (elapsedTime < time)
        {
            float x = startX;
            float y = startY;
            x = Mathf.Lerp(startX, dest.transform.localPosition.x, (elapsedTime / time));
            y = Mathf.Lerp(startY, dest.transform.localPosition.y, (elapsedTime / time));
            Vector3 newPos = transform.localPosition;
            newPos.x = x;
            newPos.y = y;
            transform.localPosition = newPos;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        anim.speed = 0;
        yield return new WaitForSeconds(1);
        anim.speed = 1;
        CheckDirection(destB.transform);

        elapsedTime = 0;
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;

        while (elapsedTime < time)
        {
            float x = startX;
            float y = startY;
            x = Mathf.Lerp(startX, destB.transform.localPosition.x, (elapsedTime / time));
            y = Mathf.Lerp(startY, destB.transform.localPosition.y, (elapsedTime / time));
            Vector3 newPos = transform.localPosition;
            newPos.x = x;
            newPos.y = y;
            transform.localPosition = newPos;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        anim.speed = 0;
        yield return new WaitForSeconds(1);
        anim.speed = 1;

        StartCoroutine(GoToPoint(dest, destB));
    }
}
