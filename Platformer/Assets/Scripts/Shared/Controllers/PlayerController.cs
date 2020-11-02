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

        /** Shield of the player*/
        public GameObject shield = null;

        /** Star shield of the player*/
        public GameObject superShield = null;

        /** Set to true when the player has won */
        public bool hasWon = false;

        /** Star power-up duration in seconds */
        public float starTimeout = 10.0f;

        /** Shield power-up duration in seconds */
        public float shieldTimeout = 2.0f;

        /** Wheter the star power-up is active */
        public bool starActive = false;

        /** Wheter the flower power-up is active */
        public bool flowerActive = false;

        /** Wheter the mushroom power-up is active */
        public bool mushroomActive = false;

        /** Wheter the shield power-up is active */
        public bool shieldActive = false;

        /** Set to true when the player is hurt */
        private bool isHurt = false;

        /** Set to true when the player is not alive */
        private bool wasKilled = false;

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
            actorAnimator.SetBool("isAlive", isAlive);
            actorAnimator.SetBool("isHurt", isHurt);
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
            input.fireEnabled = true;
        }


        /**
         * Activates the star power-up.
         */
        public void ActivateMushroomPowers() {
            mushroomActive = true;
            StartCoroutine("ActivateMushroomCoroutine");
        }


        /**
         * Activates the shield power-up.
         */
        public void ActivateShieldPowers() {
            StartCoroutine("ActivateShieldCoroutine");
        }


        /**
         * Activates the star power-up.
         */
        public void ActivateStarPowers() {
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
            input.fireEnabled = false;
            StartCoroutine("DeactivateMushroomCoroutine");
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
                if (mushroomActive) {
                    DeactivateAllPowers();
                    ActivateShieldPowers();
                } else {
                    Kill();
                }
            }
        }


        /**
         * {inheritDoc}
         */
        public override void Kill() {
            if (isAlive == true) {
                base.Kill();
                input.enabled = false;
                playerDied.Invoke();
            }
        }


        /**
         * Resurrects this player.
         */
        public void Resurrect() {
            isAlive = true;
            isHurt = false;
            wasKilled = false;
            input.enabled = true;
            EnableColliders();
            DeactivateAllPowers();
            ActivateShieldPowers();
        }


        /**
         * Activate the shield for the defined timeout.
         */
        private IEnumerator ActivateStarCoroutine() {
            starActive = true;
            superShield.SetActive(true);
            yield return new WaitForSeconds(starTimeout);
            superShield.SetActive(false);
            starActive = false;
         }


        /**
         * Activate the shield for the defined timeout.
         *
         * This coroutine makes the player jump back, activates the shield
         * and temporarily disables the player collisions with monsters.
         */
        private IEnumerator ActivateShieldCoroutine() {
            isHurt = true;
            shieldActive = true;

            SetIgnoreMonsterCollisions(true);
            actorRigidbody.AddForce(600.0f * Vector2.up);
            yield return new WaitForSeconds(0.1f);

            isHurt = false;
            shield.SetActive(true);
            yield return new WaitForSeconds(shieldTimeout);
            SetIgnoreMonsterCollisions(false);

            shield.SetActive(false);
            shieldActive = false;
         }


         /**
          * Scale the player when a mushroom is activated.
          */
        private IEnumerator ActivateMushroomCoroutine() {
            float currentScale = transform.localScale.y;

            for (float scale = currentScale; scale <= 1.4f; scale += 0.05f) {
                transform.localScale = new Vector3(scale, scale, scale);
                yield return new WaitForSeconds(0.025f);
            }
        }


         /**
          * Scale the player when a mushroom is deactivated.
          */
        private IEnumerator DeactivateMushroomCoroutine() {
            float currentScale = transform.localScale.y;

            for (float scale = currentScale; scale >= 1.0f; scale -= 0.05f) {
                transform.localScale = new Vector3(scale, scale, scale);
                yield return new WaitForSeconds(0.025f);
            }
        }


        /**
         * Ignore player collisions with the monsters
         */
        private void SetIgnoreMonsterCollisions(bool ignore) {
            int players = LayerMask.NameToLayer("Player");
            int monsters = LayerMask.NameToLayer("Monster");
            Physics2D.IgnoreLayerCollision(players, monsters, ignore);
        }
    }
}
