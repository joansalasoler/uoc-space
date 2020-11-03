using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Kills actors when they touch the ground limit.
     */
    public class OnGroundLimitCollision: MonoBehaviour {

        /**
         * Invoked when an object collides with this object.
         */
        private void OnCollisionEnter2D(Collision2D collision) {
            GameObject target = collision.collider.gameObject;
            ActorController actor = target.GetComponent<ActorController>();

            if (actor != null && actor.isAlive) {
                actor.Kill();
            } else {
                Destroy(target);
            }
        }
    }
}
