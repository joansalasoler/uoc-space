using UnityEngine;
using System.Collections.Generic;

namespace Game.Shared {

    /**
     * Handle collisions for a simple brick.
     */
    public class OnBrickCollision : OnRewardCollision {

        /**
         * Invoked when the object is enabled.
         */
        private void Start() {
            var trigger = GetComponentInChildren<OnBrickKeyTrigger>();
            trigger.brickKeyTriggerEnter = OnBrickKeyTriggerEnter;
        }


        /**
         * Destroy the game object that triggered the collision.
         */
        private void DestroyBrick() {
            Destroy(gameObject);
        }


        /**
         * Invoked when an object collides with this object.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            // Only collisions with the brick key are rewarded and if
            // the player's mushroom power-up is active
        }


        /**
         * On collision activates the reward and destroys the brick.
         */
        protected void OnBrickKeyTriggerEnter(Collider2D collider) {
            PlayerController player = GetColliderPlayer(collider);

            if (player != null && player.mushroomActive) {
                var collisionPoint = Vector3.up + collider.bounds.center;
                EmitEarnedPoints(earnedPoints, collisionPoint);
                RewardColliderPlayer(collider);
                DestroyBrick();
            }
        }
    }
}
