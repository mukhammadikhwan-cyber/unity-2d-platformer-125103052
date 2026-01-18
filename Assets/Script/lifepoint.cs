using UnityEngine;

public class lifepoint : MonoBehaviour
{
    Move KomponenGerak;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KomponenGerak = GameObject.Find("player").GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            KomponenGerak.koin++;
            KomponenGerak.nyawa++;
            Destroy (gameObject);
        }
    }
}
