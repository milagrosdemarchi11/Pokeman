using System.Collections;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float velocidad = 5f;
    private Rigidbody2D rb;

    private float moveX;
    private float moveY;
    private Vector2 entrada;

    public Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool recibiendoDaño;
    public bool muerto = false;
    public int vida = 1;

    //private ShotPlayer rayoScript;

    public bool tienePoder = false;
    private Coroutine parpadeoCoroutine;


    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicaNormal;
    [SerializeField] private AudioClip musicaPoder;

    private Coroutine musicaCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //rayoScript = GetComponent<ShotPlayer>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        musicSource.volume = 1f;
        musicSource.clip = musicaNormal;
        musicSource.Play();

        //rayoScript.enabled = false; // empieza apagado
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0)
        {
            entrada = new Vector2(moveX, 0);
        }
        else if (moveY != 0)
        {
            entrada = new Vector2(0, moveY);
        }
        else
        {
            entrada = Vector2.zero;
        }

        Move();
        UpdateAnimations();


    }

    private void Move()
    {
        if (Time.timeScale == 1f)
        {
            animator.SetFloat("MoveX", entrada.x);
            animator.SetFloat("MoveY", entrada.y);
        }
    }

    private void UpdateAnimations()
    {
        if (entrada.x > 0)
            spriteRenderer.flipX = false;
        else if (entrada.x < 0)
            spriteRenderer.flipX = true;

        if (entrada.y > 0)
            spriteRenderer.flipY = false;
        else if (entrada.y < 0)
            spriteRenderer.flipY = true;

        if (moveY == 0)
            spriteRenderer.flipY = false;

        if (entrada.magnitude > 0.1f)
            animator.speed = 1f;
        else
            animator.speed = 0f;
    }

    public Vector2 ObtenerDireccion()
    {
        if (entrada != Vector2.zero)
            return entrada;

        return new Vector2(1, 0);
    }

    void FixedUpdate()
    {
        rb.velocity = entrada * velocidad;
        rb.rotation = 0f;
        rb.angularVelocity = 0f;
    }

    //PODER DEL RAYO
    public void ActivarPoder()
    {
        if (!tienePoder)
        {
            StartCoroutine(PoderPorTiempo());
        }
    }

    public void RecibeDaño(int cantDaño)
    {
        if (!recibiendoDaño)
        {
            recibiendoDaño = true;
            vida -= cantDaño;

            if (vida <= 0)
            {
                Debug.Log("Jugador muerto");
                muerto = true;
            }

            //Vector2 rebote = new Vector2 (transform.position.x - direccion.x, 0.2f).normalized;
            // rb.Addforce(rebote* fuerzaRenote, ForceMode2D.Impulse);
        }

    }



    IEnumerator PoderPorTiempo()
    {
        tienePoder = true;
        //Debug.Log("Rayo activado");

        if (musicaCoroutine != null)
            StopCoroutine(musicaCoroutine);

        musicaCoroutine = StartCoroutine(CambiarMusica(musicaPoder));

        // arrancar parpadeo
        parpadeoCoroutine = StartCoroutine(Parpadear());

        //rayoScript.enabled = true;

        // acá podés activar efectos (partículas, animación, etc)

        yield return new WaitForSeconds(3f);

        tienePoder = false;
        //Debug.Log("Rayo desactivado");

        if (musicaCoroutine != null)
            StopCoroutine(musicaCoroutine);

        musicaCoroutine = StartCoroutine(CambiarMusica(musicaNormal));

        // detener parpadeo
        if (parpadeoCoroutine != null)
            StopCoroutine(parpadeoCoroutine);

        spriteRenderer.color = Color.white;

        //rayoScript.enabled = false;

        // acá desactivás el efecto
    }

    IEnumerator Parpadear()
    {
        while (true)
        {
            //spriteRenderer.color = Color.yellow; // podés cambiar color
            spriteRenderer.color = new Color(1f, 1f, 0f, 0.5f);
            yield return new WaitForSeconds(0.2f);

            //spriteRenderer.color = Color.white;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator CambiarMusica(AudioClip nueva)
    {
        // fade out
        for (float v = 1; v > 0; v -= Time.deltaTime * 2)
        {
            musicSource.volume = v;
            yield return null;
        }

        musicSource.clip = nueva;
        musicSource.Play();

        // fade in
        for (float v = 0; v < 1; v += Time.deltaTime * 2)
        {
            musicSource.volume = v;
            yield return null;
        }
    }
}