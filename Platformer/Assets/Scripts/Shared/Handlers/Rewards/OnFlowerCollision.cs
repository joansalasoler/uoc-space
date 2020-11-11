using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Flower power-up collision.
     */
    public class OnFlowerCollision: OnPowerupCollision {

        /**
         * Activate the power-up when collected.
         */
        protected override void CollectPowerup(Collider2D collider) {
            GetColliderPlayer(collider).ActivateFlowerPowers();
            AudioService.PlayOneShot(gameObject, "Collect Flower");
            base.CollectPowerup(collider);
        }
    }
}
