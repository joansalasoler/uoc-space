using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


/**
 * Pause screen controller.
 */
public class PauseController : MonoBehaviour {

    /** Object to show when paused */
    [SerializeField] private GameObject overlay = null;


    /**
     * Shows the main menu scene.
     */
    public void LoadScene(string name) {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        SceneManager.LoadScene(name);
    }


    /**
     * Toggle game pause status.
     */
    public void TogglePause() {
        Time.timeScale = overlay.activeSelf ? 1.0f : 0.0f;
        AudioListener.pause = (overlay.activeSelf == false);
        overlay.SetActive(overlay.activeSelf == false);
        EventSystem.current.SetSelectedGameObject(null);
        overlay.GetComponentInChildren<Button>().Select();
    }


    /**
     * Invoked on each frame update.
     */
    private void Update() {
        if (Input.GetButtonUp("Cancel")) {
            TogglePause();
        }
    }
}
