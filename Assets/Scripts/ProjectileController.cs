using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Transform art;
    public Rigidbody rb;
    public bool isDocked = true;
    public float nudgeForce = 5f;
    public GameObject Camtracker;
    public CannonController currentCannon; // NEW — track which cannon you're docked in
    public float speed;
    public float amplitude = 15f; // max degrees of sway either direction
    Quaternion baseRotation;
    float seedX, seedY, seedZ;

    void Start()
    {
        baseRotation = art.localRotation;
        // Randomized offsets so multiple objects with this script don't sync up and sway in unison
        seedX = Random.Range(0f, 100f);
        seedY = Random.Range(0f, 100f);
        seedZ = Random.Range(0f, 100f);

    }

    void Update()
    {
        if (!isDocked)
        {
            float h = Input.GetAxis("Horizontal");
            rb.AddForce(transform.right * h * nudgeForce);
        }
        if (isDocked && Input.GetButtonDown("Fire1"))
        {
            Launch();
            AudioManager.Instance.PlayFire();
        }
        
        RandomRotation();
    }

    void Launch()
    {
        currentCannon?.ReleaseCooldown();
        isDocked = false;
        rb.isKinematic = false;
        Vector3 fireDir = currentCannon.pivot.up;
        rb.AddForce(fireDir * 20f, ForceMode.Impulse);
        CameraJuice.Instance.OnFire(); // CHANGED
    }

    public void DockAt(CannonController cannon)
    {
        currentCannon = cannon;
        isDocked = true;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        transform.position = cannon.dockPoint.position;
        rb.isKinematic = true;
        CameraJuice.Instance.OnDock(); // CHANGED
       
    }

    public void RandomRotation()
    {

        float z = (Mathf.PerlinNoise(seedZ, Time.time * speed) - 0.5f) * 2f * amplitude;
        float x = (Mathf.PerlinNoise(seedX, Time.time * speed) - 0.5f) * 2f * amplitude;
        float y = (Mathf.PerlinNoise(seedY, Time.time * speed) - 0.5f) * 2f * amplitude;
        art.localRotation = baseRotation * Quaternion.Euler(x, y, z);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Obstacle"))
            GameManager.Instance.RespawnAtCheckpoint(this);
    }
}

