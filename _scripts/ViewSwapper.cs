using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSwapper : MonoBehaviour {

    public RectTransform canvas;
    public Button left;
    public Button right;
    public GameObject currentSet;
    public Image fader;
    public Text label;
    public GameObject[] sets;

    private int frame = 1;
    private int frames;
    private float width;
    private Vector3 defaultPos;

    private string[] labels;

	void Start ()
    {
        frames = 3;
        defaultPos = currentSet.transform.position;
        width = 0.628f;
        SetLabels("MainHub");
	}

    public void MoveFrame(int toMove)
    {
        StartCoroutine(MoveToFrame(toMove));
    }
	
    public void SwitchSets(){
        GameObject newSet = new GameObject();
        foreach(GameObject obj in sets){
            if (obj.name == labels[frame]){
                newSet = obj;
            }
        }
        StartCoroutine(FadeInAndOut(currentSet, newSet));
    }

    private void CheckButtons(){
        if (frame == 0)
        {
            left.interactable = false;
        }
        else
        {
            left.interactable = true;
        }
        if (frame == frames - 1)
        {
            right.interactable = false;
        }
        else
        {
            right.interactable = true;
        }
    }

    IEnumerator MoveToFrame(int toMove)
    {
        frame -= toMove;
        //print("Current frame changed to " + frame);
        CheckButtons();
        UpdateLabel();

        float elapsedTime = 0;
        float time = 0.5f;
        Vector3 start = currentSet.transform.position;

        while (elapsedTime < time)
        {
            Vector3 current = start;
            float x = Mathf.Lerp(start.x, start.x + (width * toMove), (elapsedTime / time));
            current.x = x;
            currentSet.transform.position = current;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Vector3 final = start;
        float xFinal = start.x + (width * toMove);
        final.x = xFinal;
        currentSet.transform.position = final;
    }

    IEnumerator FadeInAndOut(GameObject oldSet, GameObject newSet){
        float time = 1;
        float elapsedTime = 0;
        float startAlpha = 0;
        float currentAlpha = 0;
        fader.transform.gameObject.SetActive(true);

        while (elapsedTime < time)
        {
            Color c = fader.color;
            currentAlpha = Mathf.Lerp(startAlpha, 1, (elapsedTime / time));
            c.a = currentAlpha;
            fader.color = c;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        oldSet.SetActive(false);
        newSet.SetActive(true);
        currentSet = newSet;
        currentSet.transform.position = defaultPos;
        frame = 1;
        SetLabels(currentSet.name);
        CheckButtons();

        startAlpha = 1;
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            Color c = fader.color;
            currentAlpha = Mathf.Lerp(startAlpha, 0, (elapsedTime / time));
            c.a = currentAlpha;
            fader.color = c;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Color final = Color.black;
        final.a = 0;
        fader.color = final;
        fader.transform.gameObject.SetActive(false);
    }

    private void UpdateLabel()
    {
        label.text = "To " + labels[frame];
        if(labels[frame] == ""){
            label.text = "";
        }
        label.transform.parent.GetComponent<Button>().interactable = !(labels[frame] == "");
    }

    private void SetLabels(string location){
        print(location);
        switch(location){
            case "MainHub":
                string[] a = { "Country", "", "Town" };
                labels = a;
                break;
            case "Country":
                string[] b = { "Woods", "Farm", "Quarry" };
                labels = b;
                break;
            case "Town":
                string[] c = { "Guilds", "Market", "Smithies" };
                labels = c;
                break;
            default:
                print("This isn't on the list : " + location);
                string[] z = { "", "MainHub", "" };
                labels = z;
                break;
        }
        UpdateLabel();
    }
}
