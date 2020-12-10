using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for the clouds.
     */
    public class CloudController: MonoBehaviour {


        /**
         * Start moving the clouds only when visible.
         */
        private void OnBecameVisible() {
            GetComponent<ConstantForce2D>().enabled = true;
        }
    }
}
