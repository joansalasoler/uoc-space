using System;
using UnityEngine;

namespace Game.Shared {

    /**
     *
     */
    public class OnStarCollision: MonoBehaviour {

        /**
         * Invoked when an object collides with this object.
         */
        private void OnCollisionEnter2D(Collision2D collision) {
            GameObject target = collision.collider.gameObject;
            ActorController player = target.GetComponent<ActorController>();
            PowerupController powerup = GetComponent<PowerupController>();

            if (player != null && player.isAlive) {
                powerup.Collect();
            }
        }
    }
}
