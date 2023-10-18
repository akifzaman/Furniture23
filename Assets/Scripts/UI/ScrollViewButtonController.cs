using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButtonController : MonoBehaviour
{
    public int index;
    public Item item;
    public Image ButtonIcon;
    private Button button;

    public void Initialize(Item item) //for item buttons
    {
        button = GetComponent<Button>();
        ButtonIcon.sprite = item.ItemIcon;
        this.item = item;
        button.onClick.AddListener(() => UpdateSelectedItem(item));
    }
    public void Initialize(int val, Sprite texture) //for texture buttons
    {
        index = val;
        button = GetComponent<Button>();
        ButtonIcon.sprite = texture;
        button.onClick.AddListener(() => UpdateSelectedItemTexture(val));
    }
    public void UpdateSelectedItem(Item item)
    {
        ApplicationManager.instance.SelectedItem = item;    
    }
    public void UpdateSelectedItemTexture(int index)
    {
        Debug.Log($"index: {index}");
        TouchManager.instance.currentGameObject.GetComponentInChildren<MeshRenderer>().material = 
            TouchManager.instance.currentGameObject.GetComponent<ItemController>().item.Materials[index];
    }
}
