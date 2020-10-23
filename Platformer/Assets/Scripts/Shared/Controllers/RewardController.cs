using UnityEngine;
using System.Collections.Generic;

namespace Game.Shared {

    /**
     * Controller for an object that contains rewards.
     */
    public class RewardController : MonoBehaviour {

        /** Reward object instances */
        [SerializeField] private List<GameObject> rewards = null;


        /**
         * Checks if any rewards are left.
         */
        public bool IsEmpty() {
            return rewards.Count < 1;
        }


        /**
         * Invoked when the object is enabled.
         */
        private void Start() {
            var delegator = GetComponentInChildren<RewardDelegator>();
            delegator.rewardCollision = OnRewardCollision;
        }


        /**
         * On collision activates the next reward on the queue.
         */
        private void OnRewardCollision() {
            if (IsEmpty() == false) {
                GameObject reward = rewards[0];
                reward.SetActive(true);
                rewards.RemoveAt(0);
            }
        }
    }
}
