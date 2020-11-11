using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Gift box star power-up activate.
     */
    public class OnStarEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Activate Star");
        }
    }
}
