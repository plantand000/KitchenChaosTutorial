using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class InputHandler : MonoBehaviour
    {

        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
        }

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
            inputVector.Normalize();

            return inputVector;
        }
    }
}
