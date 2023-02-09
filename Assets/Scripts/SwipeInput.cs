using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
    public const float MAX_SWIPE_TIME = 0.5f;
    // Factor of the screen width that we consider a swipe
    // 0.17 works well for portrait mode 16:9 phone
    public const float MIN_SWIPE_DISTANCE = 0.17f;

    public static bool SwipedRight = false;
    public static bool SwipedLeft = false;
    public static bool SwipedUp = false;
    public static bool SwipedDown = false;

    public bool DebugWithArrowKeys = true;

    private bool _isFingerDown = false;
    private Vector2 _startPosition;
    private float _startTime;

    public void Update()
    {
        SwipedRight = false;
        SwipedLeft = false;
        SwipedUp = false;
        SwipedDown = false;

        // For PC
        if (!_isFingerDown && Input.GetMouseButtonDown(0))
        {
            _isFingerDown = true;
            _startPosition = new Vector2(Input.mousePosition.x / (float)Screen.width, Input.mousePosition.y / (float)Screen.width);
            _startTime = Time.time;
        }

        if (_isFingerDown && Input.GetMouseButtonUp(0))
        {
            _isFingerDown = false;

            if (Time.time - _startTime > MAX_SWIPE_TIME) // Press too long
            {
                return;
            }

            Vector2 endPosition = new Vector2(Input.mousePosition.x / (float)Screen.width, Input.mousePosition.y / (float)Screen.width);
            Vector2 swipeDirection = new Vector2(endPosition.x - _startPosition.x, endPosition.y - _startPosition.y);

            if (swipeDirection.magnitude < MIN_SWIPE_DISTANCE) // Swipe too short
            {
                return;
            }

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                // Horizontal swipe
                if (swipeDirection.x > 0.0f)
                {
                    SwipedRight = true;
                }
                else
                {
                    SwipedLeft = true;
                }
            }
            else
            {
                // Vertical swipe
                if (swipeDirection.y > 0.0f)
                {
                    SwipedUp = true;
                }
                else
                {
                    SwipedDown = true;
                }
            }
        }

        // // For mobile
        // if (Input.touches.Length > 0)
        // {
        //     Touch touch = Input.GetTouch(0);

        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         _startPosition = new Vector2(touch.position.x / (float)Screen.width, touch.position.y / (float)Screen.width);
        //         _startTime = Time.time;
        //     }

        //     if (touch.phase == TouchPhase.Ended)
        //     {
        //         if (Time.time - _startTime > MAX_SWIPE_TIME) // Press too long
        //         {
        //             return;
        //         }

        //         Vector2 endPosition = new Vector2(touch.position.x / (float)Screen.width, touch.position.y / (float)Screen.width);
        //         Vector2 swipeDirection = new Vector2(endPosition.x - _startPosition.x, endPosition.y - _startPosition.y);

        //         if (swipeDirection.magnitude < MIN_SWIPE_DISTANCE) // Swipe too short
        //         {
        //             return;
        //         }

        //         if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        //         {
        //             // Horizontal swipe
        //             if (swipeDirection.x > 0.0f)
        //             {
        //                 SwipedRight = true;
        //             }
        //             else
        //             {
        //                 SwipedLeft = true;
        //             }
        //         }
        //         else
        //         {
        //             // Vertical swipe
        //             if (swipeDirection.y > 0.0f)
        //             {
        //                 SwipedUp = true;
        //             }
        //             else
        //             {
        //                 SwipedDown = true;
        //             }
        //         }
        //     }
        // }

        if (DebugWithArrowKeys)
        {
            SwipedDown = SwipedDown || Input.GetKeyDown(KeyCode.DownArrow);
            SwipedUp = SwipedUp || Input.GetKeyDown(KeyCode.UpArrow);
            SwipedRight = SwipedRight || Input.GetKeyDown(KeyCode.RightArrow);
            SwipedLeft = SwipedLeft || Input.GetKeyDown(KeyCode.LeftArrow);
        }
    }
}