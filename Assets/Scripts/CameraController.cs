using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Target;                    //what camera follow
    public float Smoothing;                     //dampening effect

    private Vector3 offset;                     //the difference between target and camera
    private float lowY;                         //the lowest y postion to follow (for jump)

    [SerializeField] private bool lockYTransform;

    private void Start()
    {
        if (Target == null)
        {
            Debug.LogError("Missing target!");
            return;
        }

        offset = transform.position - Target.position;
        lowY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (Target == null)
        {
            Debug.LogError("Missing target!");
            return;
        }

        Vector3 targetCamPos = Target.position + offset;

        //follow the target
        transform.position = Vector3.Lerp(transform.position, targetCamPos, Smoothing * Time.deltaTime);

        if (transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}
