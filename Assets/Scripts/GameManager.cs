using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CannonController lastCannon;
    public int failCount = 0;
    public CannonController[] allCannons;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // NEW — stops a second GameManager appearing if MainLevel reloads
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // NEW
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded; // NEW
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded; // NEW

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SCN_MainScene")
        {
            allCannons = FindObjectsByType<CannonController>(FindObjectsSortMode.None); // CHANGED
            lastCannon = allCannons.Length > 0 ? allCannons[0] : null;
            failCount = 0;
            foreach (var c in allCannons) c.UpdateVisual(0);

            var vcam = FindFirstObjectByType<CinemachineCamera>(); // CM2: CinemachineVirtualCamera
            var projectile = FindFirstObjectByType<ProjectileController>();
            if (vcam != null && projectile != null)
                vcam.Follow = projectile.transform;
        }
    }

    public void RespawnAtCheckpoint(ProjectileController p)
    {
        AudioManager.Instance.PlayFail();
        failCount++;
        foreach (var c in allCannons) c.UpdateVisual(failCount);
        p.DockAt(lastCannon);
    }

    public void RestartGame() => SceneManager.LoadScene("SCN_MainScene"); // no manual reset needed — OnSceneLoaded above handles it

    public void TriggerEnding(string sceneName) => SceneManager.LoadScene(sceneName); // NEW

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // lets you test the button while in Play Mode
#else
        Application.Quit(); // actually closes the game in a built executable
#endif

    }
}
