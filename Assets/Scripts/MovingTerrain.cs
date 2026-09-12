using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform PointA;
    public Transform PointB;
    public Transform TheTerrain;
    public float speed;
    public float rotationSpeed;
    public float amplitude = 15f; // max degrees of sway either direction
    Quaternion baseRotation;
    float seedX, seedY, seedZ;
    void Start()
    {
        baseRotation = TheTerrain.localRotation;
        // Randomized offsets so multiple objects with this script don't sync up and sway in unison
        seedX = Random.Range(0f, 100f);
        seedY = Random.Range(0f, 100f);
        seedZ = Random.Range(0f, 100f);

    }

    // Update is called once per frame
    void Update()
    {
        MoveTerrain();
        RandomRotation();
    }

    public void MoveTerrain()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(PointA.position, PointB.position, pingPong);
    }

    public void RandomRotation()
    {
  
            float z = (Mathf.PerlinNoise(seedZ, Time.time * speed) - 0.5f) * 2f * amplitude;
            float x = (Mathf.PerlinNoise(seedX, Time.time * speed) - 0.5f) * 2f * amplitude;
            float y = (Mathf.PerlinNoise(seedY, Time.time * speed) - 0.5f) * 2f * amplitude;
            TheTerrain.localRotation = baseRotation * Quaternion.Euler(x, y, z);
    }
}
