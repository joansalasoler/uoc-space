using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Gift box flower power-up activated.
     */
    public class OnFlowerEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Activate Flower");
        }
    }
}
