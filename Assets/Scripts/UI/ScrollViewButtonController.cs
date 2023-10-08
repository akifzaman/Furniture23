using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButtonController : MonoBehaviour
{
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
    public void Initialize(Sprite texture) //for texture buttons
    {
        button = GetComponent<Button>();
        ButtonIcon.sprite = texture;
        button.onClick.AddListener(() => UpdateSelectedItemTexture(texture));
    }
    public void UpdateSelectedItem(Item item)
    {
        ApplicationManager.instance.SelectedItem = item;    
    }
    public void UpdateSelectedItemTexture(Sprite texture)
    {

    }
}
