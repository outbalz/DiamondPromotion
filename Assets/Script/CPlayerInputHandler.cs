using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerInputHandler : MonoBehaviour
{
	#region inspector
	[SerializeField] private CCharacterMovementController _movementController;
	[SerializeField] private CCharacterStateController _stateController;
    #endregion

    private void Awake()
    {
        if( _movementController == null)
        {
            if(!TryGetComponent<CCharacterMovementController>(out _movementController))
            {
                Debug.LogWarning("Missing Movement Controller");
                enabled = false;
                return;
            }
        }
    }


    private void FixedUpdate()
    {
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        _movementController.MoveChararcter(moveVector, isRunning);
    }

}
