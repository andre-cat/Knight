using System.Collections;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private IEnumerator corrutina;
    private Rigidbody2D cuerpo;
    public Animator animador;
    public float vel = 0.25f;
    public float empuje = 2.2f;
    public bool salto = false;
    public bool derecha = true;
    public float smoothness = 1;
    public float times = 3;

    // Start is called before the first frame update
    void Start()
    {
        
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Animaciones
        atack();
        walk();
        jump();
        roll();

        // Físicas
        xMove();
        yMove();
        zRotation();
    }

    void atack()
    {
        bool ataque = Input.GetKeyDown("space");
        animador.SetBool("ataque", ataque);
    }

    void walk()
    { animador.SetFloat("velocidad", Mathf.Abs(Input.GetAxisRaw("Horizontal"))); }

    void jump()
    {
        if (Input.GetKeyDown("up") == true)
        { salto = true; }
        else if (Mathf.Abs(cuerpo.velocity.y) <= 0.01)
        { salto = false; }
        animador.SetBool("salto", salto);
    }

    void roll()
    {
        if (Mathf.Abs(cuerpo.velocity.y) <= 0.01)
        {
            Input.GetKeyDown("down");
            if (Input.GetKeyDown("down") == true)
            {
                Vector3 nueva;
                Vector3 transition;
                float x = vel;

                if (derecha == false)
                { x = -x; }

                for (int i = 0; i < times; i++)
                {
                    nueva = new Vector3(transform.position.x + x, transform.position.y, transform.position.z);
                    transition = Vector3.Lerp(transform.position, nueva, smoothness * Time.fixedDeltaTime);
                    transform.position = transition;
                    animador.SetBool("rodar", Input.GetKeyDown("down"));
                }
            }
        }
    }

    void xMove()
    {
        float x = Input.GetAxisRaw("Horizontal") * vel;
        transform.position += new Vector3(x, 0, 0);
    }

    void yMove()
    {
        if (Input.GetKeyDown("up") && Mathf.Abs(cuerpo.velocity.y) < 0.01)
        { cuerpo.AddForce(new Vector2(0, empuje), ForceMode2D.Impulse); }
    }


    void zRotation()
    {
        if (Input.GetKeyDown("right"))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            derecha = true;
        }
        if (Input.GetKeyDown("left"))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            derecha = false;
        }
    }

    private IEnumerator esperar(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}


