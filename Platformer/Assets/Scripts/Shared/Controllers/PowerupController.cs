using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the power-ups.
     */
    public class PowerupController : MonoBehaviour {

        /** Wether the power-up is active */
        public bool wasCollected = false;


        /**
         * Collects this power-up.
         */
        public virtual void Collect() {
            if (wasCollected == false) {
                wasCollected = true;
                gameObject.SetActive(false);
            }
        }
    }
}
