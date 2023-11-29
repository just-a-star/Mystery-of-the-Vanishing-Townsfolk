using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWritter : MonoBehaviour
{

    public float delay = 0.05f;
    public string fullText;
    string currentText = "";
    public Text textComponent;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<Text>();
        StartCoroutine(ShowText());
    }

    private void Update()
    {
        isPaused = Time.timeScale == 0f;
    }

    IEnumerator ShowText()
    {
        int totalCharacters = fullText.Length;

        for (int i = 0; i <= totalCharacters; i++)
        {
            if (!isPaused)
            {
                currentText = fullText.Substring(0, i);
                textComponent.text = currentText;
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                while (isPaused)
                {
                    yield return null;
                }
            }
        }
    }


}
