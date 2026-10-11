using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CItemMenuController : MonoBehaviour
{
    #region inspector
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _rootTransform;
    [SerializeField] private Text _nameText;
    [SerializeField] private GameObject _player;
    #endregion

    #region private var
    private static CItemMenuController _instance;
    private int _slotIndex;
    private CItemDataSO _item;
    private bool _isOverItem = false;
    #endregion

    #region property
    public static CItemMenuController Instance => _instance;
    public bool IsOverItem { get { return _isOverItem; } set { _isOverItem = value; } }
    #endregion

    private void Awake()
    {
        if(_rectTransform == null || _rootTransform == null)
        {
            Debug.LogWarning("Missing Transform");
            Destroy(gameObject);
            return;
        }

        if(_nameText == null)
        {
            Debug.LogWarning("Missing text");
            Destroy(gameObject);
            return;
        }

        if (_player == null)
        {
            Debug.LogWarning("Missing player");
            Destroy(gameObject);
            return;
        }

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else if (_instance == null)
        {
            _instance = this;
        }


        gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    public void SetSlotItem(int slotIndex, CItemDataSO item)
    {
        _slotIndex = slotIndex;
        _item = item;
    }

    public void OpenItemMenu()
    {
        if(_item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        _nameText.text = _item.ItemName;

        Vector2 mousePos = Input.mousePosition;

        float pivotX = -0.05f;
        float pivotY = 1;

        if ((Screen.width - mousePos.x) / _rootTransform.localScale.x < _rectTransform.sizeDelta.x)
        {
            pivotX = 1;
        }

        if (mousePos.y / _rootTransform.localScale.y < _rectTransform.sizeDelta.y)
        {
            pivotY = 0;
        }

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePos;
    }

    public void CloseItemMenu()
    {
        gameObject.SetActive(false);
        _item = null;
    }

    public void OnButtonClick()
    {
        if(_item == null)
        {
            gameObject.SetActive(false);
            return;
        }

        _item.OnUse(_player);
        CInventoryManager.RemoveFromInventory(_slotIndex);

    }

}
