using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Vector3[] patrolsPos;
    [SerializeField] private float moveSpeed;
    private int currentPatrolIndex;

    private void Start()
    {
        currentPatrolIndex = 0;
        transform.position = patrolsPos[currentPatrolIndex];
    }

    private void Update()
    {
        if (LevelManager.Instance.IsPlaying)
        {
            if (transform.position == patrolsPos[currentPatrolIndex])
            {
                currentPatrolIndex++;
                currentPatrolIndex = currentPatrolIndex % patrolsPos.Length;
            }
            transform.position = Vector3.MoveTowards(transform.position, patrolsPos[currentPatrolIndex], moveSpeed * Time.deltaTime);
        }
    }
}
