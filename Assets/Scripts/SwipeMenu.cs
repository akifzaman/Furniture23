using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public Scrollbar scrollBar;
    public Transform Parent;
    public int numberOfItems;
   
    public float scrollBarValue;
    public float valuePerItem;
    [SerializeField] private int CurrentIndex = 0;
    [SerializeField] private int PreviousIndex = 0;

    private void Start()
    {
        numberOfItems = Parent.transform.childCount;
        valuePerItem = (1f / numberOfItems);
    }
    private void Update()
    {
        scrollBarValue = Mathf.Clamp(scrollBar.GetComponent<Scrollbar>().value, 0f, 1f);
        CurrentIndex = Mathf.Clamp(Mathf.CeilToInt(scrollBarValue / valuePerItem), 0 , numberOfItems - 1);
        if (CurrentIndex == PreviousIndex) return;
        Parent.GetChild(numberOfItems/2).transform.DOScaleY(1.3f, 0.1f);
        Parent.GetChild(PreviousIndex).transform.DOScaleY(1f, 0.1f);
        if(CurrentIndex < PreviousIndex) Parent.GetChild(0).SetAsLastSibling();
        else Parent.GetChild(numberOfItems - 1).SetAsFirstSibling();
        PreviousIndex = CurrentIndex;
    }

}
