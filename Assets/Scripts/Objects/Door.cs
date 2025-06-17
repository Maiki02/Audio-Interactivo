
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door: MonoBehaviour
{
    [SerializeField] private GameObject canvasUI;

    [Header("Configuración de la animaación de la puerta")]
    [SerializeField] private Animator doorAnimator;

    [Header("Tipo de acción sobre la puerta")]
    [SerializeField] private TypeDoorInteract type = TypeDoorInteract.None;
    
    private GameObject player;
    private bool isPlayerNearby = false;
    private bool isDoorOpen = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("No se encontró el objeto Player en la escena.");
        }
    }

    private void Update()
    {
        // Si el jugador está cerca y aún no abrimos, al pulsar E ejecutamos la acción:
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isDoorOpen = !isDoorOpen; // Cambiamos el estado de la puerta

            if (type == TypeDoorInteract.OpenAndClose && doorAnimator != null)
            {
                Debug.Log("Cambiando estado de la puerta: " + (isDoorOpen ? "Abriendo" : "Cerrando"));
                doorAnimator.SetBool("isOpen", isDoorOpen); // Activamos la animación de abrir/cerrar
                //AudioController.Instance.PlaySFX(isDoorOpen ? AudioType.DoorOpen : AudioType.DoorClose);
            }

        }
    }   

    // Cuando el Player entra en el trigger de la puerta:
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entrando al trigger de la puerta");
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            this.canvasUI.SetActive(true);
        }
    }

    // Cuando el Player sale:
    private void OnTriggerExit(Collider other)
    {
        
        Debug.Log("Player saliendo del trigger de la puerta");
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            this.canvasUI.SetActive(false); // Desactivamos el canvas UI si está activo
        }
    }


    // Para que se vea en la escena el área de interacción:
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        Collider c = GetComponent<Collider>();
        if (c is BoxCollider box)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.center, box.size);
        }
    }
}
