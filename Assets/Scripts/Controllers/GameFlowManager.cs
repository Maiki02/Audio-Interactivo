using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Para Image

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private GameObject blackImage;  // Debe llevar un componente Image
    private Image _blackImageUI;
    [SerializeField] private AudioSource narratorAudioSource; // Fuente de audio para el narrador

    void Start()
    {
        // Cacheamos el Image y lanzamos la corutina
        _blackImageUI = blackImage.GetComponent<Image>();
        StartCoroutine(InitGameCoroutine());
        //blackImage.SetActive(false); // Desactivamos la pantalla negra al inicio
    }

    private IEnumerator InitGameCoroutine()
    {
        float initialDelay = 1f;
        float fadeDuration = 10f;
        bool dialogueStarted = false;

        AudioController.Instance.PlaySFX(AudioType.Wind);
        AudioController.Instance.PlaySFX(AudioType.Rain);
        
        // 1) Esperamos 1 segundo con la pantalla negra
        yield return new WaitForSeconds(initialDelay);

        // 2) Al segundo 1, iniciamos lluvia y viento

        // 3) Fade out de opacidad de 1 a 0 en fadeDuration segundos
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            _blackImageUI.color = new Color(0f, 0f, 0f, alpha);

            // 4) Al cabo de 3 segundos, lanzamos el diálogo del narrador (solo una vez)
            if (!dialogueStarted && elapsed >= 3f)
            {
                dialogueStarted = true;
                narratorAudioSource.PlayOneShot(narratorAudioSource.clip);
            }

            yield return null;
        }

        // 5) Desactivamos la pantalla negra
        blackImage.SetActive(false);

        // 6) Esperamos 3 segundos más e iniciamos la música de fondo
        yield return new WaitForSeconds(3f);

        AudioController.Instance.PlayMusic(AudioType.BackgroundMusic, true);

        
    }
}

