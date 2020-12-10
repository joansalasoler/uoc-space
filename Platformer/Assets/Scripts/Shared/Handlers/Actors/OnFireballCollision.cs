using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Collisions with fireballs.
     */
    public class OnFireballCollision: OnPowerupCollision {

        /** Template for the destroy effect */
        [SerializeField] protected GameObject destroyEffect = null;


        /**
         * Damage the monsters if the ball hits them or destroy the
         * fireballs if it collides on the horizontal with something.
         */
        protected override void OnCollisionEnter2D(Collision2D collision) {
            base.OnCollisionEnter2D(collision);

            if (IsAliveColliderActor(collision.collider)) {
                GetColliderActor(collision.collider).Damage();
                Destroy(gameObject);
            } else {
                ContactPoint2D[] contacts = new ContactPoint2D[16];
                int contactCount = collision.GetContacts(contacts);

                foreach (ContactPoint2D contact in contacts) {
                    if (Mathf.Abs(contact.normal.x) >= 0.3) {
                        ShowCollision(collision.GetContact(0).point);
                        Destroy(gameObject);
                    }
                }
            }
        }


        /**
         * Shows a particles effect on collision with objects.
         */
        private void ShowCollision(Vector2 position) {
            GameObject effect = Instantiate(destroyEffect);
            effect.transform.position = position;
            effect.SetActive(true);
            Destroy(effect, 1.0f);
        }
    }
}
