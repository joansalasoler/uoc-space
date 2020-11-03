using UnityEngine;
using System.Collections.Generic;

namespace Game.Shared {

    /**
     * Handle collisions for an object that contains rewards.
     */
    public class OnGiftboxCollision : OnRewardCollision {

        /** Reward object instances */
        [SerializeField] private List<GameObject> rewards = null;

        /** Sprite to use when the box becomes empty */
        [SerializeField] private Sprite emptySprite = null;


        /**
         * Invoked when the object is enabled.
         */
        private void Start() {
            var trigger = GetComponentInChildren<OnBrickKeyTrigger>();
            trigger.brickKeyTriggerEnter = OnBrickKeyTriggerEnter;
        }


        /**
         * Invoked when an object collides with this object.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            // Only collisions with the Giftbox key are rewarded
        }


        /**
         * On collision activates the next reward on the queue.
         */
        protected void OnBrickKeyTriggerEnter(Collider2D collider) {
            if (!IsEmpty() && IsAliveColliderActor(collider)) {
                var collisionPoint = Vector3.up + collider.bounds.center;
                EmitEarnedPoints(earnedPoints, collisionPoint);
                RewardColliderPlayer(collider);
                PopNextReward();
            }

            if (IsEmpty() && emptySprite != null) {
                var renderer = GetComponentInChildren<SpriteRenderer>();
                renderer.sprite = emptySprite;
            }
        }


        /**
         * Activate the first reward on the list and remove it.
         */
        protected void PopNextReward() {
            rewards[0].SetActive(true);
            rewards.RemoveAt(0);
        }


        /**
         * Checks if any rewards are left.
         */
        public bool IsEmpty() {
            return rewards.Count < 1;
        }
    }
}
