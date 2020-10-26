using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Generic power-up collision.
     */
    public class OnPowerupCollision: OnRewardCollision {

        /**
         * Invoked when an object collides with this object.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            if (IsAliveColliderActor(collision.collider)) {
                RewardColliderPlayer(collision.collider);
                EmitEarnedPoints(earnedPoints, collision.GetContact(0).point);
                CollectPowerup();
            }
        }


        /**
         * Collects the power-up if a power up controller is attached
         * to the object. Otherwise, does nothing.
         */
        protected void CollectPowerup() {
            PowerupController powerup = GetComponent<PowerupController>();
            if (powerup != null) powerup.Collect();
        }
    }
}
