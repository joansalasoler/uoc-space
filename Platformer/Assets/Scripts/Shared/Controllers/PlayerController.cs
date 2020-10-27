using System;
using UnityEngine;
using System.Collections;

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

        /** Star power-up duration in seconds */
        public float starTimeout = 20.0f;

        /** Shield power-up duration in seconds */
        public float shieldTimeout = 5.0f;

        /** Wheter the star power-up is active */
        public bool starActive = false;

        /** Wheter the flower power-up is active */
        public bool flowerActive = false;

        /** Wheter the mushroom power-up is active */
        public bool mushroomActive = false;

        /** Wheter the shield power-up is active */
        public bool shieldActive = false;

        /** Set to true when the player is not alive */
        private bool wasKilled = false;

        /** Set to true when the player has won */
        private bool hasWon = false;

        /** Input events controlller */
        private InputController input;

        /** Sprite renderer of the player */
        private SpriteRenderer actorRenderer;


        /**
         * Obtain references to the required components when the
         * player is enabled.
         */
        protected override void OnEnable() {
            base.OnEnable();
            actorRenderer = GetComponent<SpriteRenderer>();
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
                DisableColliders();
                actorRigidbody.AddForce(input.jumpForce * Vector2.up);
                wasKilled = true;
            }
        }


        /**
         * Activates the flower power-up.
         */
        public void ActivateFlowerPowers() {
            flowerActive = true;
        }


        /**
         * Activates the star power-up.
         */
        public void ActivateMushroomPowers() {
            mushroomActive = true;
        }


        /**
         * Activates the shield power-up.
         */
        public void ActivateShieldPowers() {
            StopCoroutine("ActivateShieldCoroutine");
            StartCoroutine("ActivateShieldCoroutine");
        }


        /**
         * Activates the star power-up.
         */
        public void ActivateStarPowers() {
            StopCoroutine("ActivateStarCoroutine");
            StartCoroutine("ActivateStarCoroutine");
        }


        /**
         * Deactivates all the active power-up.
         */
        public void DeactivateAllPowers() {
            starActive = false;
            flowerActive = false;
            mushroomActive = false;
            shieldActive = false;
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
        public override void Damage() {
            if (!starActive && !shieldActive) {
                if (flowerActive || mushroomActive) {
                    DeactivateAllPowers();
                    ActivateShieldPowers();
                } else {
                    base.Damage();
                    input.enabled = false;
                    playerDied.Invoke();
                }
            }
        }


        /**
         * Activate the shield for the defined timeout.
         */
        public IEnumerator ActivateStarCoroutine() {
            starActive = true;
            yield return new WaitForSeconds(starTimeout);
            starActive = false;
         }


        /**
         * Activate the shield for the defined timeout.
         */
        public IEnumerator ActivateShieldCoroutine() {
            shieldActive = true;
            yield return new WaitForSeconds(shieldTimeout);
            shieldActive = false;
         }
    }
}
