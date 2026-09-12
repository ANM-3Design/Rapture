using UnityEngine;

public class MovingTerrain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform PointA;
    public Transform PointB;
    public float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTerrain();
    }

    public void MoveTerrain()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(PointA.position, PointB.position, pingPong);
    }
}
