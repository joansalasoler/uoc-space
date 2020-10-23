using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * Game over screen controller.
 */
public class GameOverController : MonoBehaviour {

    /** Object to show when the player dies */
    [SerializeField] private GameObject overlay = null;


    /**
     * Toggle the game over overlay.
     */
    public void ToggleOverlay() {
        overlay.SetActive(overlay.activeSelf == false);
    }
}
