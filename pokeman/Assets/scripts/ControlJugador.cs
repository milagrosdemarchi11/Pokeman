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

    public bool tieneRayo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //rayoScript = GetComponent<ShotPlayer>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;

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

    // ⚡ PODER DEL RAYO
    public void ActivarRayo()
    {
        if (!tieneRayo)
        {
            StartCoroutine(RayoPorTiempo());
        }
    }

   public void RecibeDaño (int cantDaño)
   {
     if (!recibiendoDaño)
     { 
        recibiendoDaño = true;
        vida -= cantDaño;

        if (vida<=0)
        { 
            Debug.Log("Jugador muerto");
            muerto=true;
        }  

        //Vector2 rebote = new Vector2 (transform.position.x - direccion.x, 0.2f).normalized;
       // rb.Addforce(rebote* fuerzaRenote, ForceMode2D.Impulse);
     }

    }

    

    IEnumerator RayoPorTiempo()
    {
        tieneRayo = true;
        Debug.Log("Rayo activado");

        //rayoScript.enabled = true;

        // acá podés activar efectos (partículas, animación, etc)

        yield return new WaitForSeconds(10f);

        tieneRayo = false;
        Debug.Log("Rayo desactivado");

        //rayoScript.enabled = false;

        // acá desactivás el efecto
    }
}