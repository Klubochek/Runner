using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance;
    public Vector2 startTuch;
    public Vector2 endTuch;
    public Vector2 swipeDelta;
    const float SWIPE_RANGE = 50;
    public bool touchMoved;


    public Action<bool[]> SwipeEvent;
    public Action<Vector2> ClickEvent;

    public enum Direction
    {
        Left, Right, Up, Down
    };

    bool[] swipe = new bool[4];


    Vector2 TouchPostion() => (Vector2)Input.mousePosition;
    bool TouchBegan() => Input.GetMouseButtonDown(0);
    bool TouchEnded() => Input.GetMouseButtonUp(0);
    bool GetTouch() => Input.GetMouseButton(0);
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (TouchBegan())
        {
            startTuch = TouchPostion();
            touchMoved = true;
            
        }
        else if (TouchEnded())
        {
            SendSwipe();
            touchMoved = false;
        }
        swipeDelta = Vector3.zero;
        if (touchMoved && GetTouch())
        {
            swipeDelta = TouchPostion() - startTuch;
        }
        if (swipeDelta.magnitude > SWIPE_RANGE)
        {
            FindDirectionOfSwipe();
        }
    }

    private void FindDirectionOfSwipe()
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            swipe[(int)Direction.Left] = swipeDelta.x < 0;
            swipe[(int)Direction.Right] = swipeDelta.x > 0;
        }
        else
        {
            swipe[(int)Direction.Up] = swipeDelta.y > 0;
            swipe[(int)Direction.Down] = swipeDelta.y < 0;
        }
        SendSwipe();
    }

    private void SendSwipe()
    {
        if (swipe[0] || swipe[1] || swipe[2] || swipe[3] && RoadGenerator.Instance.speed > 0 && RoadGenerator.Instance.IsStarted)
        {
            
            SwipeEvent?.Invoke(swipe);
        }
        else if (RoadGenerator.Instance.speed ==0&&!RoadGenerator.Instance.IsStarted)
        {
            
            ClickEvent?.Invoke(TouchPostion());
            RoadGenerator.Instance.StartLevel();
        }
        Reset();
    }
    
    private void Reset()
    {
        startTuch = swipeDelta = Vector3.zero;
        touchMoved = false;
        for (int i = 0; i < 4; i++)
        {
            swipe[i] = false;
        }
    }

}
