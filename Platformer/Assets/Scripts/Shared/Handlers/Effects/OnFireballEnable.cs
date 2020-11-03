using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Player fireball enable.
     */
    public class OnFireballEnable: MonoBehaviour {
        private void OnEnable() {
            AudioService.PlayOneShot(gameObject, "Player Fire");
        }
    }
}
