using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public RectTransform ItemButtonsPanel;
    public RectTransform TextureButtonsPanel;
    public Transform ItemButtonsContainer;
    public Transform TextureButtonsContainer;
    public ScrollViewButtonController ItemButtonPrefab;
    public ScrollViewButtonController TextureButtonPrefab;
    public Vector2 DisplayPosition;
    public Vector2 HidePosition;
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
    public void Start()
    {
        InstantiateItemButtons();
        DisplayPosition = ItemButtonsPanel.anchoredPosition;
        HidePosition = TextureButtonsPanel.anchoredPosition;
    }

    public void InstantiateItemButtons()
    {
        foreach (var item in ApplicationManager.instance.Items)
        {
            var btn = Instantiate(ItemButtonPrefab, ItemButtonsContainer);
            btn.Initialize(item);
        }
    }
    public void InstantiateTextureButtons(Item item)
    {
        CleanPanel(TextureButtonsContainer);
        foreach (var texture in item.AvailableTextures)
        {
            var btn = Instantiate(TextureButtonPrefab, TextureButtonsContainer);
            btn.Initialize(texture);
        }
    }
    public void CleanPanel(Transform panel)
    {
        foreach (Transform element in panel)
        {
            Destroy(element.gameObject);
        }
    }
    [ContextMenu("ShowTexturePanel")]
    public void ShowTexturePanel()
    {
        ItemButtonsPanel.anchoredPosition = HidePosition;
        TextureButtonsPanel.gameObject.SetActive(true);
        TextureButtonsPanel.DOAnchorPosY(DisplayPosition.y, 1f).OnComplete(() =>
        {
            ItemButtonsPanel.gameObject.SetActive(false);
        });
        
    }
    [ContextMenu("ShowItemPanel")]
    public void ShowItemPanel()
    {
        TextureButtonsPanel.anchoredPosition = HidePosition;
        ItemButtonsPanel.gameObject.SetActive(true);
        ItemButtonsPanel.DOAnchorPosY(DisplayPosition.y, 1f).OnComplete(() =>
        {
            TextureButtonsPanel.gameObject.SetActive(false);
        });
    }
}
