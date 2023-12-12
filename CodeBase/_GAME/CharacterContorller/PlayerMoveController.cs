using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public CharacterController CharacterController;
    public float Speed = 2f;
    public float RotationSpeed = 300f;
    public float Gravity = 1;

    private Camera _cam;
    private Animator _animator;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _cam = Camera.main;
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector2 input = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1.0f);
        Vector3 a = Quaternion.Euler(0, _cam.transform.eulerAngles.y, 0) * new Vector3(input.x, 0, input.y);

        _moveDirection = new Vector3(a.x * Speed, _moveDirection.y, a.z * Speed);

        if (CharacterController.isGrounded)
        {
            _moveDirection.y = 0;
        }

        _moveDirection.y -= Time.deltaTime * Gravity;

        if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            var diff = hit.point - transform.position;
            diff.Normalize();

            float rot = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, rot, 0), Time.deltaTime * RotationSpeed);
            a = transform.InverseTransformDirection(a);

            CharacterController.Move(_moveDirection * Time.deltaTime);

            _animator.SetFloat("InputMagnitude", a.magnitude, 0.1f, Time.deltaTime);
            _animator.SetFloat("Horizontal", a.x, 0.1f, Time.deltaTime);
            _animator.SetFloat("Vertical", a.z, 0.1f, Time.deltaTime);
        }
    }
}
