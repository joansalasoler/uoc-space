using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the actors.
     */
    public class ActorController : MonoBehaviour {

        /** Wether the actor is dead or alive */
        public bool isAlive = true;

        /** Animator for the player */
        protected Animator actorAnimator;


        /**
         * Kills this actor.
         */
        public virtual void Kill() {
            isAlive = false;
            actorAnimator.SetBool("isAlive", false);
        }


        /**
         * Obtain references to the required components.
         */
        protected virtual void OnEnable() {
            actorAnimator = GetComponent<Animator>();
        }
    }
}
