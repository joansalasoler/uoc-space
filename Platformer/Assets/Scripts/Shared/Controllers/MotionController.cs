using UnityEngine;

namespace Game.Shared {

    /**
     * Makes a rigidbody move constantly on the horizontal axis and
     * change its direction when it collides with something.
     */
    public class MotionController : MonoBehaviour {

        /** Maximum target speed of the object */
        public float maxSpeed = 3.0f;

        /** Maximum target speed of the object */
        public float minSpeed = .5f;

        /** Direction of the move in the range [-1.0, 1.0] */
        public float direction = -1.0f;

        /** Minimum angle of a collision from the side */
        public float minSideAngle = 70.0f;

        /** Wether the object is looking to the left */
        private bool isFlipped = false;

        /** Adds an impulse force to the object */
        public Vector2 impulse = Vector2.zero;

        /** Reference to the object's rigidbody */
        private Rigidbody2D objectRigidbody;

        /** Sprite renderer of the object */
        private SpriteRenderer objectRenderer;

        /** Target velocity of the object */
        private Vector2 velocity;


        /**
         * Initialize the movement when enabled.
         */
        private void OnEnable() {
            objectRigidbody = GetComponent<Rigidbody2D>();
            objectRenderer = GetComponent<SpriteRenderer>();
            velocity = maxSpeed * direction * Vector2.right;
            objectRigidbody.velocity = velocity;
        }


        /**
         * Update the player state and animations on each frame.
         *
         * Notice that in order to not miss any jump requests they are
         * cleared only on FixedUpdate.
         */
        private void Update() {
            isFlipped = direction == .0f ? isFlipped : direction < .0f;
            objectRenderer.flipX = isFlipped;
        }


        /**
         * Ensures that the object is constantly moving on the correct
         * direction. On each side collision, or if the object is stopped,
         * the direction is reversed and the velocity set to maximum.
         */
        private void FixedUpdate() {
            float speed = Mathf.Abs(objectRigidbody.velocity.x);

            if (impulse != Vector2.zero) {
                objectRigidbody.AddForce(impulse, ForceMode2D.Impulse);
                impulse = Vector2.zero;
            }

            if (speed < minSpeed) {
                direction = -direction;
                velocity = objectRigidbody.velocity;
                velocity.x = maxSpeed * direction;
                objectRigidbody.velocity = velocity;
            } else if (speed * direction < .0f) {
                velocity = objectRigidbody.velocity;
                velocity.x = maxSpeed * direction;
                objectRigidbody.velocity = velocity;
            }
        }


        /**
         * Toggles the direction if a side collision occurred.
         */
        private void OnCollisionEnter2D(Collision2D collision) {
            if (IsSideCollision(collision)) {
                direction = IsLeftCollision(collision) ? -1.0f : 1.0f;
            }
        }


        /**
         * Checks if a collision happened on the left direction.
         */
        private bool IsLeftCollision(Collision2D collision) {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 center = collision.collider.bounds.center;

            return contact.point.x < center.x;
        }


        /**
         * Checks if a collision happened on the side of the object.
         */
        private bool IsSideCollision(Collision2D collision) {
            ContactPoint2D contact = collision.GetContact(0);
            float angle = Vector2.Angle(contact.normal, Vector2.up);
            bool isSide = angle >= minSideAngle;

            return angle >= minSideAngle;
        }
    }
}
