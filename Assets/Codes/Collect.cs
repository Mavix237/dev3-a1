using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchMove : MonoBehaviour
{
    void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    void Update(){
        if(Touch.activeTouches.Count > 0)
        {
            Touch myTouch = Touch.activeTouches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(myTouch.screenPosition);
                //out - value getting returned
                if (Physics.Raycast(raycast, out RaycastHit hit))
                {
                    // if(hit.collider.CompareTag("Collectible"))
                    // {
                    //     hit.collider.GetComponent<TapAction>().Tapped();
                    // }
                    if(hit.collider.TryGetComponent<TapAction>(out TapAction tapAction))
                    {
                        tapAction.Tapped();
                    }
                }
            }
        }
    }
}


