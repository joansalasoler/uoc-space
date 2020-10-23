using System;
using UnityEngine;

namespace Game.Shared {

    /**
     *
     */
    public class OnMonsterCollision: MonoBehaviour {

        /**
         * Invoked when an object collides with this object.
         */
        private void OnCollisionEnter2D(Collision2D collision) {
            GameObject target = collision.collider.gameObject;
            PlayerController player = target.GetComponent<PlayerController>();
            MonsterController monster = GetComponent<MonsterController>();

            if (player != null && player.isAlive && monster.isAlive) {
                if (IsDeadlyForCollider(collision)) {
                    player.Kill();
                } else {
                    monster.Kill();
                }
            }
        }


        /**
         *
         */
        private bool IsDeadlyForCollider(Collision2D collision) {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 center = collision.collider.bounds.center;

            return center.y - contact.point.y < .15f;
        }
    }
}
