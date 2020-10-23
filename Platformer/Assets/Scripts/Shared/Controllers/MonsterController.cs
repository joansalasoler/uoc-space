using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for monster characters.
     */
    public class MonsterController : ActorController {

        /**
         * {inheritDoc}
         */
        public override void Kill() {
            base.Kill();
            gameObject.SetActive(false);
        }
    }
}
