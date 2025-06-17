using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private float maxDistance = 10f;
    private IInteractable currentInteractable;
    public Camera cameraChildren;

    void Awake()
    {
        cameraChildren = GetComponentInChildren<Camera>();
        if (cameraChildren == null)
        {
            Debug.LogError("Camera transform not found in PlayerInteractor.");
        }
    }

    void Update()
    {
        // Lanzar rayo desde el centro de la pantalla
        Ray ray = cameraChildren.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                // Si es distinto al anterior, actualizar hover
                if (interactable != currentInteractable)
                {
                    currentInteractable?.OnHoverExit();
                    currentInteractable = interactable;
                    currentInteractable.OnHoverEnter();
                }

                // Interactuar al presionar E
                if (Input.GetKeyDown(KeyCode.E))
                    currentInteractable.OnInteract();

                return;
            }
        }

        // Si no hay ningún interactable bajo el rayo, desactivar outline
        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }
    }
}
