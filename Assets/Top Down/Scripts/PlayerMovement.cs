using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System;
using System.Collections;


public class playerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; //velocidade que o jogador se move

    private Vector2 _movement;

    private Animator _animator; 
    private Rigidbody2D _rb;


    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 14f; //A velocidade do jogador durante o dash
    [SerializeField] float dashDuration = .5f; // quanto tempo o jogador esta em dash
    [SerializeField] float dashCooldown = 1.5f; // quanto tempo em segundos ate o jogador poder dar outro dash
    public bool isDashing = false, canDash = true, Correndo = false;


    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _Ultimohorizontal = "UltimoHorizontal";
    private const string _Ultimovertical = "UltimoVertical"; 
    //Essas ultimas 4 linhas s�o usadas para anima��o, essas springs entre aspas s�o a maneira que elas s�o escritas no ANIMATOR
    //Elas s�o usadas para o ANIMATOR para saber em que momento usar as anima��es de andar ou ficar parado


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        if (!isDashing) //Enquanto em dash nada mais acontece
        {

            _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

            _rb.linearVelocity = _movement * _moveSpeed;

            _animator.SetFloat(_horizontal, _movement.x); //anima��o do jogador ao andar para direita e para esquerda
            _animator.SetFloat(_vertical, _movement.y); //anima��o do jogador ao andar para cima e para baixo

            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_Ultimovertical, _movement.y);
                _animator.SetFloat(_Ultimohorizontal, _movement.x);
                //detecta a ultima posi��o que o jogador estava se movendo, assim fazendo que ele fique parado nessa posi��o em idle
            }

           if (Input.GetKeyUp(KeyCode.Mouse1) && canDash)
            {
                StartCoroutine(Dash(currentMousePosition));

            }
        }
    }

    private IEnumerator Dash(Vector2 targetPosition)
    {
        isDashing = true;
        canDash = false; //n�o pode dar dash ate o cooldown acabar

        // Calcula a dire��o do dash (do jogador para o mouse)
        Vector2 dashDirection = (targetPosition - (Vector2)transform.position).normalized;

        // Aplica a velocidade na dire��o calculada(posi��o do mouse)
        _rb.linearVelocity = dashDirection * dashSpeed;

        // Aguarda a dura��o do dash
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; //Pode dar dash novamente
    }

}
