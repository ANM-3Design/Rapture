using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CannonController lastCannon;
    public int failCount = 0;
    public CannonController[] allCannons; // dragged in Inspector, or FindObjectsOfType at Start

    void Awake() => Instance = this;

    public void RespawnAtCheckpoint(ProjectileController p)
    {
        failCount++;
        foreach (var c in allCannons) c.UpdateVisual(failCount); // loop every cannon, every time — fine at this scale
        p.DockAt(lastCannon);
    }

    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

