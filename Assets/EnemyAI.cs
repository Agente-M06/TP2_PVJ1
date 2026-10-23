using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] patrolPoints;

    [Header("Rangos y Tiempos")]
    [SerializeField] private float detectionRange = 12f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int attackDamage = 20;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float lastAttackTime = -99f;
    private bool isStunned = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) playerTransform = p.transform;
        }

        GoToNextPatrolPoint();
    }

    private void Update()
    {
        if (isStunned || (GameManager.Instance != null && GameManager.Instance.currentState != GameState.Playing))
        {
            if (agent.enabled && agent.hasPath)
                agent.ResetPath();
            return;
        }

        if (playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(playerTransform.position);

            if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                GoToNextPatrolPoint();
            }
        }
    }

    private void AttackPlayer()
    {
        lastAttackTime = Time.time;
        PlayerHealth health = playerTransform.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(attackDamage);
            Debug.Log($"[Centinela] Golpeó al jugador cuerpo a cuerpo (-{attackDamage} HP).");
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    public void Stun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunRoutine(duration));
        }
    }

    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        if (agent.enabled) agent.isStopped = true;
        Debug.Log($"[Centinela] ¡Enemigo aturdido por {duration} segundos!");

        yield return new WaitForSeconds(duration);

        isStunned = false;
        if (agent.enabled) agent.isStopped = false;
        Debug.Log("[Centinela] Enemigo recuperado del aturdimiento.");
    }
}