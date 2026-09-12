using Unity.VisualScripting;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody rb;
    public bool isDocked = true;

    public float nudgeForce = 5f;

    void Update()
    {
        if (!isDocked)
        {
            float h = Input.GetAxis("Horizontal"); // whatever axis maps to steer
            rb.AddForce(transform.right * h * nudgeForce); // straight ballistic + nudge, no separate InputManager class
        }
        if (isDocked && Input.GetButton("Fire1")) Launch();
     

    }

    public void DockAt(CannonController cannon)
    {
        isDocked = true;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        transform.position = cannon.dockPoint.position;
        rb.isKinematic = true; // physics off while docked
    }

    void Launch()
    {
        

        isDocked = false;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 20f, ForceMode.Impulse); // hardcoded launch force, tune by eye


    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Obstacle"))
            GameManager.Instance.RespawnAtCheckpoint(this);
    }
}

