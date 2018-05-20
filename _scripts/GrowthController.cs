using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthController : MonoBehaviour {

    public GameObject point;
    public Sprite[] stages;

    private int step;
    private SpriteRenderer s;
    private float range;
    private bool occupied = false;

    private void Start()
    {
        s = GetComponent<SpriteRenderer>();
        s.sprite = stages[step];
        StartCoroutine(RandomGrowth());
        range = s.size.x / 4;
        Vector3 test = transform.position;
        test.x += Random.Range(-range, range);
        test.y += 0.025f;
        point.transform.position = test;
        step = Random.Range(0, stages.Length - 1);
        s.sprite = stages[step];
    }

    public void ChangePoint(){
        Vector3 test = transform.position;
        test.x += Random.Range(-range, range);
        test.y += 0.025f;
        point.transform.position = test;
    }

    public void Grow(){
        step++;
        if (step == stages.Length){
            step = 0;
        }
        s.sprite = stages[step];
    }

    public void Harvest(){
        step = 0;
        s.sprite = stages[step];
        occupied = false;
    }

    public bool IsOccupied(){
        return occupied;
    }

    public bool IsReady(){
        return (step == stages.Length - 1);
    }

    public void MakeOccupied(){
        occupied = true;
    }

    IEnumerator RandomGrowth(){
        float rando = Random.Range(3, 8);
        yield return new WaitForSeconds(rando);
        if (step < stages.Length - 1)
        {
            Grow();
        }
        StartCoroutine(RandomGrowth());
    }
}
