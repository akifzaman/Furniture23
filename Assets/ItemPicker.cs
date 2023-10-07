using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public static ItemPicker instance;

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
        if (Input.touchCount > 0)
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
                    else if (raycastHit.collider.CompareTag("Furniture") && currentGameObject != null)
                    {
                        currentGameObject.GetComponent<ItemController>().OnItemDeselect();
                        currentGameObject = null;
                    }
                }
            }
        }
    }
}

//handle the exception testcase where you want to select another gameobject from the world space when you already have a gameObject selected and hovering