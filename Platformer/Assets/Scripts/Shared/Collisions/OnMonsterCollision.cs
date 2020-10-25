using System;
using UnityEngine;

namespace Game.Shared {

    /**
     *
     */
    public class OnMonsterCollision: MonoBehaviour {

        /** Last time a force was added after killing a monster */
        private static float lastForceTime = 0.0f;

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
                    float elapsedTime = Time.fixedTime - lastForceTime;

                    if (elapsedTime > 0.2) {
                        var body = player.GetComponent<Rigidbody2D>();
                        body.AddForce(bounceForce * Vector2.up);
                        lastForceTime = Time.fixedTime;
                    } else if (elapsedTime < 0.5) {
                        Debug.Log("Extra reward!");
                    }

                    monster.Kill();
                }
            }
        }


        /**
         *
         */
        private bool IsDeadlyForCollider(Collision2D collision) {
            Vector2 monsterCenter = collision.collider.bounds.center;
            Vector2 playerCenter = collision.otherCollider.bounds.center;

            return monsterCenter.y - playerCenter.y < .5f;
        }
    }
}
