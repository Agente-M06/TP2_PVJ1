using UnityEngine;

public class DataPickup : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(scoreValue);
            }
            Destroy(gameObject);
        }
    }
}