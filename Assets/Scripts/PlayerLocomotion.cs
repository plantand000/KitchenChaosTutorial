using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [SerializeField]
        private InputHandler inputHandler;

        [SerializeField]
        private float movementSpeed = 5;
        [SerializeField]
        private float rotationSpeed = 10;

        private bool isWalking;

        private void Update()
        {

            float delta = Time.deltaTime;

            Vector2 inputVector = inputHandler.GetMovementVectorNormalized();
            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            transform.position += moveDirection * movementSpeed * Time.deltaTime; // Create indepedence from framerate

            isWalking = moveDirection != Vector3.zero;

            HandleRotation(moveDirection, delta);

        }

        #region PlayerRotation
        private void HandleRotation(Vector3 moveDir ,float delta)
        {
            float rs = rotationSpeed;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, delta * rs);
        }
        #endregion

        public bool IsWalking()
        {
            return isWalking;
        }

    }
}
