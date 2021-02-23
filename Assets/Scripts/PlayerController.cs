using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles all movement during the game/play scenes
public class PlayerController : MonoBehaviour
{
    //private GameInput controls;

    private float mouseRotationZ;
    private Vector2 mouseDifference;

    public Rigidbody2D playerRb;
    //public GameObject wandObject;
    //public GameObject cooldownOverlay;

    public float boostForce = 7;
    public float boostCooldown = .5f;

    //private float boostNextFireTime = 0;
    //private float boostCooldownLeftPercent;

    //public static string controlScheme;
    //public string controlSchemeInput;

    private InputActions inputActions;


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
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


    private void FixedUpdate()
    {
        // Set the variables for aiming at the mouse
        SetMouseDirection();

    }


    // Rotates pivotObjectName to point at mouse cursor
    public void RotateWand(GameObject pivotObjectName)
    {
        pivotObjectName.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotationZ);
    }



    // Calculates direction and angle between player and mouse
    public void SetMouseDirection()
    {
        mouseDifference = Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue()) - transform.position;
        mouseDifference.Normalize();

    }

    // Handles boosting player in the direction of the mouse plus setting and checking the cooldown
    public void BoostPlayer()
    {
        print("Boost!");

        playerRb.AddForce(mouseDifference * boostForce, ForceMode2D.Impulse);

    }

    // Calculates boost cooldown then adjusts sprite opacity and enables/disables the wand

    /*
    public void BoostCooldownEffect()
    {
        SpriteRenderer spriteRenderer = cooldownOverlay.GetComponent<SpriteRenderer>();

        if (boostNextFireTime > Time.time)
        {
            boostCooldownLeftPercent = (boostNextFireTime - Time.time) / boostCooldown;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, boostCooldownLeftPercent * .4f);
            //GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            boostCooldownLeftPercent = 1;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            //GameObject.Find("Wand").GetComponent<SpriteRenderer>().enabled = true;
        }
    }
*/

}







