using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CFieldEffectController : MonoBehaviour
{

    #region inspector
    [SerializeField] private float _healAmount;
    #endregion


    #region private var
    private Dictionary<CHPController, Coroutine> _effectMap = new Dictionary<CHPController, Coroutine>();
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        CHPController chara = collision.GetComponent<CHPController>();

        if (chara == null)
        {
            return;
        }

        if (!_effectMap.ContainsKey(chara))
        {
            Coroutine healroutine = StartCoroutine(CoHealroutine(chara));
            _effectMap.Add(chara, healroutine);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        CHPController chara = collision.GetComponent<CHPController>();

        if (chara == null)
        {
            return;
        }

        if (_effectMap.ContainsKey(chara))
        {
            StopCoroutine(_effectMap[chara]);
            _effectMap.Remove(chara);
        }
    }


    private IEnumerator CoHealroutine(CHPController chara)
    {
        while (chara.HP > 0) 
        {
            chara.TakeDamage(-_healAmount, true);
            yield return new WaitForSeconds(1);
        }

        if (_effectMap.ContainsKey(chara))
        {
            _effectMap.Remove(chara);
        }
        yield break;
    }
}
