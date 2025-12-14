using System.Collections;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI errorText; // keep disabled in hierarchy when not playing

    public void Toggle(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

   
    public IEnumerator DisplayError(string message)
    {
        if (errorText.IsActive())
            yield break; // Exit if an error message is already being displayed

        errorText.text = message;
        errorText.gameObject.SetActive(true);

        // Show for 5 seconds
        yield return new WaitForSeconds(1f);

        // Fade out over 1 second
        float fadeDuration = 1f;
        float elapsed = 0f;
        Color originalColor = errorText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            errorText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        errorText.gameObject.SetActive(false);
        errorText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f); // Reset alpha
    }


}
