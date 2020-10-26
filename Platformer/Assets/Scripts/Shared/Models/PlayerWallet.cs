using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Shared {

    /**
     * Stores a player's earned coins and points.
     */
    [CreateAssetMenu]
    public class PlayerWallet : ScriptableObject {

        /** Invoked when the wallet changes */
        public static Action<PlayerWallet> walletChange;

        /** Score points */
        public int points = 0;

        /** Collected */
        public int coins = 0;


        /**
         * Reset the wallet.
         */
        public void Awake() {
            Clear();
        }


        /**
         * Empty the wallet.
         */
        public void Clear() {
            coins = 0;
            points = 0;
        }


        /**
         * Store a number of coins.
         */
        public void StoreCoins(int coins) {
            this.coins += coins;
            walletChange.Invoke(this);
        }


        /**
         * Store a number of points.
         */
        public void StorePoints(int points) {
            this.points += points;
            walletChange.Invoke(this);
        }
    }
}
