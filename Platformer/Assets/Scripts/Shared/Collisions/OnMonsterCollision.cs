using System;
using UnityEngine;

namespace Game.Shared {

    /**
     *
     */
    public class OnMonsterCollision: MonoBehaviour {

        /** Push up force when the player kills a monster */
        public float bounceForce = 1300.0f;


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
                    var body = player.GetComponent<Rigidbody2D>();
                    body.AddForce(bounceForce * Vector2.up);
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
