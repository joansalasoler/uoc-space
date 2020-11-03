using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Gift box mushroom activated.
     */
    public class OnMushroomEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Activate Mushroom");
        }
    }
}
