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

            if (player != null && player.isAlive && monster.isAlive) {
                if (player.shieldActive) {
                    return;
                } else if (player.starActive) {
                    int points = 2 * earnedPoints;
                    RewardColliderPlayer(collision.collider);
                    EmitEarnedPoints(points, collision.GetContact(0).point);
                    monster.Damage();
                } else if (IsDeadlyForPlayer(collision)) {
                    player.Damage();
                } else {
                    int points = earnedPoints;
                    float elapsedTime = Time.fixedTime - lastDamageTime;

                    if (elapsedTime > 0.2) {
                        player.BounceUp();
                        lastDamageTime = Time.fixedTime;
                    } else if (elapsedTime < 0.5) {
                        RewardColliderPlayer(collision.collider);
                        points += earnedPoints;
                    }

                    RewardColliderPlayer(collision.collider);
                    EmitEarnedPoints(points, collision.GetContact(0).point);
                    monster.OnHeadCollision();
                }
            }
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
