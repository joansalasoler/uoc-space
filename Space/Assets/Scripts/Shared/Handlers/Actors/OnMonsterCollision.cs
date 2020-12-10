using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles the collision of players with the monsters.
     */
    public class OnMonsterCollision: OnRewardCollision {

        /** Last time a monster was damaged by the player */
        protected static float lastDamageTime = 0.0f;


        /**
         * Invoked when an object collides with this object. It kills the
         * player or the monster according to the collision point.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            PlayerController player = GetColliderPlayer(collision.collider);
            MonsterController monster = GetComponent<MonsterController>();
            int points = earnedPoints;

            // Handle only collisions with the player and if both the
            // player and the monser are alive

            if (player == null || !player.isAlive || !monster.isAlive) {
                return;
            }

            // If the player has star powers, kill the monster and reward
            // the player twice the monster's price.

            if (player.starActive) {
                points = 2 * earnedPoints;
                RewardColliderPlayer(collision.collider);
                EmitEarnedPoints(points, collision.GetContact(0).point);
                monster.Damage();
                return;
            }

            // Damage the player if the collision was not on the monster's head

            if (IsDeadlyForPlayer(collision)) {
                player.Damage();
                return;
            }

            // Damage the monster if the collision was on its head. When two
            // monsters are damaged this way in less than 0.5 seconds appart,
            // then reward the player twice of the price.

            player.BounceUp();

            if (Time.fixedTime - lastDamageTime < 0.5) {
                RewardColliderPlayer(collision.collider);
                points = 2 * earnedPoints;
            }

            lastDamageTime = Time.fixedTime;
            RewardColliderPlayer(collision.collider);
            EmitEarnedPoints(points, collision.GetContact(0).point);
            monster.OnHeadCollision();
        }


        /**
         * Checks if the collision must kill the player. That is, if the
         * collision did not happen on the head of the monster.
         */
        protected bool IsDeadlyForPlayer(Collision2D collision) {
            Vector2 monsterCenter = collision.collider.bounds.center;
            Vector2 playerCenter = collision.otherCollider.bounds.center;
            bool isDeadly = monsterCenter.y - playerCenter.y < .5f;

            return isDeadly;
        }
    }
}
