using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemSO", fileName ="ItemSO_")]
public class CItemDataSO : ScriptableObject
{
	#region inspector
	[SerializeField] private string _itemName;
	[SerializeField] private Sprite _itemSprite;
	#endregion

	#region property
	public string ItemName => _itemName;
	public Sprite ItemSprite => _itemSprite;
	#endregion

}
