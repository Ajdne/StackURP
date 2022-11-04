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

    public Transform camera;

    private Vector3 forwardOffset;
    private Vector3 rightOffset;

    private bool finalPlatform;

    [SerializeField] private GameObject playerHead;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        
        CalculateCameraOffset();
    }

    private void Update()
    {
        //apply the current camera's offset to the joystick input
        Vector3 forwardRelativeDirection = joystick.Vertical * forwardOffset;
        Vector3 rightRelativeDirection = joystick.Horizontal * rightOffset;

        Vector3 moveDirectionRaw = forwardRelativeDirection + rightRelativeDirection; //combine the player's input with the offset
        Vector3 moveDirection = new Vector3(moveDirectionRaw.x, 0f, moveDirectionRaw.z); //calculate the normalized movement direction by blocking the movement on the Y-axis

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
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

    public void CalculateCameraOffset()
    {
        forwardOffset = camera.forward;
        rightOffset = camera.right;
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
            UIManager.Instance.LevelComplete();
        }
       else if(!finalPlatform) ActivateMovement();

    }

    public void ReachFinish(bool reachFinish)
    {
        finalPlatform = reachFinish;
    }

    public GameObject GetPlayerHead()
    {
        return playerHead;
    }
}
