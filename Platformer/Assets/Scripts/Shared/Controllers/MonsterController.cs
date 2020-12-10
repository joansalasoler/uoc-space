using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for monster characters.
     */
    public class MonsterController : ActorController {

        /** Identifier for the type of monster */
        public string monsterType = "monster";

        /** Magintude of the force applied when damaged */
        public float damageForce = 600.0f;

        /** Motion controller for the monster */
        protected MotionController motionController;

        /** If the monster was activated */
        protected bool wasActivated = false;


        /**
         * Obtain references to the required components.
         */
        protected override void OnEnable() {
            base.OnEnable();
            motionController = GetComponent<MotionController>();
        }


        /**
         * {inheritDoc}
         */
        public override void Damage() {
            if (isAlive == true) {
                isAlive = false;
                AudioService.PlayOneShot(gameObject, "Monster Hurt");
                DisableMotion();
                DisableColliders();
                AddDamageForce();
            }
        }


        /**
         * {inheritDoc}
         */
        public override void Kill() {
            if (isAlive == true) {
                AudioService.PlayOneShot(gameObject, "Monster Die");
                DisableMotion();
                base.Kill();
            }
        }


        /**
         * Handle collisions with the monster's head.
         */
        public virtual void OnHeadCollision() {
            this.Kill();
        }


        /**
         * Enable this monster's movement.
         */
        protected void EnableMotion() {
            motionController.enabled = true;
            motionController.StartMoving();
        }


        /**
         * Stop the monster and disable its movement.
         */
        protected void DisableMotion() {
            if (motionController.enabled) {
                motionController.enabled = false;
                motionController.StopMoving();
            }
        }


        /**
         * Adds a force to the monster when it is damaged.
         */
        protected void AddDamageForce() {
            float direction = .5f * motionController.direction;
            Vector2 force = new Vector2(direction, 1.0f);
            actorRigidbody.AddForce(damageForce * force);
        }


        /**
         * Activate the motion controller when visible.
         */
        protected void OnBecameVisible() {
            if (!wasActivated && isAlive) {
                wasActivated = true;
                EnableMotion();
            }
        }


        /**
         * Disable the monster if it is dead.
         */
        protected void OnBecameInvisible() {
            if (this.isAlive == false) {
                Destroy(gameObject);
            }
        }
    }
}
