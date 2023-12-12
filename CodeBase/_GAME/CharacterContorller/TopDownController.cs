using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public float speed = 5f;
    public Transform modelTransform;
    public Animator animator;

    private CharacterController controller;
    private Camera _camera;
    private Vector3 moveDirection;
    private Vector3 lookDirection;

    float _blendXLerp = 0f;
    float _blendYLerp = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        _camera = Camera.main;
        lookDirection = Vector3.forward;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Player1_Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Добавляем новый код
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.transform.position.y - modelTransform.position.y;
        Vector3 lookPos = _camera.ScreenToWorldPoint(mousePos);
        lookDirection = lookPos - modelTransform.position;
        lookDirection.y = 0f;
        modelTransform.rotation = Quaternion.LookRotation(lookDirection);

        _blendXLerp = Mathf.Lerp(_blendXLerp, Vector3.Dot(moveDirection, modelTransform.right), Time.deltaTime * 5);
        _blendYLerp = Mathf.Lerp(_blendYLerp, Vector3.Dot(moveDirection, modelTransform.forward), Time.deltaTime * 5);

        animator.SetFloat("Horizontal", _blendXLerp);
        animator.SetFloat("Vertical", _blendYLerp);

        moveDirection.y = Physics.gravity.y;

        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
