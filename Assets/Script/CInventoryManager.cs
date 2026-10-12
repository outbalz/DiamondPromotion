using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CItemSlotData
{
	#region inspctor
	[SerializeField] private CItemDataSO _itemData;
	[SerializeField] private int _amount;
	#endregion

	#region property
	public CItemDataSO Item => _itemData;
	public int Amount => _amount;
	#endregion

	public bool AddToSlot(CItemDataSO item, int amount)
	{
		if (item != _itemData)
		{
			return false;
		}

		_amount += amount;
		return true;
	}

	public void RemoveAllFormSlot()
	{
		_amount = 0;
	}

	public bool RemoveFormSlot(int amount)
	{
		if(_amount - amount < 0)
		{
			return false;
		}

		_amount -= amount;
		return true;
	}

	public CItemSlotData(CItemDataSO item, int amount)
	{
		_itemData = item;
		_amount = amount;
	}
}

public class CInventoryManager : MonoBehaviour
{
	#region inspector
	[SerializeField] private Transform _itemSlotLayout;
	[SerializeField] private CItemSlotController _itemSlotPrefab;
	[SerializeField] private int _inventorySize = 20;
	#endregion

	#region private var
	private CItemSlotData[] _inventory;
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

		_inventory = new CItemSlotData[_inventorySize];
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

    public static bool AddToInventory(CItemDataSO item, int amount)
    {
        if (_instance == null)
        {
            Debug.LogWarning("Missing CInventoryManager Instance");
            return false;
        }

        CInventoryManager inst = _instance;

		for (int i = 0; i < inst._inventory.Length; i++)
		{
			if (inst._inventory[i] == null || inst._inventory[i].Item == item)
			{
				AddToInventory(item, i, amount);
				return true;
			}
		}

		Debug.LogWarning("Inventory is full");
		return false;
    }

    public static bool AddToInventory(CItemDataSO item, int index, int amount)
	{
		if (_instance == null)
		{
			Debug.LogWarning("Missing CInventoryManager Instance");
			return false;
		}

        CInventoryManager inst = _instance;

		/*
		if (inst._inventory[index] != null)
		{
			Debug.Log("inventory slot is already occupied");
			return false;
		}

		inst._inventory[index] = item;
		*/

		if(inst._inventory[index] == null)
		{
			inst._inventory[index] = new CItemSlotData(item, amount);
			inst._inventorySlots[index].UpdateSlot(item);
			return true;
		}

		if(inst._inventory[index].Item != item && inst._inventory[index].Item != null)
		{
			Debug.Log("inventory slot is already occupied");
			return false;
		}

		if(inst._inventory[index].AddToSlot(item, amount))
		{
			inst._inventorySlots[index].UpdateSlot(item);
			return true;
		}
		
		return false;
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

	public static CItemSlotData GetSlotData(int index)
	{
		if(_instance == null)
		{
			return null;
		}

		return _instance._inventory[index];
	}

	public static int GetItemAmount(int index)
	{
		if (_instance == null)
		{
			return 0;
		}

		return _instance._inventory[index].Amount;
	}


	// 책임 분리 원칙을 위해 아래 내용은 분리하는게 좋을 수도 있겠지만 편의상 일단 여기 남겨둡니다.
    private void Update()
    {
		CItemMenuController itemMenu = CItemMenuController.Instance;


		if(itemMenu == null)
		{
			return;
		}

        if (Input.GetMouseButtonUp(0))
        {
			if (!itemMenu.IsOverItem)
			{
				itemMenu.CloseItemMenu();
			}
        }

        if (Input.GetMouseButtonDown(1))
        {
			if (itemMenu.IsOverItem)
			{
				itemMenu.OpenItemMenu();
			}
        }

    }

}
