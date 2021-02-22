using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPB
{

    public class EnvironmentController : MonoBehaviour
    {
        //private GameInput controls;

        private Vector2 inputVector;

        [HideInInspector] public GameObject levelMovableObject;
        public GameManager gameManager;

        public float rotationSpeed;

        private void Awake()
        {
            // Sets controls variable for InputAction asset
            //controls = new GameInput();

            // Subscribes to events and directs output
            //controls.Player.Move.performed += context => SetVectorInput(context.ReadValue<Vector2>());
            //controls.Player.Move.canceled += context => SetVectorInput(new Vector2(0, 0));
        }


        private void OnEnable()
        {
            // Enables input controls
            //controls.Enable();
        }

        private void OnDisable()
        {
            // Disables input controls
            //controls.Disable();

        }



        private void FixedUpdate()
        {
            // Moves/rotates environment based on Vector2 input
            //MoveEnvironment(GameManager.level);
        }

        // Moves/rotates environmentPivotObject based on Vector2 input 
        //public void MoveEnvironment(Vector2 input)
        //{
        //    levelMovableObject.transform.Rotate(Vector3.back, input.x * rotationSpeed);
        //}

        // Sets inputVector variable from input action events

        public void SetVectorInput(in Vector2 context)
        {
            inputVector = context;
        }


        // Moves/rotates environmentPivotObject based on Vector2 input 
        public void MoveEnvironment(int level)
        {
            switch (level)
            {
                case 1:
                    //rotationSpeed = .5f;
                    //levelMovableObject.transform.Rotate(Vector3.back, rotationSpeed);
                    //levelMovableObject.transform.Rotate(Vector3.back, inputVector.x * rotationSpeed);
                    //levelMovableObject.transform.position = new Vector2(Mathf.PingPong(Time.time * rotationSpeed, 5), levelMovableObject.transform.position.y);

                    break;

                case 2:
                    //rotationSpeed = .5f;

                    //if (levelMovableObject.transform.rotation.z > -10 && levelMovableObject.transform.rotation.z < 10)
                    //{
                        //rotationSpeed = 2;
                    //}
                    //else if (levelMovableObject.transform.rotation.z > 10 && levelMovableObject.transform.rotation.z < -10)
                    //{
                        //rotationSpeed = -2;
                    //}

                    //levelMovableObject.transform.Rotate(Vector3.back, -rotationSpeed);

                    break;
                case 3:
                    levelMovableObject.transform.Rotate(Vector3.back, inputVector.x * rotationSpeed);
                    //print("Level :" + level);
                    break;
                case 4:
                    levelMovableObject.transform.Rotate(Vector3.back, inputVector.x * rotationSpeed);
                    //print("Level :" + level);
                    break;
                case 5:
                    levelMovableObject.transform.Rotate(Vector3.back, inputVector.x * rotationSpeed);
                    //print("Level :" + level);
                    break;
                default:
                    print("Default");
                    break;
            }

        }

    }
}
