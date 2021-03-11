using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [Range(0, 20)] public float speed = 1;
    public eSpace space = eSpace.Object;
    public eMovement movement = eMovement.Tank;
    public float turnRate = 3;
    //public float lives = 3;

    public enum eSpace
    {
        World,
        Camera,
        Object
    }

    public enum eMovement
    {
        Free,
        Tank,
        Strafe
    }

    CharacterController characterController;
    Rigidbody rb;

    bool useSession = false;
    public bool isDead { get; set; } = false;
    Vector3 inputDirection = Vector3.forward;
    Vector3 velocity = Vector3.zero;
    Transform cameraTransform;

    private void Start()
    {
        velocity *= speed;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        useSession = (GameSession.Instance != null);
    }

    void Update()
    {
        Quaternion orientation = Quaternion.identity;
        switch (space)
        {
            case eSpace.World:
                break;
            case eSpace.Camera:
                Vector3 forward = cameraTransform.forward;
                forward.y = 0;
                orientation = Quaternion.LookRotation(forward);
                break;
            case eSpace.Object:
                orientation = transform.rotation;
                break;
            default:
                break;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, (-90 * turnRate * Time.deltaTime));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, (90 * turnRate * Time.deltaTime));
        }

        Vector3 direction = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        switch (movement)
        {
            case eMovement.Free:
                direction = orientation * inputDirection;
                rotation = (direction.sqrMagnitude > 0) ? Quaternion.LookRotation(direction) : transform.rotation;
                break;
            case eMovement.Tank:
                direction.z = inputDirection.z;
                direction = orientation * direction;

                rotation = orientation * Quaternion.AngleAxis(inputDirection.x, Vector3.up);
                break;
            case eMovement.Strafe:
                direction = orientation * inputDirection;
                rotation = Quaternion.LookRotation(orientation * Vector3.forward);
                break;
            default:
                break;
        }

        // ***

        characterController.Move(direction * speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turnRate * Time.deltaTime);

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        inputDirection = Vector3.zero;
        velocity = Vector3.zero;

        Debug.Log("Death");
        isDead = true;
        //lives--;
    }


    /*public void OnMove(InputValue input)
    {
        if (!animator.GetBool("Death") && GameSession.Instance.State == GameSession.eState.Session)// && Game.Instance.State == Game.eState.Game)
        {
            Vector2 v2 = input.Get<Vector2>();
            inputDirection = Vector3.zero;
            inputDirection.x = v2.x;
            inputDirection.z = v2.y;
        }
    }*/
}
