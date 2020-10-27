using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Collisions with fireballs.
     */
    public class OnFireballCollision: OnPowerupCollision {
        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);

            if (IsAliveColliderActor(collision.collider)) {
                GetColliderActor(collision.collider).Damage();
            }
        }
    }
}
