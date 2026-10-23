using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MudZone : MonoBehaviour
{
    [Range(0.1f, 1f)]
    [SerializeField] private float speedMultiplier = 0.4f;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.SetSpeedModifier(speedMultiplier);
                Debug.Log("[Zona de Barro] Entraste al lodazal: Velocidad reducida al 40%.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.SetSpeedModifier(1f);
                Debug.Log("[Zona de Barro] Saliste del lodazal: Velocidad restablecida.");
            }
        }
    }
}