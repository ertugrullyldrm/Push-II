using UnityEngine;

public class Left : MonoBehaviour
{

    [SerializeField] private Vector3[] pos;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform leftedObject;

    private int currentPosIndex;
    private bool isOpen;

    private void Awake()
    {
        isOpen = false;
        currentPosIndex = 0;
        leftedObject.position = pos[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isOpen = true;
            currentPosIndex = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isOpen = false;
            currentPosIndex = 0;
        }
    }

    private void Update()
    {
        if (LevelManager.Instance.IsPlaying)
        {
            if (isOpen && transform.position != pos[currentPosIndex])
            {
                leftedObject.position = Vector3.MoveTowards(leftedObject.position, pos[currentPosIndex], moveSpeed * Time.deltaTime);
            }
            else if (!isOpen && transform.position != pos[currentPosIndex])
            {
                leftedObject.position = Vector3.MoveTowards(leftedObject.position, pos[currentPosIndex], moveSpeed * Time.deltaTime);
            }

        }
    }
}
