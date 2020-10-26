using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Controller for player characters.
     */
    public class PlayerController : ActorController {

        /** Invoked when the player dies */
        public Action playerDied;

        /** Invoked when the player wins */
        public Action playerWon;

        /** Stores the inventory of this player */
        public PlayerWallet wallet = null;

        /** Set to true when the player is not alive */
        private bool wasKilled = false;

        /** Set to true when the player has won */
        private bool hasWon = false;

        /** Input events controlller */
        private InputController input;

        /** Rigidbody of the player */
        private Rigidbody2D actorRigidbody;

        /** Sprite renderer of the player */
        private SpriteRenderer actorRenderer;


        /**
         * Obtain references to the required components when the
         * player is enabled.
         */
        protected override void OnEnable() {
            base.OnEnable();
            actorRenderer = GetComponent<SpriteRenderer>();
            actorRigidbody = GetComponent<Rigidbody2D>();
            input = GetComponent<InputController>();
        }


        /**
         * Update the player state and animations on each frame.
         *
         * Notice that in order to not miss any jump requests they are
         * cleared only on FixedUpdate.
         */
        private void Update() {
            actorRenderer.flipX = input.isFlipped;
            actorAnimator.SetBool("isRunning", input.isRunning);
            actorAnimator.SetBool("isJumping", input.isJumping);
            actorAnimator.SetFloat("moveSpeed", Mathf.Abs(input.velocity.x));
        }


        /**
         * Animates the dead of the player.
         */
        private void FixedUpdate() {
            if (isAlive == false && wasKilled == false) {
                var colliders = GetComponentsInChildren<Collider2D>();
                Array.ForEach(colliders, c => c.enabled = false);
                actorRigidbody.AddForce(input.jumpForce * Vector2.up);
                wasKilled = true;
            }
        }


        /**
         * Declare this player as a winner.
         */
        public void DeclareWinner() {
            if (hasWon == false) {
                playerWon.Invoke();
                hasWon = true;
            }
        }


        /**
         * {inheritDoc}
         */
        public override void Kill() {
            base.Kill();
            input.enabled = false;
            playerDied.Invoke();
        }
    }
}
