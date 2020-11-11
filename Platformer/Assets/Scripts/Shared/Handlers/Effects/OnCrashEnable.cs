using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Crash particles shown for a scoring collision.
     */
    public class OnCrashEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Collect Points");
        }
    }
}
