using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/HealingItemDataSO", fileName = "HealingItemDataSO_")]
public class CHealingItemDataSO : CItemDataSO
{

    #region inspector
    [SerializeField] private float _healAmount;
    #endregion

    public override void OnUse(GameObject target)
    {
        CHPController targetHP = target.GetComponent<CHPController>();

        if (targetHP == null)
        {
            return;
        }

        targetHP.TakeDamage(-_healAmount);
    }
}
