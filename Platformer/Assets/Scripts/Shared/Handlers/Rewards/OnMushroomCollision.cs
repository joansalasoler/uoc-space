using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Mushroom power-up collision.
     */
    public class OnMushroomCollision: OnPowerupCollision {

        /**
         * Activate the power-up when collected.
         */
        protected override void CollectPowerup(Collider2D collider) {
            GetColliderPlayer(collider).ActivateMushroomPowers();
            AudioService.PlayOneShot(gameObject, "Collect Mushroom");
            base.CollectPowerup(collider);
        }
    }
}
