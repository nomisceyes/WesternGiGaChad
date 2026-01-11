using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }

    public void WarpTo(Vector3 position)
    {
        _agent.Warp(position);
    }
}