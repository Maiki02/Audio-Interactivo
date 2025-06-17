
using UnityEngine;

public class Turntable : MonoBehaviour
{
    [SerializeField] private AudioSource turntableAudioSource;

    [Header("Configuración del sonido")]
    [Tooltip("Sonido en 3D")]
    [SerializeField] private float spatialBlend = 1f; // Sonido 3D
    [Tooltip("Si el sonido debe repetirse en bucle")]

    private bool wasRepeated = false;

    void Awake()
    {
        turntableAudioSource = GetComponent<AudioSource>();
        turntableAudioSource.spatialBlend = spatialBlend; // Configura el sonido como 3D
    }

    void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering collision with Turntable");
        if (!wasRepeated && other.gameObject.CompareTag("Player") )
        {
            turntableAudioSource.Play();
            turntableAudioSource.volume = AudioController.Instance.SfxVolume; // Se ajusta el volumen del sonido del tocadiscos al volumen de SFX
            wasRepeated = true; // Marcamos que el sonido ha sido reproducido
        }
    }
    

}
