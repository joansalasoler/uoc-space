using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Gift box extralife activated.
     */
    public class OnExtralifeEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Activate Extralife");
        }
    }
}
