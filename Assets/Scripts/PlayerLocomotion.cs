using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AP
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 5;
        [SerializeField]
        private float rotationSpeed = 10;

        private void Start()
        {

        }

        private void Update()
        {

            // Legacy Input System
            Vector2 inputVector = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                inputVector.y = +1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputVector.x = -1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputVector.y = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputVector.x = +1;
            }


            inputVector.Normalize();

            Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
            transform.position += moveDirection * movementSpeed * Time.deltaTime; // Create indepedence from framerate

            float rs = rotationSpeed;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rs);

            #region DepricatedRotation
            // Make the transform to rotate the character using previous tutorial
            //Vector3 targetDirection = Vector3.zero;
            //targetDirection = moveDirection.normalized;
            //targetDirection.y = 0;

            //if (targetDirection == Vector3.zero)
            //{
            //    targetDirection = transform.forward;
            //}

            //float rs = rotationSpeed;
            //Quaternion tr = Quaternion.LookRotation(targetDirection);
            //Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rs * Time.deltaTime);

            //transform.rotation = targetRotation;
            #endregion

        }
    }
}
