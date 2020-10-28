using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Gift box coin enable.
     */
    public class OnCoinEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Collect Coin");
        }
    }
}
