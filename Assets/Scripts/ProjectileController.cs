using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isDocked = true;
    public float nudgeForce = 5f;
    CannonController currentCannon; // NEW — track which cannon you're docked in

    void Update()
    {
        if (!isDocked)
        {
            float h = Input.GetAxis("Horizontal");
            rb.AddForce(transform.right * h * nudgeForce);
        }
        if (isDocked && Input.GetButtonDown("Fire1")) Launch();
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

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Obstacle"))
            GameManager.Instance.RespawnAtCheckpoint(this);
    }
}

