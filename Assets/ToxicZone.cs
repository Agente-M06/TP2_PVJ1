using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ToxicZone : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 10;
    [SerializeField] private float tickInterval = 1.5f;

    private float timer = 0f;
    private bool playerInside = false;
    private PlayerHealth targetHealth;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            targetHealth = other.GetComponent<PlayerHealth>();
            timer = 0f;
            Debug.Log("[Gas Tóxico] ¡Peligro! Entraste a la zona de gas.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            targetHealth = null;
            Debug.Log("[Gas Tóxico] Saliste del gas tóxico.");
        }
    }

    private void Update()
    {
        if (!playerInside || targetHealth == null) return;
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing) return;

        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            timer = 0f;
            targetHealth.TakeDamage(damagePerTick);
            Debug.Log($"[Gas Tóxico] Daño por inhalación (-{damagePerTick} HP).");
        }
    }
}