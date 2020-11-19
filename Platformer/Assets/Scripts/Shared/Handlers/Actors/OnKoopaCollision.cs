using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Handles collisions with the Koopa monsters.
     */
    public class OnKoopaCollision: OnMonsterCollision {

        /**
         * Handle the collision of Koopas with other objects.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            KoopaController monster = GetComponent<KoopaController>();

            // If the monster shell is not active, handle the collision
            // like any other monster

            if (!monster.shellActive) {
                base.OnCollisionEnter2D(collision);
                return;
            }

            // If the shell collides with a monster, damage the other monster

            if (collision.collider.CompareTag("Monster")) {
                ActorController actor = GetColliderActor(collision.collider);
                RewardColliderPlayer(collision.collider);
                EmitEarnedPoints(earnedPoints, collision.GetContact(0).point);
                actor.Damage();
                return;
            }

            // If the collision happened with a player, either kill the
            // player or add remove the shell propulsion force.

            PlayerController player = GetColliderPlayer(collision.collider);

            if (player != null && player.isAlive && monster.isAlive) {
                if (monster.wasPropeled && IsDeadlyForPlayer(collision)) {
                    player.Damage();
                } else {
                    player.BounceUp();
                    OnShieldCollision(collision);
                }
            }
        }


        /**
         * Handle collisions of the player with a  Koopa's shell. This
         * causes the shell to move fast from side to side or stop its
         * movement if it was already moving.
         */
        private void OnShieldCollision(Collision2D collision) {
            KoopaController koopa = GetComponent<KoopaController>();
            ContactPoint2D contact = collision.GetContact(0);

            if (!koopa.wasPropeled) {
                AudioService.PlayOneShot(gameObject, "Shell Collide");
                koopa.StartPropulsion(15.0f * contact.normal.x);
            } else if (koopa.wasPropeled && contact.normal.y < 0.5f ) {
                AudioService.PlayOneShot(gameObject, "Shell Collide");
                koopa.StopPropulsion();
            }
        }
    }
}
