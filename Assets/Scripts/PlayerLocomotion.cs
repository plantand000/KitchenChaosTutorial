using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [SerializeField]
        private InputHandler inputHandler;

        [SerializeField] private float movementSpeed = 5;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float interactDistance = 2f;

        [SerializeField] private LayerMask countersLayerMask;

        private bool isWalking;
        private Vector3 lastInteractDirection;

        private void Update()
        {

            float delta = Time.deltaTime;
            HandleMovement(delta);
            HandleInteractions(delta);
        }

        #region Movement
        private void HandleInteractions(float delta)
        {
            Vector2 inputVector = inputHandler.GetMovementVectorNormalized();
            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            if (moveDirection != Vector3.zero)
            {
                lastInteractDirection = moveDirection;
            }

            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, lastInteractDirection, out raycastHit, interactDistance, countersLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    // Has ClearCounter
                    clearCounter.Interact();
                }
            }

        }

        private void HandleMovement(float delta)
        {
            Vector2 inputVector = inputHandler.GetMovementVectorNormalized();
            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

            // Collision Detection Using Raycast
            float moveDistance = movementSpeed * delta;
            float playerRadius = .7f;
            float playerHeight = 2f; // Use editor to determine
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

            if (!canMove)
            {
                // Cannont move toward the movement direction

                // Attempt only x movement
                Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

                if (canMove)
                {
                    // Can move only on the X
                    moveDirection = moveDirectionX;
                }
                else
                {
                    // Attempt only z movement
                    Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                    if (canMove)
                    {
                        // Can move only on the Z
                        moveDirection = moveDirectionZ;
                    }
                    else
                    {
                        // Cannot move in any direction
                        moveDirection = Vector3.zero;
                    }

                }

            }
            if (canMove)
            {
                transform.position += moveDirection * moveDistance;
            }

            isWalking = moveDirection != Vector3.zero;

            HandleRotation(moveDirection, delta);
        }

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
