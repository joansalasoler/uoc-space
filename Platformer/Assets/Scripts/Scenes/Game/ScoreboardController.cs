using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Shared;
using Game.Shared.Timers;


/**
 * Show the score of the player on the canvas.
 */
public class ScoreboardController : MonoBehaviour {

    /** Template for the displayed points */
    [SerializeField] protected GameObject pointsTemplate = null;

    /** Count down watch instance */
    [SerializeField] private CountdownWatch countdownWatch = null;

    /** Text for the number of coins earned */
    [SerializeField] private Text coins = null;

    /** Text for the number of points earned */
    [SerializeField] private Text points = null;

    /** Text for the countdown time remaining */
    [SerializeField] private Text time = null;


    /**
     * Attach the events.
     */
    public void OnEnable() {
        PlayerWallet.walletChange += UpdateWallet;
        OnRewardCollision.pointsEarned += OnPointsEarned;
        InvokeRepeating("UpdateCountdown", 0.0f, 1.0f);
    }


    /**
     * Detach the events.
     */
    public void OnDisable() {
        PlayerWallet.walletChange -= UpdateWallet;
        OnRewardCollision.pointsEarned -= OnPointsEarned;
        CancelInvoke("UpdateCountdown");
    }


    /**
     * Update the texts for a wallet.
     */
    public void UpdateWallet(PlayerWallet wallet) {
        points.text = wallet.points.ToString();
        coins.text = wallet.coins.ToString();
    }


    /**
     * Update the text for the countdown.
     */
    public void UpdateCountdown() {
        time.text = countdownWatch.GetSeconds().ToString("0");
    }


    /**
     * Shows a dust effect when points are earned by the player.
     */
    private void OnPointsEarned(Vector2 position, int points) {
        GameObject dust = Instantiate(pointsTemplate);
        dust.transform.position = position;
        dust.SetActive(true);
        Destroy(dust, 1.0f);
    }
}
