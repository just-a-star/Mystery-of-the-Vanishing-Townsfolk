using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    public float shakePower = .2f;
    public float shakeDurasi = .5f;

    // Start is called before the first frame update
    void Start()
    {
        ShakeCamera();
    }

    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < shakeDurasi)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakePower;
            transform.position = originalPosition + shakeOffset;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;

    }
}
