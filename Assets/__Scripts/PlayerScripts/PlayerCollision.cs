using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private IStacking stackingScript;
    private IMovement movementScript;
    
    private bool canCollide = true;
    public bool CanCollide { get { return canCollide; } set { canCollide = value; } }

    [SerializeField] private Animator animator;

    [Header("Audio Settings"), Space(10f)]
    [SerializeField] private AudioClip uffClip;
    [SerializeField] private AudioSource audioSource;

    [Header("Emoji Settings"), Space(10f)]
    [SerializeField] private Animator emojiAnimator;
    [SerializeField] private List<GameObject> loseEmojis = new List<GameObject>();

    private void Start()
    {
        stackingScript = GetComponent<IStacking>();
        movementScript = GetComponent<IMovement>();

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canCollide)
        {
            if (stackingScript.GetStackCount() < other.gameObject.GetComponent<IStacking>().GetStackCount())
            {
                // play animation
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);
                //animator.SetBool("Collision", true);

                animator.Play("Player Collision");

                // reset audio source pitch - need this for blue player
                // stacking script changes the pitch
                audioSource.pitch = 1;
                // play audio clip once
                audioSource.PlayOneShot(uffClip);

                // activate emoji
                //emojis[0].SetActive(true);

                int randomEmoji = Random.Range(0, loseEmojis.Count);
                CanvasLookAt.currentEmoji = loseEmojis[randomEmoji];

                // play emoji animation
                emojiAnimator.Play("EmojiAnimation");

                // lose stacks
                stackingScript.LoseStacks();
                //stackingScript.RemoveAllStacks();

                // stop the player from moving during animation
                movementScript.DeactivateMovement();

                // activate movement again in the animation


                StartCoroutine(DeactivateAnimation());
            }
            //else
            //{
            //    int randomChance = Random.Range(0, 101);

            //    if(randomChance > 30)
            //    {
            //        // play emoji animation
            //        emojiAnimator.Play("EmojiAnimation");
            //    }
            //}
        }
    }

    IEnumerator DeactivateAnimation()
    {
        yield return new WaitForSeconds(2f);

        // deactivate animation
        //animator.SetBool("Collision", false);

        //GetComponent<FallState>().AnimationOver = true;
        movementScript.ActivateMovement();
    }
}
