using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Extralife mushroom collision.
     */
    public class OnExtralifeCollision: OnPowerupCollision {

        /**
         * Activate the power-up when collected.
         */
        protected override void CollectPowerup(Collider2D collider) {
            AudioService.PlayOneShot(gameObject, "Collect Extralife");
            base.CollectPowerup(collider);
        }
    }
}
