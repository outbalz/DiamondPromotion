using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/ItemSO", fileName ="ItemSO_")]
public abstract class CItemDataSO : ScriptableObject
{
	#region inspector
	[SerializeField] private string _itemName;
	[SerializeField] private Sprite _itemSprite;
	#endregion

	#region property
	public string ItemName => _itemName;
	public Sprite ItemSprite => _itemSprite;
	#endregion

	public abstract void OnUse(GameObject target);
}
