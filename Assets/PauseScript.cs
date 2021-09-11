using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // TODO: Add a popup UI with the resume button, exit button, and such.
    public static bool paused = false;

    public float pauseTransitionDuration = 1f;

    // Update is called once per frame
    void Update() {
        if (paused == false && Input.GetButtonDown("escape")) {
            StartCoroutine("Pause");
        }

        if (paused == true && Input.GetButtonDown("escape")) {
            StartCoroutine("Unpause");
        }
    }

    IEnumerator Pause() {
        while (Time.timeScale > 0) {
            Time.timeScale = Mathf.SmoothStep(1f, 0f, pauseTransitionDuration);     // Sick slow motion transition from unpaused to paused.
        }
        yield return null;
    }

    IEnumerator Unpause() {
        while (Time.timeScale < 1) {
            Time.timeScale = Mathf.SmoothStep(0f, 1f, pauseTransitionDuration);     // Reversed transition from paused to unpaused.
        }
        yield return null;
    }
}