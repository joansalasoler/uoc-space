using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Generic reward collision handler.
     */
    public class OnRewardCollision: MonoBehaviour {

        /** Invoked when some points are earned */
        public static Action<Vector2, int> pointsEarned;

        /** Points earned on the collision */
        [SerializeField] protected int earnedPoints = 100;

        /** Coins earned on the collision */
        [SerializeField] protected int earnedCoins = 0;


        /**
         * Invoked when an object collides with this object.
         */
        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            if (IsAliveColliderActor(collision.collider)) {
                RewardColliderPlayer(collision.collider);
                EmitEarnedPoints(earnedPoints, collision.GetContact(0).point);
            }
        }


        /**
         * Checks if the collider is a player an it is alive.
         */
        protected bool IsAliveColliderActor(Collider2D collider) {
            var actor = collider.gameObject.GetComponent<ActorController>();
            bool isAlive = (actor != null && actor.isAlive);

            return isAlive;
        }


        /**
         * Emits an event for the point that are collected.
         */
        protected void EmitEarnedPoints(int points, Vector2 position) {
            if (points > 0) pointsEarned.Invoke(position, points);
        }


        /**
         * Checks if the collider has a wallet and if that is the case
         * adds the reward points and coins to it.
         */
        protected void RewardColliderPlayer(Collider2D collider) {
            PlayerController player = GetColliderPlayer(collider);

            if (player is PlayerController && player.wallet != null) {
                if (earnedCoins > 0) player.wallet.StoreCoins(earnedCoins);
                if (earnedPoints > 0) player.wallet.StorePoints(earnedPoints);
            }
        }


        /**
         * Obtain the player controller component of the collider.
         */
        protected PlayerController GetColliderPlayer(Collider2D collider) {
            return collider.gameObject.GetComponent<PlayerController>();
        }
    }
}
