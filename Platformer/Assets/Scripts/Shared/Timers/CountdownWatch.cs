using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Shared.Timers {

    /**
     * A count down watch that emits events.
     */
    public class CountdownWatch : MonoBehaviour {

        /** Seconds set as a target */
        public float targetSeconds = 400.0f;

        /** Emitted when the timer has elapsed */
        public UnityEvent timerElapsed = null;

        /** Time when the watch was started in seconds */
        private float startTime = 0.0f;



        /**
         * Start or resume the watch.
         */
        public void Start() {
            startTime = Time.time;
            Invoke("OnTimerElapsed", targetSeconds);
        }


        /**
         * Gets the elsapsed time in seconds.
         */
        public float GetSeconds() {
            float elapsed = Time.time - startTime;
            float remaining = targetSeconds - elapsed;

            return remaining > 0.0f ? remaining : 0.0f;
        }


        /**
         * Invoked when the timer has elapsed.
         */
        private void OnTimerElapsed() {
            timerElapsed.Invoke();
        }
    }
}
