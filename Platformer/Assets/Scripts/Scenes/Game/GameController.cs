using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Shared;


/**
 * Game scene controller.
 */
public class GameController : MonoBehaviour {

    /** Player instance */
    public PlayerController player;

    /** Game over overlay controller */
    public GameOverController gameover;

    /** Pause overlay controller */
    public PauseController pause;


    /**
     * Invoked when the script is started.
     */
    private void Start() {
        player.playerDied += InvokeGameOver;
        player.playerDied += InvokeMainScene;
    }


    /**
     * Loads the main menu scene.
     */
    private void LoadMainScene() {
        SceneManager.LoadScene("Main");
    }


    /**
     * Toggles the game over screen.
     */
    private void ShowGameOverScreen() {
        gameover.ToggleOverlay();
    }


    /**
     * Shows the game over screen after a delay.
     */
    private void InvokeGameOver() {
        pause.enabled = false;
        Invoke("ShowGameOverScreen", 3.0f);
    }


    /**
     * Loads the main menu after a delay.
     */
    private void InvokeMainScene() {
        Invoke("LoadMainScene", 6.0f);
    }
}
