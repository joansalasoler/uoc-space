using System;
using UnityEngine;

namespace Game.Shared {

    /**
     *
     */
    public class OnExtralifeCollision: MonoBehaviour {

        /**
         * Invoked when an object collides with this object.
         */
        private void OnCollisionEnter2D(Collision2D collision) {
            GameObject target = collision.collider.gameObject;
            ActorController player = target.GetComponent<ActorController>();
            ScoreController score = target.GetComponent<ScoreController>();
            PowerupController powerup = GetComponent<PowerupController>();

            if (player != null && player.isAlive) {
                score.EarnPoints(100);
                powerup.Collect();
            }
        }
    }
}
