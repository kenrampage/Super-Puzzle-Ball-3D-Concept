using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Handles all movement during the game/play scenes
public class PlayerController : MonoBehaviour
{
    // public SO_ScoreKeeper scoreKeeper;
    public SO_SessionData sessionData;
    public Rigidbody2D playerRb;

    public float boostForce = 7;
    public float boostCooldown = .5f;

    private float boostNextFireTime = 0;
    private float boostCooldownLeftPercent;

    public GameObject cooldownOverlay;
    public SpriteRenderer cooldownSprite;

    private InputActions inputActions;
    public Camera gameCamera;
    private Vector2 relativeMousePos;
    private Vector2 normalizedMousePos;
    [SerializeField] LayerMask groundLayerMask;
    public Vector2 groundPosition;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void Start()
    {
        inputActions.Player.Click.performed += _ => BoostPlayer();
        SpriteRenderer cooldownSprite = cooldownOverlay.GetComponent<SpriteRenderer>();

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
        BoostCooldownEffect();
        GetDistanceToGround();
    }

    // Calculates direction and angle between player and mouse
    public void SetMouseDirection()
    {
        relativeMousePos = gameCamera.ScreenToWorldPoint(inputActions.Player.MouseAim.ReadValue<Vector2>()) - transform.position;
        normalizedMousePos = relativeMousePos.normalized;

    }

    // Handles boosting player in the direction of the mouse plus setting and checking the cooldown
    public void BoostPlayer()
    {
        if (sessionData.CurrentGameState == SO_SessionData.GameState.RUNNING)
        {
            SetMouseDirection();

            if (boostCooldownLeftPercent == 1)
            {
                playerRb.AddForce(normalizedMousePos * boostForce, ForceMode2D.Impulse);
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

        if (boostNextFireTime > Time.time)
        {
            boostCooldownLeftPercent = (boostNextFireTime - Time.time) / boostCooldown;
            cooldownSprite.color = new Color(cooldownSprite.color.r, cooldownSprite.color.g, cooldownSprite.color.b, boostCooldownLeftPercent * .4f);
        }
        else
        {
            boostCooldownLeftPercent = 1;
            cooldownSprite.color = new Color(cooldownSprite.color.r, cooldownSprite.color.g, cooldownSprite.color.b, 0);
        }
    }

    // checks the distance to the object directly below the player on the ground layer mask. Used for positioning the ground target for cinemachine
    public void GetDistanceToGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayerMask);

        if (raycastHit.collider != null)

            groundPosition = new Vector2(transform.position.x, transform.position.y - raycastHit.distance);

        Color rayColor = Color.red;
        Debug.DrawRay(transform.position, Vector2.down * raycastHit.distance, rayColor);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Target")
        {
            GameManager.Instance.RemoveTarget(other.gameObject);
            Destroy(other.gameObject);
        }
    }

}







