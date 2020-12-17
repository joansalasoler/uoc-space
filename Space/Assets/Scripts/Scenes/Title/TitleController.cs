using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


/**
 * Title scene controller.
 */
public class TitleController : MonoBehaviour {

    /**
     * Load the main menu after a delay.
     */
    private void Start() {
        Invoke("LoadMainMenu", 5.0f);
    }


    /**
     * Load the main menu scene.
     */
    public void LoadMainMenu() {
        SceneManager.LoadScene("Main");
    }
}
