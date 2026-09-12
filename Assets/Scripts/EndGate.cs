using UnityEngine;

public class EndGate : MonoBehaviour
{
    public string endingSceneName = "SCN_MainScene";
    public Collider Col;
    bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (other.CompareTag("Projectile"))
        {
            triggered = true;
            GameManager.Instance.TriggerEnding(endingSceneName);
        }
    }
}
