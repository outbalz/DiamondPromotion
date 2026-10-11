using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CDragableItemController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region inspector
    [SerializeField] private Image _itemImage;
    [SerializeField] private CItemSlotController _slot;
    #endregion

    #region private var
    private CItemDataSO _itemData;
    #endregion

    public void UpdateSlotItem(CItemDataSO item)
    {
        _itemData = item;

        if (_itemData != null)
        {
            _itemImage.enabled = true;
            _itemImage.sprite = _itemData.ItemSprite;
        }

        else
        {
            _itemImage.enabled = false;
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        _itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_slot.transform);
        _itemImage.raycastTarget = true;
    }

    public void SetSlot(CItemSlotController slot)
    {
        if(CInventoryManager.AddToInventory(_itemData, slot.SlotIndex))
        {
            CInventoryManager.RemoveFromInventory(_slot.SlotIndex);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(CItemMenuController.Instance == null)
        {
            return;
        }

        CItemMenuController.Instance.SetSlotItem(_slot.SlotIndex, _itemData);
        CItemMenuController.Instance.IsOverItem = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CItemMenuController.Instance == null)
        {
            return;
        }

        //CItemMenuController.Instance.SetSlotItem(_slot.SlotIndex, null);
        CItemMenuController.Instance.IsOverItem = false;
    }
}
