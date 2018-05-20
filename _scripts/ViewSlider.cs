using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSlider : MonoBehaviour {

    public GameObject map;
    public GameObject bg;
    public int swipeSensitivity = 50;
    public int screenPixels = 126;

    private Vector3 startPos;
    private float startX;
    private float currentX;
    private float spriteWidth;
    private float screenWidth;
    private int maxFrames;
    private float screenPix;
    private int currentFrame = 4;

    private void Start()
    {
        spriteWidth = map.GetComponent<SpriteRenderer>().size.x;
        screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        screenPix = screenPixels / 100f;
        maxFrames = (int)(spriteWidth / screenPix);
    }

    // Update is called once per frame
    void Update () {
        
        if (Input.GetMouseButtonDown(0))
        {
            startX = Input.mousePosition.x;
            startPos = bg.transform.position;
        }
        if (Input.GetMouseButton(0))
        {
            currentX = (startX - Input.mousePosition.x) / (Screen.width * 0.75f);
            Vector3 newPos = bg.transform.position;
            newPos.x = startPos.x + currentX;

            if (bg.transform.position.x < (spriteWidth) / 2 - screenWidth && Mathf.Sign(currentX) > 0)
            {
                bg.transform.position = newPos;
                if (bg.transform.position.x > (spriteWidth) / 2 - screenWidth)
                {
                    newPos.x = (spriteWidth) / 2 - screenWidth;
                    bg.transform.position = newPos;
                }
            }
            if (bg.transform.position.x > (-spriteWidth) / 2 + screenWidth && Mathf.Sign(currentX) < 0)
            {
                bg.transform.position = newPos;
                if (bg.transform.position.x < (-spriteWidth) / 2 + screenWidth)
                {
                    newPos.x = (-spriteWidth) / 2 + screenWidth;
                    bg.transform.position = newPos;
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Input.touchCount > 0){
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                if (touchDeltaPosition.x > swipeSensitivity)
                {
                    StartCoroutine(SnapToFrame(1));
                }
                else if (touchDeltaPosition.x < -swipeSensitivity)
                {
                    StartCoroutine(SnapToFrame(-1));
                } else {
                    StartCoroutine(SnapToFrame(0));
                }
            }
            else
            {
                StartCoroutine(SnapToFrame(0));
            }
        }
	}

    IEnumerator SnapToFrame(int direction){
        float endPos = -bg.transform.position.x + spriteWidth / 2; // Middle of screen
        int frame = Mathf.RoundToInt((endPos - (screenPix/2)) / screenPix);
        if (direction != 0)
        {
            frame = currentFrame + direction;
        }
        if (frame < 0) frame = 0;
        if (frame > maxFrames - 1) frame = maxFrames - 1;
        float elapsedTime = 0;
        float time = 0.25f;
        Vector3 start = bg.transform.position;

        while (elapsedTime < time)
        {
            Vector3 current = start;
            float x = Mathf.Lerp(start.x, (spriteWidth / 2) - ((frame * screenPix) + (screenPix/2)), (elapsedTime / time));
            current.x = x;
            bg.transform.position = current;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        currentFrame = frame;
        Vector3 final = bg.transform.position;
        final.x = (spriteWidth / 2) - ((frame * screenPix) + (screenPix/2));
        bg.transform.position = final;
    }
}
