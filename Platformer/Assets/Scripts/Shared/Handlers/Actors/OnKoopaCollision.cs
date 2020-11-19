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
            PlayerController player = GetColliderPlayer(collision.collider);
            KoopaController monster = GetComponent<KoopaController>();

            if (!monster.shellActive) {
                base.OnCollisionEnter2D(collision);
            } else if (collision.collider.CompareTag("Monster")) {
                ActorController actor = GetColliderActor(collision.collider);
                RewardColliderPlayer(collision.collider);
                EmitEarnedPoints(earnedPoints, collision.GetContact(0).point);
                actor.Damage();
            } else if (player != null && player.isAlive && monster.isAlive) {
                if (monster.wasPropeled && IsDeadlyForPlayer(collision)) {
                    player.Damage();
                } else {
                    OnShieldCollision(collision);
                }
            }
        }


        /**
         * Handle collisions of the player with a  Koopa's shell.
         */
        private void OnShieldCollision(Collision2D collision) {
            PlayerController player = GetColliderPlayer(collision.collider);
            KoopaController koopa = GetComponent<KoopaController>();

            if (player != null && player.isAlive) {
                ContactPoint2D contact = collision.GetContact(0);
                player.BounceUp();

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
}
