using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // This coroutine can be called by any other script so that the camera shake can be activated at any time
    // It takes the paramters of how long the shake should last and how much the camera should shake
    public IEnumerator Shake (float duration, float magnitude)
    {
        // Gets the starting position of the camera
        Vector3 originalPos = transform.localPosition;

        // Uses a float to keep track of how long the camera has been shaking
        float elapsed = 0.0f;

        // Runs this on a loop until the duration is over
        while (elapsed < duration)
        {
            // Picks random positions for the camera based on how big the magnitude is
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            // Sets the cameras position to that new random position
            transform.localPosition = new Vector3(x, y, z);

            // Increases the elapsed variable based on how long has passed
            elapsed += Time.deltaTime;

            // Returns nothing
            yield return null;
        }

        // Sets the camera back to it's original position
        transform.localPosition = originalPos;
    }
}
