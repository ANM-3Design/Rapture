using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform dockPoint;
    public Animator anim; // has an int parameter "failCount"
    bool isLoaded;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            isLoaded = true;
            GameManager.Instance.lastCannon = this;
            other.GetComponent<ProjectileController>().DockAt(this);
        }
    }

    public void UpdateVisual(int failCount) => anim.SetInteger("failCount", failCount);
    // Cheat: let the Animator Controller's own transition thresholds decide what
    // sprite/pose shows at failCount 3 vs 6 — no C# lookup table, no ScriptableObject.
}

