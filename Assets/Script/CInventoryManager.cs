using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryManager : MonoBehaviour
{
	#region inspector
	[SerializeField] private Transform _itemSlotLayout;
	[SerializeField] private CItemSlotController _itemSlotPrefab;
	[SerializeField] private int _inventorySize = 20;
	#endregion

	#region private var
	private CItemDataSO[] _inventory;
	private CItemSlotController[] _inventorySlots;
	private static CInventoryManager _instance;
	#endregion

	#region property
	public static CInventoryManager Instance => _instance;
    #endregion

    private void Awake()
    {
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}

        else if(_instance == null)
		{
			_instance = this;
		}

		_inventory = new CItemDataSO[_inventorySize];
		_inventorySlots = new CItemSlotController[_inventorySize];

		InitializeInventory();
    }

    private void OnDestroy()
    {
        if(_instance == this)
		{
			_instance = null;
		}
    }

    private void InitializeInventory()
    {
		if(_itemSlotPrefab == null)
		{
			Debug.LogWarning("Missing _itemSlotPrefab");
			return;
		}


		for (int i = 0; i < _inventorySize; i++)
		{
			CItemSlotController slot = Instantiate(_itemSlotPrefab, _itemSlotLayout);

			if (slot == null)
			{
				continue;
			}

			_inventorySlots[i] = slot;
			slot.InitializeSlot(i);
        }
    }

    public static bool AddToInventory(CItemDataSO item)
    {
        if (_instance == null)
        {
            Debug.LogWarning("Missing CInventoryManager Instance");
            return false;
        }

        CInventoryManager inst = _instance;

		for (int i = 0; i < inst._inventory.Length; i++)
		{
			if (inst._inventory[i] == null)
			{
				AddToInventory(item, i);
				return true;
			}
		}

		Debug.LogWarning("Inventory is full");
		return false;
    }

    public static bool AddToInventory(CItemDataSO item, int index)
	{
		if (_instance == null)
		{
			Debug.LogWarning("Missing CInventoryManager Instance");
			return false;
		}

        CInventoryManager inst = _instance;

		if (inst._inventory[index] != null)
		{
			Debug.Log("inventory slot is already occupied");
			return false;
		}

		inst._inventory[index] = item;
		inst._inventorySlots[index].UpdateSlot(item);
		return true;
    }

	public static void RemoveFromInventory(int index)
	{
		if (_instance == null)
		{
			Debug.LogWarning("Missing CInventoryManager Instance");
			return;
		}

		CInventoryManager inst = _instance;

        if (inst._inventory[index] == null)
        {
            Debug.LogWarning("inventory slot is already empty");
            return;
        }

        inst._inventory[index] = null;
        inst._inventorySlots[index].UpdateSlot(null);
    }

}
