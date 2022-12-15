using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static SwipeController Instance;
    public Vector2 startTuch;
    public Vector2 endTuch;
    public Vector2 swipeDelta;
    const float SWIPE_RANGE = 50;
    public bool touchMoved;


    public Action<bool[]> SwipeEvent;
    public Action ClickEvent;

    public enum Direction
    {
        Left, Right, Up, Down
    };

    bool[] swipe = new bool[4];

    private void Awake()
    {
        Instance = this;
    }

    private void FindDirectionOfSwipe()
    {
        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            swipe[(int)Direction.Left] = swipeDelta.x > 0;
            swipe[(int)Direction.Right] = swipeDelta.x < 0;
        }
        else
        {
            swipe[(int)Direction.Up] = swipeDelta.y < 0;
            swipe[(int)Direction.Down] = swipeDelta.y > 0;
        }
        SendSwipe();
    }

    private void SendSwipe()
    {
        if (swipe[0] || swipe[1] || swipe[2] || swipe[3] && RoadGenerator.Instance.speed > 0 && RoadGenerator.Instance.IsStarted)
        {

            SwipeEvent?.Invoke(swipe);
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        startTuch = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        swipeDelta = startTuch - eventData.position;
        Debug.Log(swipeDelta);
        FindDirectionOfSwipe();
    }

    public void StartClick()
    {
        ClickEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
