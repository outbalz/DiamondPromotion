using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemDatabaseSO", fileName = "ItemDatabaseSO_")]
public class CItemDatabaseSO : ScriptableObject
{
	#region inspector
	[SerializeField] private List<CItemDataSO> _itemList;
    #endregion

    #region property
    public CItemDataSO this[int index] => _itemList[index];
    #endregion

    public Dictionary<CItemDataSO, int> GetItemListMap()
    {
        Dictionary<CItemDataSO, int> itemListMap = new Dictionary<CItemDataSO, int>();

        for (int i = 0; i < _itemList.Count; i++)
        {
            itemListMap.Add(_itemList[i], i);
        }

        return itemListMap;
    }
}
