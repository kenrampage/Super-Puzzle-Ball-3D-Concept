using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles all movement during the game/play scenes
public class PlayerController : MonoBehaviour
{
    //private GameInput controls;

    private float mouseRotationZ;
    private Vector2 mouseDifference;

    public Rigidbody2D playerRb;
    public GameObject wandObject;
    public GameManager gameManager;
    public GameObject cooldownOverlay;

    public float boostForce = 7;
    public float boostCooldown = .5f;

    private float boostNextFireTime = 0;
    private float boostCooldownLeftPercent;

    public static string controlScheme;
    public string controlSchemeInput;


    private void Awake()
    {
        // Sets controls variable for InputAction asset
        //controls = new GameInput();

        // Subscribes to events and directs output
        //controls.Player.Boost.performed += context => BoostPlayer();

        controlScheme = controlSchemeInput;

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
        // Set the variables for aiming at the mouse
        SetMouseDirection();

        // Rotates wand to point at mouse
        RotateWand(wandObject);

        // Calculates cooldown % remaining and manages visual effects for it
        BoostCooldownEffect();

    }


    // On collision with 2d colliders
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            collideWithTarget(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Powerup"))
        {
            collideWithPowerup(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Button"))
        {
            collideWithButton(other.gameObject);
        }

    }

    // When colliding with targets
    public void collideWithTarget(GameObject other)
    {
        Destroy(other);
        print("Destroyed " + other.name);

    }

    // when colliding with powerups
    public void collideWithPowerup(GameObject other)
    {
        Destroy(other);
        print("Destroyed " + other.name);
    }

    // when colliding with buttons
    public void collideWithButton(GameObject other)
    {
        print("Destroyed " + other.name);
    }

    // Rotates pivotObjectName to point at mouse cursor
    public void RotateWand(GameObject pivotObjectName)
    {
        pivotObjectName.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotationZ);
    }



    // Calculates direction and angle between player and mouse
    public void SetMouseDirection()
    {
        // Calculates the difference between mouse position and player position and normalizes it.
        switch (controlScheme)
        {
            case "a":
                mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                break;
            case "b":
                mouseDifference = -(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                break;
            default:
                mouseDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                break;
        }

        mouseDifference.Normalize();

        // Calculates the angle in degrees between two normalized values
        mouseRotationZ = Mathf.Atan2(mouseDifference.y, mouseDifference.x) * Mathf.Rad2Deg;

    }

    // Handles boosting player in the direction of the mouse plus setting and checking the cooldown
    public void BoostPlayer()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PAUSED)
        {
            if (boostCooldownLeftPercent == 1)
            {
                playerRb.AddForce(mouseDifference * boostForce, ForceMode2D.Impulse);
                boostNextFireTime = Time.time + boostCooldown;
            }
            else
            {
                print((boostNextFireTime - Time.time) + " Seconds Left on the boost cooldown");
            }
        }

    }

    // Calculates boost cooldown then adjusts sprite opacity and enables/disables the wand
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


}







