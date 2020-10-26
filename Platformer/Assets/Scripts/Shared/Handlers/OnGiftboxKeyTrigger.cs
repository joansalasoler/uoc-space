using UnityEngine;
using System;

namespace Game.Shared {

    /**
     * Delegates collisions for a reward key trigger.
     */
    public class OnGiftboxKeyTrigger : MonoBehaviour {

        /** Reward collision delegate */
        public Action<Collider2D> giftboxKeyTriggerEnter;


        /**
         * On a trigger collision invokes the reward delegate if the
         * collider matches with the target collider.
         */
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                giftboxKeyTriggerEnter.Invoke(collider);
            }
        }
    }
}
