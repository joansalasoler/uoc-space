using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * UFO landing spotlight.
     */
    public class OnSpotlightEnable: MonoBehaviour {

        private bool hasBeenRun = false;

        private void OnBecameVisible() {
            if (hasBeenRun == false) {
                AudioService.PlayOneShot(gameObject, "Player Land");
                Destroy(gameObject, 3.0f);
                hasBeenRun = true;
            }
        }
    }
}
