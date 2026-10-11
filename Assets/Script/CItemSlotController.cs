using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemSlotController : MonoBehaviour
{
	#region inspector
	[SerializeField] private CDragableItemController _item;
	#endregion

	#region private var
	private int _slotIndex;
    #endregion

    #region property
    public int SlotIndex => _slotIndex;
    #endregion

    public void InitializeSlot(int index, CItemDataSO item = null)
    {
        _slotIndex = index;
        UpdateSlot(item);
    }

    public void UpdateSlot(CItemDataSO item)
    {
        _item.UpdateSlotItem(item);
    }
}
