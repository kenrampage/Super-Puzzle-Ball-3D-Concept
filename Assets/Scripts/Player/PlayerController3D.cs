﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles all movement during the game/play scenes
public class PlayerController3D : MonoBehaviour
{
    // public SO_ScoreKeeper scoreKeeper;
    public SessionDataSO sessionData;
    public Rigidbody playerRb;

    public float boostForce = 7;
    // public float boostCooldown = .5f;

    // private float boostNextFireTime = 0;
    // private float boostCooldownLeftPercent;
    public bool boostOn = false;
    private InputActions inputActions;
    public Camera gameCamera;
    private Vector3 boostDirection;

    [SerializeField] LayerMask groundLayerMask;
    public Vector3 groundPosition;

    // public bool inWindArea = false;
    // public GameObject windArea;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Start()
    {
        inputActions.Player.Click.performed += _ => BoostPlayer();

    }

    private void OnEnable()
    {
        gameCamera = Camera.main;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        // BoostCooldownEffect();
        GetDistanceToGround();
    }



    // Calculates direction and angle between player and mouse
    public void SetBoostDirection()
    {

        Vector3 mousePosition = gameCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 playerPosition = gameCamera.WorldToViewportPoint(transform.position);

        boostDirection = (new Vector3(mousePosition.x - playerPosition.x, mousePosition.y - playerPosition.y, transform.position.z)).normalized;

    }

    // Handles boosting player in the direction of the mouse
    public void BoostPlayer()
    {
        if (sessionData.CurrentGameState == SessionDataSO.GameState.RUNNING)
        {
            SetBoostDirection();

            if (boostOn)
            {
                playerRb.AddForce(boostDirection * boostForce, ForceMode.Impulse);
                boostOn = false;

            }
            else
            {
                print("Boost is OFF");
            }
        }


    }

    // Turns the boost back on after collision with anything that's not tagged NoBoostReset
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "NoBoostReset")
        {
            boostOn = true;
        }

    }

    // Failsafe to turn boost back on if ball gets stuck
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != "NoBoostReset")
        {
            boostOn = true;
        }
    }


    // checks the distance to the object directly below the player on the ground layer mask. Used for positioning the ground target for cinemachine
    public void GetDistanceToGround()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, Mathf.Infinity, groundLayerMask))

            groundPosition = new Vector3(transform.position.x, transform.position.y - raycastHit.distance, transform.position.z);

        // Color rayColor = Color.red;
        // Debug.DrawRay(transform.position, Vector2.down * raycastHit.distance, rayColor);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag == "WindArea")
    //     {
    //         windArea = other.gameObject;
    //         inWindArea = true;
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "WindArea")
    //     {
    //         windArea = null;
    //         inWindArea = false;
    //     }
    // }

}







