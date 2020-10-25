using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for monster characters.
     */
    public class MonsterController : ActorController {

        /** Motion controller for the monster */
        protected MotionController motionController;


        /**
         * {inheritDoc}
         */
        public override void Kill() {
            motionController.enabled = false;
            motionController.StopMoving();
            base.Kill();
        }


        /**
         * Obtain references to the required components when the
         * player is enabled.
         */
        protected override void OnEnable() {
            base.OnEnable();
            motionController = GetComponent<MotionController>();
        }


        /**
         * Activate the motion controller when visible.
         */
        private void OnBecameVisible() {
            if (this.isAlive == true) {
                motionController.enabled = true;
            }
        }


        /**
         * Disable the monster if it is dead.
         */
        private void OnBecameInvisible() {
            if (this.isAlive == false) {
                Destroy(gameObject);
            }
        }
    }
}
