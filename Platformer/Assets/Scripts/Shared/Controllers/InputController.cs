using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Moves an actor according to the user input.
     */
    public class InputController : MonoBehaviour {

        /** Target velocity of the player */
        [HideInInspector] public Vector2 velocity;

        /** Fireball template object */
        [SerializeField] private GameObject fireball = null;

        /** Maximum horizontal speed on the ground */
        public float groundSpeed = 350.0f;

        /** Maximum horizontal speed on the air */
        public float airSpeed = 150.0f;

        /** Horizontal speed multiplier when running */
        public float runFactor = 2.0f;

        /** Vertical speed multiplier for canceled jumps */
        public float fallFactor = .35f;

        /** Magnitude of the applied force to jump */
        public float jumpForce = 1450.0f;

        /** Target horizontal move direction */
        public float direction;

        /** If fireballs are enabled */
        public bool fireEnabled = false;

        /** Wether the player is currently jumping */
        public bool isJumping = false;

        /** Wether the player is running or walking */
        public bool isRunning = false;

        /** Wether the player is touching the ground */
        public bool isGrounded = false;

        /** Wether the player is moving to the left */
        public bool isFlipped = false;

        /** True when the player requested a jump */
        private bool isJumpRequested = false;

        /** True when the player canceled a jump */
        private bool isJumpCanceled = false;

        /** Timestamp when the last fireball was loaded */
        private float fireLoadTime = 0.0f;

        /** Rigidbody of the actor */
        private Rigidbody2D actorRigidbody;

        /** Collider filter for the ground detector */
        private ContactFilter2D groundFilter;

        /** Hit placeholder for the ground detector */
        private RaycastHit2D[] groundHits;


        /**
         * Initialize the player when it is started.
         */
        private void Start() {
            groundHits = new RaycastHit2D[16];
            groundFilter = GetGroundContactFilter2D();
        }


        /**
         * Obtain references to the required components.
         */
        private void OnEnable() {
            actorRigidbody = GetComponent<Rigidbody2D>();
            velocity = actorRigidbody.velocity;
        }


        /**
         * Update the actor state on each frame.
         *
         * Notice that, in order to not miss any jump requests, they are
         * cleared only after running them on FixedUpdate.
         */
        private void Update() {
            direction = Input.GetAxis("Horizontal");
            isRunning = direction != .0f && Input.GetButton("Fire1");
            isFlipped = direction == .0f ? isFlipped : direction < .0f;
            isJumpRequested |= Input.GetButtonDown("Jump");
            isJumpCanceled |= Input.GetButtonUp("Jump");

            // Throw a fireball only if they were enabled and if the
            // player hasn't started to run instead. This is because
            // we use the same button for running an shooting.

            if (fireEnabled && Input.GetButtonDown("Fire1")) {
                fireLoadTime = Time.time;
            } else if (fireEnabled && Input.GetButtonUp("Fire1")) {
                if (Time.time - fireLoadTime < 0.5f) {
                    ThrowFireball();
                }
            }
        }


        /**
         * Handle the user input and move the player accordingly.
         *
         * For the horizontal move sets the velocity on the X axis. To jump,
         * adds a force on the vertical axis which is canceled by reducing
         * the vertical velocity when the player releases the jump button.
         */
        private void FixedUpdate() {
            bool wasGrounded = isGrounded;
            float speed = GetHorizontalTargetSpeed();

            isGrounded = CheckGroundCollisions();
            velocity = actorRigidbody.velocity;
            velocity.x = speed * direction * Time.fixedDeltaTime;

            if (isGrounded && !wasGrounded) {
                isJumping = false;
            } else if (isJumpCanceled && isJumping) {
                velocity.y = fallFactor * velocity.y;
            } else if (isJumpRequested && isGrounded && !isJumping) {
                actorRigidbody.AddForce(jumpForce * Vector2.up);
                isJumping = true;
            }

            actorRigidbody.velocity = velocity;
            isJumpRequested = false;
            isJumpCanceled = false;
        }


        /**
         * Throws a fireball from the player's position.
         */
        public void ThrowFireball() {
            GameObject ball = Instantiate(fireball);
            Rigidbody2D body = ball.GetComponent<Rigidbody2D>();
            Destroy(ball, 2.5f);

            body.velocity = Vector2.right * actorRigidbody.velocity;
            ball.transform.position = actorRigidbody.transform.position;
            body.AddForce((isFlipped ? -60.0f : 60.0f) * transform.right);
            body.AddTorque(200.0f);
        }


        /**
         * Obtains the player's target speed for the horizontal direction.
         * This takes into account if the player is grounded or running.
         */
        private float GetHorizontalTargetSpeed() {
            float multiplier = isRunning ? runFactor : 1.0f;
            float speed = multiplier * (isGrounded ? groundSpeed : airSpeed);

            return speed;
        }


        /**
         * Checks if our player is on the ground by raycasting its rigidbody
         * a predefined distance to the down direction.
         */
        private bool CheckGroundCollisions() {
            return actorRigidbody.Cast(
                Vector2.down, groundFilter, groundHits, .02f) > 0;
        }


        /**
         * Creates a contact filter for the ground. Uses the collisions
         * matrix for the current game object as a filter.
         */
        private ContactFilter2D GetGroundContactFilter2D() {
            int layer = gameObject.layer;
            int layerMask = Physics2D.GetLayerCollisionMask(layer);
            ContactFilter2D groundFilter = new ContactFilter2D();

            groundFilter.SetLayerMask(layerMask);
            groundFilter.useLayerMask = true;
            groundFilter.useTriggers = false;

            return groundFilter;
        }
    }
}
