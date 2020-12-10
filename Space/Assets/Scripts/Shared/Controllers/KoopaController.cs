using System;
using System.Collections;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for Koopa monsters.
     */
    public class KoopaController : MonsterController {

        /** If a propel force was added to the monster */
        [HideInInspector] public bool wasPropeled = false;

        /** If the monster cannot damage the player */
        public bool shellActive = false;

        /** Time the monstar was stopped without being propeled */
        private float stoppedTime = 0.0f;

        /** Maximum speed of the monster prior to its propulsion*/
        private float maxSpeed = 3.0f;


        /**
         * {inheritDoc}
         */
        public override void OnHeadCollision() {
            AudioService.PlayOneShot(gameObject, "Shell Collide");
            stoppedTime = Time.time;
            ActivateShell();
        }


        /**
         * Makes the monster bounce from side to side of the playfield.
         */
        public void StartPropulsion(float force) {
            motionController.impulse = force * Vector2.right;
            motionController.direction = force / Mathf.Abs(force);
            motionController.maxSpeed = 15.0f;
            wasPropeled = true;
            EnableMotion();
        }


        /**
         * Stops the monster's propulsion movement.
         */
        public void StopPropulsion() {
            stoppedTime = Time.time;
            motionController.StopMoving();
            wasPropeled = false;
            DisableMotion();
        }


        /**
         * Activate the shell state of this monster.
         */
        private void ActivateShell() {
            gameObject.layer = LayerMask.NameToLayer("Shells");
            maxSpeed = motionController.maxSpeed;
            actorAnimator.SetBool("isHiding", true);
            shellActive = true;
            wasPropeled = false;
            DisableMotion();
        }


        /**
         * Deactivate the shell state of this monster.
         */
        private void DeactivateShell() {
            gameObject.layer = LayerMask.NameToLayer("Monster");
            motionController.maxSpeed = maxSpeed;
            actorAnimator.SetBool("isHiding", false);
            shellActive = false;
            wasPropeled = false;
            EnableMotion();
        }


        /**
         * Deactivate the monster's shell automatically.
         */
        private void Update() {
            if (shellActive && !wasPropeled) {
                if (Time.time - stoppedTime > 5.0f) {
                    DeactivateShell();
                }
            }
        }
    }
}
