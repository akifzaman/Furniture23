using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{
    public Item item;
    private Button button;
    public void Initialize(Item item)
    {
        this.item = item;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => UpdateSelectedItem(item));
    }
    public void UpdateSelectedItem(Item item)
    {
        ApplicationManager.instance.SelectedItem = item;    
    }
}
