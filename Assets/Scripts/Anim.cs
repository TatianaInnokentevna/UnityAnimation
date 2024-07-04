using UnityEngine;


public class Anim : MonoBehaviour
{
    private Animator _anim;

    private Rigidbody _rb;
    private float horSpeed = 5f;
    private float Speed = 15f;
    


    void Start()
    {
        _anim = GetComponent<Animator>(); 
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
     

    }

    void FixedUpdate()
    {
        float horMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");
        _rb.MovePosition(transform.position + transform.TransformDirection(new Vector3(0, 0, vertMove) * horSpeed * Time.fixedDeltaTime));
        transform.rotation *= Quaternion.Euler(0, horMove * Speed * Time.deltaTime, 0);

        if (vertMove != 0.0f)
            _anim.SetBool("isWalking", true);
        else
            _anim.SetBool("isWalking", false);
    }
}
