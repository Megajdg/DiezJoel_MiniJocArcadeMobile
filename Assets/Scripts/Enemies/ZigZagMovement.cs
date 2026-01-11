using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    public float amplitude = 2f;
    public float frequency = 2f;

    private float startX;

    void OnEnable()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        float x = startX + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
