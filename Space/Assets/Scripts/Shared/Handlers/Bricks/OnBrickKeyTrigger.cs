using UnityEngine;
using System;

namespace Game.Shared {

    /**
     * Delegates collisions for a reward key trigger.
     */
    public class OnBrickKeyTrigger : MonoBehaviour {

        /** Reward collision delegate */
        public Action<Collider2D> brickKeyTriggerEnter;

        /** Minimus time between trigger invocations */
        private float invokeDelay = 0.5f;

        /** Last time the trigger was invoked */
        private float timeStamp = 0.0f;


        /**
         * On a trigger collision invokes the reward delegate if the
         * collider matches with the target collider.
         */
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                if (Time.time - timeStamp > invokeDelay) {
                    brickKeyTriggerEnter.Invoke(collider);
                    timeStamp = Time.time;
                }
            }
        }
    }
}
