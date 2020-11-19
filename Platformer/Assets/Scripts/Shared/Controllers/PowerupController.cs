using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the power-ups.
     */
    public class PowerupController : MonoBehaviour {

        /** Wether the power-up is active */
        public bool wasCollected = false;


        /**
         * Collects this power-up.
         */
        public virtual void Collect() {
            if (wasCollected == false) {
                wasCollected = true;
                HideGameObject();
                DisableColliders();
                Destroy(gameObject, 2.0f);
            }
        }


        /**
         * Disable all the colliders of this power-up.
         */
        public virtual void DisableColliders() {
            var colliders = GetComponentsInChildren<Collider2D>();
            Array.ForEach(colliders, c => c.enabled = false);
        }


        /**
         * Hides the game object by disabling its renderer.
         */
        public virtual void HideGameObject() {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
