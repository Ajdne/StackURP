using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour, IMovement
{
    [SerializeField] private Joystick joystick;
    public Joystick Joystick { get { return joystick; } }

    public CharacterController player;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerCollision collisionScript;

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] private float rotateSpeed;
    public float RotateSpeed { get { return rotateSpeed; } }

    private Vector3 respawnPosition;
    public Vector3 RespawnPosition { get { return respawnPosition; } set { respawnPosition = value; } }

    private void Start()
    {
        player = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {          
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);

            animator.SetBool("Run", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
        }
        
        player.SimpleMove(moveDirection.normalized * moveSpeed);
    }

    private void OnEnable()
    {
        player.enabled = true;
    }

    private void OnDisable()
    {
        player.enabled = false;
    }

    public void ActivateMovement()
    {
        this.enabled = true;
        collisionScript.CanCollide = true;
    }

    public void DeactivateMovement()
    {
        collisionScript.CanCollide = false;
        this.enabled = false;
    }

    public void SetMovementSpeed(float value)
    {
        moveSpeed = value;
    }

    public void CollisionFall()
    {
        DeactivateMovement();
    }

    public void IncreaseMovementSpeed(float value)
    {
        moveSpeed += value;
    }

    public void SetPlayerRespawnPosition(Vector3 resPos)
    {
        respawnPosition = resPos;
    }

    public IEnumerator RespawnPlayer()
    {
        // remove leftover stacks from player
        GetComponent<IStacking>().RemoveAllStacks();

        yield return new WaitForSeconds(0.4f);

        // need to disable movement script in order to move him
        DeactivateMovement();

       

        transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.position = RespawnPosition;

        if (GameManager.Instance.IsEndGame)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Dance", true);

            // dont activate player movement
            //Player.GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else ActivateMovement();

    }
}
