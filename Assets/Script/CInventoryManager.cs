using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventoryManager : MonoBehaviour
{
	#region inspector
	[SerializeField] private Transform _itemSlotLayout;
	[SerializeField] private GameObject _itemSlotPrefab;
	[SerializeField] private int _inventorySize = 20;
	#endregion

	#region private var
	private CItemDataSO[] _inventory;
	private GameObject[] _inventorySlots;
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
		}

        else if(_instance == null)
		{
			_instance = this;
		}

		_inventory = new CItemDataSO[_inventorySize];
		_inventorySlots = new GameObject[_inventorySize];

    }

	public static void AddToInventory(CItemDataSO item)
	{
		if (_instance == null)
		{
			Debug.LogWarning("Missing CInventoryManager Instance");
			return;
		}
		//Do something
	}

	public static void RemoveFromInventory(CItemDataSO item)
	{
		if (_instance == null)
		{
			Debug.LogWarning("Missing CInventoryManager Instance");
			return;
		}
		//Do something
	}

	
}
