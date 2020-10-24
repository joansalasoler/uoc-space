using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shared {

    /**
     *
     */
    public class ScoreController : MonoBehaviour {

        /** Invoked when points are earned */
        public static Action<ScoreController> pointsEarned;

        /** Score point earned */
        public int points = 0;


        /**
         *
         */
        public void Awake() {
            pointsEarned += Debug.Log;
        }


        /**
         *
         */
        public void EarnPoints(int points) {
            this.points += points;
            pointsEarned.Invoke(this);
            Debug.Log($"Earned { points} points");
        }
    }
}
