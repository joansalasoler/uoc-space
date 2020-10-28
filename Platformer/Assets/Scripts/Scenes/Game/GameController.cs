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
     * Transitions to the game over state.
     */
    public void OnPlayerDied() {
        pause.enabled = false;
        Invoke("ShowGameOverOverlay", 3.0f);
        Invoke("LoadMainScene", 6.0f);
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
