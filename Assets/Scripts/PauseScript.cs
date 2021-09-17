using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public static bool paused = false;

    public float pauseTransitionDuration;

    [SerializeField] public Button resumeButton, exitButton;
    public string exitScene;

    public AudioSource pauseSound, unpauseSound;
    
    void Start() {
        pauseScreen = GameObject.Find("PauseScreen");
        pauseScreen.SetActive(false);

        resumeButton.onClick.AddListener(ButtonUnpause);        // Button Listeners for resume and exit
        exitButton.onClick.AddListener(() => SwitchScene(exitScene));
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false) {      // Input handling for the pause to begin/end
            paused = true;
            pauseScreen.SetActive(true);
            StartCoroutine("Pause");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true) {
            paused = false;
            pauseScreen.SetActive(false);
            StartCoroutine("Unpause");
        }
    }

    IEnumerator Pause() {
        float timePassed = 0f;
        pauseSound.Play();
        
        while (timePassed < pauseTransitionDuration) {
            Time.timeScale = Mathf.SmoothStep(1f, 0f, timePassed/pauseTransitionDuration);     // Sick slow motion transition from unpaused to paused.
            timePassed += Time.unscaledDeltaTime;

            yield return null;
        }

        Time.timeScale = 0;
    }

    IEnumerator Unpause() {
        float timePassed = 0f;
        unpauseSound.Play();
        
        while (timePassed < pauseTransitionDuration) {
            Time.timeScale = Mathf.SmoothStep(0f, 1f, timePassed/pauseTransitionDuration);     // Reversed transition from paused to unpaused.
            timePassed += Time.unscaledDeltaTime;

            yield return null;
        }

        Time.timeScale = 1;
    }

    // private void OnGUI() {       // Testing Variables
    //     GUILayout.Label($"Paused?: {paused}");      
    //     GUILayout.Label($"Timescale: {Time.timeScale}");
    // }

    void ButtonUnpause() {      // Resume button calls this function
        paused = false;
        pauseScreen.SetActive(false);
        StartCoroutine("Unpause");
    }

    void SwitchScene(string scene) {        // Exit button calls this function, remember to set the proper scene name in the 'Exit Scene' field in the editor.
        SceneManager.LoadScene(scene);
    }
}