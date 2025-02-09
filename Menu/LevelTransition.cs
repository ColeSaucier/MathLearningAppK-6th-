using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public Animator transition;

    // Minimum distance for a swipe to be registered
    public float minSwipeDistance = 50f;

    // Maximum time allowed for a swipe
    public float maxSwipeTime = 1f;

    private Vector2 startPosition;
    private float startTime;
    private bool swipeDetected = false;

    public HomescreenSceneManager HomescreenSceneManager;


    void Update()
    {
        // Check if there are touches on the screen
        if (Input.touchCount > 0)
        {
            //Debug.LogError("Swipe Detected0");
            Touch touch = Input.GetTouch(0);

            // Detect the start of a touch
            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
                startTime = Time.time;
                swipeDetected = false;
            }
            // Detect the end of a touch
            else if (touch.phase == TouchPhase.Ended)
            {
                if (!swipeDetected)
                {
                    DetectSwipe(touch);
                }
            }
        }
    }

    void DetectSwipe(Touch touch)
    {
        // Calculate the distance of the swipe
        Vector2 endPosition = touch.position;
        Vector2 swipeDelta = endPosition - startPosition;
        //Debug.LogError("Swipe Detected1");
        // Check if the swipe is long enough
        if (swipeDelta.magnitude > minSwipeDistance)
        {
            //Debug.LogError("Swipe Detected2");
            // Check if the swipe was quick enough
            if (Time.time - startTime < maxSwipeTime)
            {
                //Debug.LogError("Swipe Detected3");
                TransitionToNextLevel();
                /*
                // Determine the direction of the swipe
                float x = swipeDelta.x;
                float y = swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    // Horizontal swipe
                    if (x > 0)
                        Debug.Log("Right Swipe");
                    else
                        Debug.Log("Left Swipe");
                }
                else
                {
                    // Vertical swipe
                    if (y > 0)
                        Debug.Log("Up Swipe");
                    else
                        Debug.Log("Down Swipe");
                }
                */
            }
        }
    }

    public void TransitionToNextLevel()
    {
        StartCoroutine(LoadLevel(HomescreenSceneManager.currentLevel));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(0.1f);

        //load scene
        SceneManager.LoadScene(levelIndex);
    }

    public void TransitionToFirstLevel(string nextScene)
    {
        SceneManager.LoadScene($"{nextScene}");
    }
}
