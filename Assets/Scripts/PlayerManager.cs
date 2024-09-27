using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public bool IsSelected;

    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject effect;

    [SerializeField] private VirtualJoystick moveJoystick;

    [SerializeField] private AudioClip[] sounds;

    private float maxSpeed = 5f;
    private Rigidbody myRB;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        myRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < -2)
        {//when player fall
            GetComponent<AudioSource>().clip = sounds[0];
            GetComponent<AudioSource>().Play();
            transform.position = startPos;
        }
    }

    private void FixedUpdate()
    {
        if (LevelManager.Instance.IsPlaying)
        {
            if (IsSelected)
            {
                Vector3 movement;
                if (moveJoystick.InputDirection != Vector3.zero && myRB.velocity.magnitude < maxSpeed)
                {
                    movement = moveJoystick.InputDirection;
                    myRB.AddForce(movement * moveSpeed);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        /*if (other.transform.tag.Equals("Enemy"))
        {
            GameObject o = Instantiate(effect, new Vector3(transform.position.x, effect.transform.position.y, transform.position.z),
                effect.transform.rotation) as GameObject;
            GetComponent<AudioSource>().clip = sounds[1];
            GetComponent<AudioSource>().Play();
            Destroy(o, 0.6f);
            transform.position = startPos;
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            effect.SetActive(true);
            effect.transform.position = new Vector3(transform.position.x, effect.transform.position.y, transform.position.z);
            effect.GetComponent<ParticleSystem>().Play();
            
            GetComponent<AudioSource>().clip = sounds[1];
            GetComponent<AudioSource>().Play();
            transform.position = startPos;
        }
    }

    private void OnMouseDown()
    {
        if (!IsSelected)
        {
            LevelManager.Instance.DisableAllPlayerSelection(gameObject);
            IsSelected = true;
            LevelManager.Instance.ChangeCameraTarget(transform);
        }
    }
}
