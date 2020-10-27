using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the actors.
     */
    public class ActorController : MonoBehaviour {

        /** Wether the actor is dead or alive */
        public bool isAlive = true;

        /** Animator for the actor */
        protected Animator actorAnimator;

        /** Rigidbody of the actor */
        protected Rigidbody2D actorRigidbody;


        /**
         * Obtain references to the required components.
         */
        protected virtual void OnEnable() {
            actorAnimator = GetComponent<Animator>();
            actorRigidbody = GetComponent<Rigidbody2D>();
        }


        /**
         * Disable all the colliders of this actor.
         */
        public virtual void DisableColliders() {
            var colliders = GetComponentsInChildren<Collider2D>();
            Array.ForEach(colliders, c => c.enabled = false);
        }


        /**
         * Damage this actor (kills it by default).
         */
        public virtual void Damage() {
            Kill();
        }


        /**
         * Kills this actor.
         */
        public virtual void Kill() {
            isAlive = false;
            actorAnimator.SetBool("isAlive", false);
        }
    }
}
