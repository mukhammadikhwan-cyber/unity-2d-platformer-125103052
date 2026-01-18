using UnityEngine;

public class rotate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float SpeedX = 0f;

    public float SpeedY = 0f;

    public float SpeedZ = 100f;

    void Update()
    {
        transform
            .Rotate(SpeedX * Time.deltaTime,
            SpeedY * Time.deltaTime,
            SpeedZ * Time.deltaTime);
    }
}
