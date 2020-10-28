using UnityEngine;
using System;

namespace Game.Shared {

    /**
     * Declares a winning player when it collides with a finish line.
     */
    public class OnFinishLineTrigger : MonoBehaviour {

        /**
         * Fired when an object collides with the finish line.
         */
        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.CompareTag("Player")) {
                GameObject target = collider.gameObject;
                var player = target.GetComponent<PlayerController>();

                if (player != null && !player.hasWon) {
                    AudioService.PlayOneShot(gameObject, "Player Win");
                    player.DeclareWinner();
                }
            }
        }
    }
}
