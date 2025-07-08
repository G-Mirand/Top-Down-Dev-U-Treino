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
    //Essas ultimas 4 linhas são usadas para animação, essas springs entre aspas são a maneira que elas são escritas no ANIMATOR
    //Elas são usadas para o ANIMATOR para saber em que momento usar as animações de andar ou ficar parado


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

            _animator.SetFloat(_horizontal, _movement.x); //animação do jogador ao andar para direita e para esquerda
            _animator.SetFloat(_vertical, _movement.y); //animação do jogador ao andar para cima e para baixo

            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_Ultimovertical, _movement.y);
                _animator.SetFloat(_Ultimohorizontal, _movement.x);
                //detecta a ultima posição que o jogador estava se movendo, assim fazendo que ele fique parado nessa posição em idle
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
        canDash = false; //não pode dar dash ate o cooldown acabar

        // Calcula a direção do dash (do jogador para o mouse)
        Vector2 dashDirection = (targetPosition - (Vector2)transform.position).normalized;

        // Aplica a velocidade na direção calculada(posição do mouse)
        _rb.linearVelocity = dashDirection * dashSpeed;

        // Aguarda a duração do dash
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true; //Pode dar dash novamente
    }

}
