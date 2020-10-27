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
            base.CollectPowerup(collider);
            GetColliderPlayer(collider).ActivateFlowerPowers();
        }
    }
}
