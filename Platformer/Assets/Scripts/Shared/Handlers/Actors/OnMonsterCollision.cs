using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles the collision of players with the monsters.
     */
    public class OnMonsterCollision: OnRewardCollision {

        /** Last time a force was added after killing a monster */
        private static float lastForceTime = 0.0f;

        /** Push up force when the player kills a monster */
        public float bounceForce = 1300.0f;


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
                    float elapsedTime = Time.fixedTime - lastForceTime;

                    if (elapsedTime > 0.2) {
                        var body = player.GetComponent<Rigidbody2D>();
                        body.AddForce(bounceForce * Vector2.up);
                        lastForceTime = Time.fixedTime;
                    } else if (elapsedTime < 0.5) {
                        RewardColliderPlayer(collision.collider);
                        points += earnedPoints;
                    }

                    RewardColliderPlayer(collision.collider);
                    EmitEarnedPoints(points, collision.GetContact(0).point);
                    monster.Kill();
                }
            }
        }


        /**
         * Checks if the collision must kill the player.
         */
        private bool IsDeadlyForPlayer(Collision2D collision) {
            Vector2 monsterCenter = collision.collider.bounds.center;
            Vector2 playerCenter = collision.otherCollider.bounds.center;

            return monsterCenter.y - playerCenter.y < .5f;
        }
    }
}
