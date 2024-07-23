using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InputManager : Singleton<InputManager>
{
    public LayerMask animalLayerMask;
    public bool isDisplayOn = false;
    public bool canClick = true;
    public Animal tappedAnimal;

    // Update is called once per frame
    void Update()
    {
        //GetMobileTouch();

        GetMouseTouch();

    }

    void GetMobileTouch()
    {
        if (isDisplayOn || !canClick || UIManager.Instance.Shot < 1)
            return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    
                 
                    break;

                case TouchPhase.Moved:
                    
                    break;

                case TouchPhase.Ended:

                    TouchBall(touch.position);

                    break;

                case TouchPhase.Canceled:
                    break;
            }
        }
    }

    void GetMouseTouch()
    {
        
            
        if (Input.GetMouseButtonDown(0))
        {
            //TouchBall(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchBall(Input.mousePosition);
        }
    }

    void TouchBall(Vector2 screenPos)
    {
        if (isDisplayOn)
            return;
        else if (!canClick)
            return;

        Ray rayTouch = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(rayTouch, out var hit, 100, layerMask: animalLayerMask))
        {
            if (hit.collider.TryGetComponent<Animal>(out var animal))
            {
                animal.Tap();
            }
        }
    }
    

}

