using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARSubsystems;

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
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            var obj = Instantiate(ApplicationManager.instance.SelectedItem.Prefab, pose.position, pose.rotation);
            ItemPicker.instance.currentGameObject = obj;
            ItemPicker.instance.currentGameObject.GetComponent<ItemController>().initialPosition = pose.position;
            ItemPicker.instance.currentGameObject.GetComponent<ItemController>().Initialize(ApplicationManager.instance.SelectedItem);
        }
    }
    //[ContextMenu("PlaceObject")]
    //public void PlaceObjectFromMenu()
    //{
    //    if (ApplicationManager.instance.SelectedItem != null)
    //    {
    //        var obj = Instantiate(ApplicationManager.instance.SelectedItem.Prefab, new Vector3(0, 0, 0), Quaternion.identity);
    //        obj.GetComponent<ItemController>().initialPosition = new Vector3(0, 0, 0);
    //        obj.GetComponent<ItemController>().Initialize(obj);
    //    }   
    //}
}
