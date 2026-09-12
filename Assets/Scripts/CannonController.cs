using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour
{
    public Transform dockPoint;
    public Transform pivot;       // NEW — empty object, drag in the Inspector
    public Animator anim;
    public float rotateSpeed = 90f;   // degrees per second
    public float minAngle = 80f, maxAngle = -80f; // clamp so it can't aim backward — set to -180/180 for full rotation
    bool isLoaded;
    bool ignoreDocking;
    float currentAngle = 0f;

    void Update()
    {
        if (isLoaded)
        {
            float h = Input.GetAxis("Horizontal"); // same axis the projectile uses for nudge-steer, no conflict since only one is active at a time
            currentAngle -= h * rotateSpeed * Time.deltaTime;
            currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
            pivot.localRotation = Quaternion.Euler(0, 0, currentAngle); // rotating around Z since that's your locked depth axis

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ignoreDocking) return;
        if (other.CompareTag("Projectile"))
        {
            isLoaded = true;
            GameManager.Instance.lastCannon = this;
            other.GetComponent<ProjectileController>().DockAt(this);
        }
    }

    public void ReleaseCooldown()
    {
        isLoaded = false;
        StartCoroutine(IgnoreDockingBriefly());
    }

    IEnumerator IgnoreDockingBriefly()
    {
        ignoreDocking = true;
        yield return new WaitForSeconds(0.3f);
        ignoreDocking = false;
    }

    public void UpdateVisual(int failCount) => anim.SetInteger("failCount", failCount);
}

