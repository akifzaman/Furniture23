using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public static TouchManager instance;
    public GameObject currentGameObject;
    
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    #endregion
    private void Update()
    {
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Furniture") && currentGameObject == null)
                    {
                        currentGameObject = raycastHit.collider.gameObject;
                        currentGameObject.GetComponent<ItemController>().OnItemSelect();
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && currentGameObject != null)
            {
                currentGameObject.GetComponent<ItemController>().MoveItem(Input.GetTouch(0));
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                currentGameObject.GetComponent<ItemController>().ScaleItem(touch1, touch2);
            }
            else if (touch1.phase == TouchPhase.Stationary && touch2.phase == TouchPhase.Moved)
            {
                currentGameObject.GetComponent<ItemController>().RotateItem(touch1, touch2);  
            }
            else currentGameObject.GetComponent<ItemController>().isModified = false;
        }
    }
}
