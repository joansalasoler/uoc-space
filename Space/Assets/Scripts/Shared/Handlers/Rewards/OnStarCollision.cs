using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Star power-up collision.
     */
    public class OnStarCollision: OnPowerupCollision {

        /**
         * Activate the power-up when collected.
         */
        protected override void CollectPowerup(Collider2D collider) {
            GetColliderPlayer(collider).ActivateStarPowers();
            AudioService.PlayOneShot(gameObject, "Collect Star");
            base.CollectPowerup(collider);
        }
    }
}
