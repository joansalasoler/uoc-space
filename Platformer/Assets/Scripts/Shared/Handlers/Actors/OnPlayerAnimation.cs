using System;
using UnityEngine;

namespace Game.Shared {

    /**
     * Play sounds on the player animations.
     */
    public class OnPlayerAnimation: MonoBehaviour {

        /** Current playing audio event */
        private string currentEvent = null;


        /**
         * Stop the loops when the player is idle.
         */
        private void OnPlayerIdleEnter() {
            StopAudioLoop();
        }


        /**
         * Play the walk sound loop.
         */
        private void OnPlayerWalkEnter() {
            PlayAudioLoop("Player Walk");
        }


        /**
         * Play the run sound loop.
         */
        private void OnPlayerRunEnter() {
            PlayAudioLoop("Player Run");
        }


        /**
         * Play the jump sound event.
         */
        private void OnPlayerJumpEnter() {
            PlayAudioShot("Player Jump");
        }


        /**
         * Play the hurt sound event.
         */
        private void OnPlayerHurtEnter() {
            PlayAudioShot("Player Hurt");
        }


        /**
         * Play the die sound event.
         */
        private void OnPlayerDieEnter() {
            PlayAudioShot("Player Die");
        }


        /**
         * Play an audio loop given its name.
         */
        private void PlayAudioLoop(string eventName) {
            if (eventName != currentEvent) {
                StopAudioLoop();
                AudioService.PlayLoop(gameObject, eventName);
                currentEvent = eventName;
            }
        }


        /**
         * Play an audio event given its name.
         */
        private void PlayAudioShot(string eventName) {
            if (eventName != currentEvent) {
                StopAudioLoop();
                AudioService.PlayOneShot(gameObject, eventName);
                currentEvent = eventName;
            }
        }


        /**
         * Stop the currently playing loop if any.
         */
        private void StopAudioLoop() {
            if (currentEvent != null) {
                AudioService.StopLoop(gameObject);
                currentEvent = null;
            }
        }
    }
}
