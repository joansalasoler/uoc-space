using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Shared;
using Game.Shared.Timers;


/**
 * Game scene controller.
 */
public class GameController : MonoBehaviour {

    /** Player instance */
    public PlayerController player;

    /** Pause overlay controller */
    public PauseController pause;

    /** Game over overlay controller */
    public EndgameController endgame;

    /** Player position at the start of the game */
    public Vector3 spawnPoint = new Vector3(-107.5f, 120.0f, 0.0f);

    /** Player position after a resurrection */
    public Vector3 respawnPoint = new Vector3(-107.5f, -4.8f, 0.0f);

    /** Initial number of lifes of a player */
    public int playerLifes = 3;


    /**
     * Initialize the game.
     */
    private void Start() {
        player.wallet.Clear();
        player.wallet.StoreLifes(playerLifes);
        player.transform.position = spawnPoint;
    }


    /**
     * Attach the events.
     */
    private void OnEnable() {
        player.playerWon += OnPlayerWon;
        player.playerDied += OnPlayerDied;
    }


    /**
     * Detach the events.
     */
    private void OnDisable() {
        player.playerWon -= OnPlayerWon;
        player.playerDied -= OnPlayerDied;
    }


    /**
     * Loads the main menu scene.
     */
    public void LoadMainScene() {
        SceneManager.LoadScene("Main");
    }


    /**
     * Shows the congratulations overlay.
     */
    public void ShowCongratsOverlay() {
        endgame.ShowCongratsOverlay();
    }


    /**
     * Shows the game over overlay.
     */
    public void ShowGameOverOverlay() {
        endgame.ShowGameOverOverlay();
    }


    /**
     * Shows the time up overlay.
     */
    public void ShowTimeUpOverlay() {
        endgame.ShowTimeUpOverlay();
    }


    /**
     * Restarts the game.
     */
    public void OnGameRestart() {
        player.wallet.Clear();
    }


    /**
     * Respawns the player.
     */
    public void OnGameRespawn() {
        player.Resurrect();
        player.transform.position = respawnPoint;
    }


    /**
     * Transitions to the game over state.
     */
    public void OnPlayerDied() {
        player.wallet.StoreLifes(-1);

        if (player.wallet.lifes > 0) {
            Invoke("OnGameRespawn", 3.0f);
        } else {
            pause.enabled = false;
            Invoke("ShowGameOverOverlay", 3.0f);
            Invoke("LoadMainScene", 6.0f);
        }
    }


    /**
     * Transitions to the player wins state.
     */
    public void OnPlayerWon() {
        ShowCongratsOverlay();
        Invoke("LoadMainScene", 6.0f);
    }


    /**
     * Transitions to the player wins state.
     */
    public void OnTimeUp() {
        if (player.hasWon == false) {
            player.Damage();
            pause.enabled = false;
            ShowTimeUpOverlay();
            Invoke("ShowGameOverOverlay", 3.0f);
            Invoke("LoadMainScene", 6.0f);
        }
    }
}
