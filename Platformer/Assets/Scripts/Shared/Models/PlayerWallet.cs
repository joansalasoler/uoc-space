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

        /** Player lifes */
        public int lifes = 0;

        /** Fireballs to throw */
        public int fireballs = 0;

        /** Score points */
        public int points = 0;

        /** Collected */
        public int coins = 0;


        /**
         * Clear the wallet.
         */
        public void Clear() {
            lifes = 0;
            fireballs = 0;
            points = 0;
            coins = 0;
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


        /**
         * Store a number of lifes.
         */
        public void StoreLifes(int lifes) {
            this.lifes += lifes;
            walletChange.Invoke(this);
        }


        /**
         * Store a number of fireballs.
         */
        public void StoreFireballs(int fireballs) {
            this.fireballs += fireballs;
            walletChange.Invoke(this);
        }
    }
}
