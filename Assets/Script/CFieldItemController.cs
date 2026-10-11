using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFieldItemController : MonoBehaviour
{
	#region inspector
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private CItemDataSO _dataSO;
    #endregion

    private void Awake()
    {
        if(_spriteRenderer == null)
        {
            if(!TryGetComponent<SpriteRenderer>(out _spriteRenderer))
            {
                Debug.LogWarning("Missing SpriteRenderer");
                enabled = false;
                return;
            }
        }

        InitializeItem();
    }

    public void InitializeItem()
    {
        if(_dataSO == null)
        {
            Debug.LogWarning("Missing itemDataSO");
            enabled = false;
            return;
        }

        _spriteRenderer.sprite = _dataSO.ItemSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CInventoryManager.AddToInventory(_dataSO))
        {
            Destroy(gameObject);
        }

    }
}
