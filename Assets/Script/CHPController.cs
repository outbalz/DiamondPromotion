using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPController : MonoBehaviour
{
    #region inspector
    [SerializeField] private float _hp;
    [SerializeField] private float _maxhp;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private CCharacterStateController _stateController;
    #endregion

    #region private var
    private bool _deathBinded = false;
    #endregion

    #region property
    public float HP => _hp;
    public float MaxHP => _maxhp;
    #endregion

    private void Awake()
    {
        if(_hpBar != null)
        {
            _hpBar.maxValue = _maxhp;
            _hpBar.SetValueWithoutNotify(_hp);
        }
    }

    private void Start()
    {
        if (_stateController != null && _deathBinded == false)
        {
            _stateController.OnDeathEvent += OnDeath;
            _deathBinded = true;
        }
    }

    private void OnEnable()
    {
        if (_stateController != null && _deathBinded == false)
        {
            _stateController.OnDeathEvent += OnDeath;
            _deathBinded = true;
        }
    }

    private void OnDisable()
    {
        if (_stateController != null && _deathBinded == true)
        {
            _stateController.OnDeathEvent -= OnDeath;
            _deathBinded = false;
        }
    }


    private void OnDeath()
    {
        _hpBar.gameObject.SetActive(false);


    }

    public void TakeDamage(float damage, bool ignoreInvincible = false)
    {
        if(_stateController.State == CCharacterStateController.ECharacterState.Death)
        {
            return;
        }

        if (_stateController != null)
        {
            if (_stateController.IsInvincible && !ignoreInvincible)
            {
                return;
            }

            else
            {
                if (damage > 0)
                {
                    _stateController.TakeDamege();
                }
            }
        }

        _hp -= damage;

        if( _hp <= 0)
        {
            _stateController.ChangeState(CCharacterStateController.ECharacterState.Death);
            return;
        }

        if(_hp > _maxhp)
        {
            _hp = _maxhp;
        }

        if (_hpBar != null)
        {
            _hpBar.SetValueWithoutNotify(_hp);
        }
    }


}
