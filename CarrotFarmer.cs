using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotFarmer : MonoBehaviour, IActionController
{
    public GrowthController[] plants;

    public Animator anim;
    public GameObject dropoff;
    public GameObject carrot;

    private GrowthController growth;
    private Pathfinding.AIDestinationSetter dest;
    private Pathfinding.IAstarAI ai;
    private bool goingToGather = true;
    private int progress = 0;
    private Transform original;
    private int attempts = 0;

    public void Start()
    {
        dest = GetComponent<Pathfinding.AIDestinationSetter>();
        ai = GetComponent<Pathfinding.IAstarAI>();
        int rando = Random.Range(0, plants.Length - 1);
        while (plants[rando].IsOccupied()) rando = Random.Range(0, plants.Length - 1);
        growth = plants[rando];
        growth.MakeOccupied();
        original = growth.point.transform;
        for (int i = 0; i < plants.Length; i++){
            if (plants[i].IsReady()){
                original = plants[i].point.transform;
                growth = plants[i];
                growth.MakeOccupied();
            }
        }
        dest.target = original;
        dest.enabled = true;
    }

    public void Arrived()
    {
        if (goingToGather){
            Gather();
        } else {
            Finish();
        }
    }

    public void Finish()
    {
        if (dest.target == dropoff.transform)
        {
            for (int i = 0; i < plants.Length; i++)
            {
                if (plants[i].IsReady() && !plants[i].IsOccupied())
                {
                    original = plants[i].point.transform;
                    growth = plants[i];
                    growth.MakeOccupied();
                }
            }
            dest.target = original;
            goingToGather = true;
            anim.SetBool("carrying", false);
            Vector3 pos = transform.position;
            pos.x += Random.Range(-0.03f, 0.03f);
            pos.y += Random.Range(-0.03f, 0.03f);
            Instantiate(carrot, pos, Quaternion.Euler(0,0,Random.Range(0,360)));
        }
        else
        {
            dest.target = dropoff.transform;
            anim.SetBool("carrying", true);
        }
        ai.SearchPath();
    }

    public void Gather()
    {
        anim.SetBool("harvesting", true);
        StartCoroutine(MoveAround());
    }

    IEnumerator MoveAround(){
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        growth.ChangePoint();
        ai.SearchPath();
        progress++;
        anim.SetBool("harvesting", false);
        if (progress >= 1 && growth.IsReady()){
            progress = 0;
            growth.Harvest();
            goingToGather = false;
        }
    }
}
