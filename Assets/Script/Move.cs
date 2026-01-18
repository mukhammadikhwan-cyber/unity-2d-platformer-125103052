using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public float kecepatan = 5f;

    public float kecepatanLompat = 7f;

    public bool balik;

    private int pindah;

    public bool tanah;

    public LayerMask targetLayer;

    public Transform deteksitanah;

    public float jangkauan = 0.2f;

    private Rigidbody2D rb;

    private Animator anim;

    public int nyawa;

    public int koin;

    private SpriteRenderer sr;

    public bool kebal = false;

    public TMP_Text infonyawa;

    public TMP_Text infokoin;

    [SerializeField]
    private GameObject gameOverUI;

    public IEnumerator KebalSebentar()
    {
        kebal = true;

        float durasiKebal = 1f;
        float intervalKedip = 0.1f;

        float timer = 0f;

        while (timer < durasiKebal)
        {
            sr.enabled = !sr.enabled; // toggle sprite
            yield return new WaitForSeconds(intervalKedip);
            timer += intervalKedip;
        }

        sr.enabled = true; // pastikan tampil normal
        kebal = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        infonyawa = GameObject.Find("UINyawa").GetComponent<TMP_Text>();
        infokoin = GameObject.Find("UIKoin").GetComponent<TMP_Text>();
    }

    void Update()
    {
        infonyawa.text = "nyawa : " + nyawa.ToString();
        infokoin.text = "koin : " + koin.ToString();

        // === GROUND CHECK ===
        tanah =
            Physics2D
                .OverlapCircle(deteksitanah.position, jangkauan, targetLayer);

        // === INPUT KIRI KANAN ===
        float arah = 0;

        if (Keyboard.current.dKey.isPressed)
            arah = 1;
        else if (Keyboard.current.aKey.isPressed) arah = -1;

        // GERAK HORIZONTAL (lebih stabil)
        rb.linearVelocity = new Vector2(arah * kecepatan, rb.linearVelocity.y);

        // === LOMPAT ===
        if (Keyboard.current.wKey.wasPressedThisFrame && tanah)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * kecepatanLompat, ForceMode2D.Impulse);
        }

        // === ANIMATOR ===
        anim.SetFloat("Speed", Mathf.Abs(arah));
        anim.SetBool("IsGrounded", tanah);

        // === BALIK BADAN ===
        if (arah > 0 && balik)
            BalikBadan();
        else if (arah < 0 && !balik) BalikBadan();

        if (nyawa <= 0)
        {
            GameOver();
        }
    }

    void BalikBadan()
    {
        balik = !balik;
        Vector3 skala = transform.localScale;
        skala.x *= -1;
        transform.localScale = skala;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER");

        // hentikan gerakan
        rb.linearVelocity = Vector2.zero;

        // matikan player
        gameObject.SetActive(false);

        // tampilkan UI Game Over
        if (gameOverUI != null) gameOverUI.SetActive(true);
    }
}
