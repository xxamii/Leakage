using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float y = Random.Range(-1f, 1f);
            float x = Random.Range(-1f, 1f);
            Vector3 shakeFactor = new Vector3(x, y, originalPosition.z) * magnitude;

            transform.localPosition = originalPosition + shakeFactor;

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
