using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrust = 1000f, rotateSpeed = 150f;
    [SerializeField] AudioClip thrusters;
    [SerializeField] ParticleSystem thruster, rightThrust, leftThrust;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }
    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(thrusters);
            if (!thruster.isPlaying)
                thruster.Play();
        }
        else
        {
            audioSource.Stop();
            thruster.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
            rb.AddRelativeForce(Vector3.left);
            if (!thruster.isPlaying)
                rightThrust.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
            rb.AddRelativeForce(Vector3.right);
            if (!thruster.isPlaying)
                leftThrust.Play();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.down);
        }
        else
        {
            rightThrust.Stop();
            leftThrust.Stop();
        }
    }
    void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
