using UnityEngine;

public class StunWeapon : MonoBehaviour
{
    [SerializeField] private float range = 20f;
    [SerializeField] private float stunDuration = 4f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            ShootStunDart();
        }
    }

    private void ShootStunDart()
    {
        if (playerCamera == null) return;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, enemyLayer))
        {
            EnemyAI enemy = hit.collider.GetComponentInParent<EnemyAI>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
                Debug.DrawLine(ray.origin, hit.point, Color.cyan, 1f);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * range, Color.blue, 0.5f);
        }
    }
}