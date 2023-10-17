using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += OnFingerDown;
    }
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
    }

    private void OnFingerDown(EnhancedTouch.Finger finger)
    {     
        if (finger.index != 0) return;

        // Check for AR plane interaction
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon) && TouchManager.instance.currentGameObject == null)
        {
            // Ensure the AR plane is not obstructed by a UI element
            if (!TouchManager.instance.IsPointerOverUIElement(finger.currentTouch.screenPosition))
            {
                Pose pose = hits[0].pose;
                var obj = Instantiate(ApplicationManager.instance.SelectedItem.Prefab, pose.position, pose.rotation);
                TouchManager.instance.currentGameObject = obj;
                TouchManager.instance.currentGameObject.GetComponent<ItemController>().initialPosition = pose.position;
                TouchManager.instance.currentGameObject.GetComponent<ItemController>().Initialize(ApplicationManager.instance.SelectedItem);
            }
        }    
    }

    
}
