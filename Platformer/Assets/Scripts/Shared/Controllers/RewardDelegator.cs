using UnityEngine;
using System;

namespace Game.Shared {

    /**
     * Delegates collisions for a reward trigger.
     */
    public class RewardDelegator : MonoBehaviour {

        /** Reward collision delegate */
        public Action rewardCollision;

        /** Target collider for the reward */
        public Collider2D targetCollider = null;


        /**
         * On a trigger collision invokes the reward delegate if the
         * collider matches with the target collider.
         */
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider == targetCollider) {
                rewardCollision.Invoke();
            }
        }
    }
}
