using UnityEngine;

public class enemy : MonoBehaviour
{
    public int damage = 1;

    public float knockbackForceX = 5f;

    public float knockbackForceY = 4f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Move player = other.GetComponent<Move>();
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (player != null && rb != null && !player.kebal)
            {
                // kurangi nyawa
                player.nyawa -= damage;

                // arah knockback
                float arah =
                    other.transform.position.x < transform.position.x ? -1 : 1;

                // reset velocity lalu dorong
                rb.linearVelocity = Vector2.zero;
                rb
                    .AddForce(new Vector2(arah * knockbackForceX,
                        knockbackForceY),
                    ForceMode2D.Impulse);

                // aktifkan kebal sementara
                player.StartCoroutine(player.KebalSebentar());
            }
        }
    }
}
