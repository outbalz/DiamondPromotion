using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemFactory : MonoBehaviour
{

    #region inspector
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _parent;
    #endregion

    public GameObject CreateItem(Vector3 pos, Quaternion rot)
    {
        if (_itemPrefab == null)
        {
            Debug.LogWarning("Missing prefab");

            return null;
        }

        Transform p = (_parent != null) ? _parent : null;


        GameObject inst = Instantiate(_itemPrefab, pos, rot, p);

        return inst;
    }

    public GameObject CreateItem(Vector3 pos)
    {
        return CreateItem(pos, Quaternion.identity);
    }

}
