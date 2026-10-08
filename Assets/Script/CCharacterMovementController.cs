using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterMovementController : MonoBehaviour
{
    #region inspector
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CCharacterStateController _state;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _runSpeed = 2.0f;
    #endregion

    private void Awake()
    {
        if (_rb == null)
        {
            if(!TryGetComponent<Rigidbody2D>(out _rb))
            {
                Debug.LogWarning("Missing Rigidbody2D");
                enabled = false;
                return;
            }
        }

        if(_state == null)
        {
            if(!TryGetComponent<CCharacterStateController>(out _state))
            {
                Debug.LogWarning("Missing CCharacterStateController");
                enabled = false;
                return;
            }
        }

    }

    public void MoveChararcter(Vector2 moveVector, bool isRunning)
    {
        if(_state.State == CCharacterStateController.ECharacterState.Attack || _state.State == CCharacterStateController.ECharacterState.Death)
        {
            _rb.velocity = Vector2.zero;
            return;
        }


        if(moveVector.sqrMagnitude < 0.0001f)
        {
            _rb.velocity = Vector2.zero;

            if (_state.State != CCharacterStateController.ECharacterState.Attack)
            {
                _state.ChangeState(CCharacterStateController.ECharacterState.Idle);
            }

            return;
        }

        Vector2.ClampMagnitude(moveVector, 1.0f);

        moveVector *= isRunning ? _runSpeed : _movementSpeed ;

        _rb.velocity = moveVector;

        _state.OnMove(moveVector, isRunning);
    }


}
