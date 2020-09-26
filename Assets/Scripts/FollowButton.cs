using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowButton : MonoBehaviour, IDragHandler
{
    public GameObject gameControllerObject;
    public Canvas canvas;

    GameController gameController;
    int mode;
    Vector3 ogPosition;

    void Awake()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        //if (gameController)
        //{
        //    Debug.Log("YES");
        //}
        mode = gameController.getMode;

        ogPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {   
        mode = gameController.getMode;
        if (mode == 2)
        {
            GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
