using UnityEngine;
using System.Collections.Generic;

namespace Game.Shared {

    /**
     * Broken brick fragment physics animation.
     */
    public class OnBrickFragmentEnable : MonoBehaviour {

        /** Adds an impulse force to the object */
        public Vector2 impulse = Vector2.zero;

        /** Adds a torque force to the object */
        public float torque = 0.0f;

        /** Initial scale of the fragment */
        private float scale = 1.0f;


        /**
         * Add a force an impulse to the brick fragment.
         */
        private void OnEnable() {
            Rigidbody2D body = GetComponent<Rigidbody2D>();
            body.AddForce(impulse, ForceMode2D.Impulse);
            body.AddTorque(torque, ForceMode2D.Force);
            Destroy(gameObject, 5.0f);
        }


        /**
         * Scale the frament over time.
         */
        private void Update() {
            scale += 0.5f * Time.deltaTime;
            transform.localScale = new Vector2(scale, scale);
        }
    }
}
