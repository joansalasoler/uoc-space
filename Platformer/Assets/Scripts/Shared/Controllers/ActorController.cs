using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Base controller for the actors.
     */
    public class ActorController : MonoBehaviour {

        /** Wether the actor is dead or alive */
        public bool isAlive = true;


        /**
         * Kills this actor.
         */
        public virtual void Kill() {
            isAlive = false;
        }
    }
}
