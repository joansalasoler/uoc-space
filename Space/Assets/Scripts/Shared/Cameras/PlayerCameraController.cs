using UnityEngine;

namespace Game.Shared {

    /**
     * A camera that follows a target player on the horizontal position.
     */
    public class PlayerCameraController : MonoBehaviour {

        /** Target player to follow */
        public PlayerController player;

        /** Offset position of the target */
        private float offsetX;


        /**
         * Store the player offset position.
         */
        private void OnEnable() {
            offsetX = player.transform.position.x - transform.position.x;
        }


        /**
         * Move the camera where the player is.
         */
        private void LateUpdate() {
            if (player.isAlive) {
                Vector3 position = transform.position;
                position.x = player.transform.position.x - offsetX;
                transform.position = position;
            }
        }
    }
}
