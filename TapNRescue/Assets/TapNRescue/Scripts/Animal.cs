using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalDirection
{
    Right,
    Left,
    Up,
    Down
}

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class Animal : MonoBehaviour
{
    [Header("VARIABLES")]
    #region VARIABLES
    public AnimalDirection animalDirection;
    public GameObject arrowObject;

    bool isTapped = false;
    bool readyToTap = true;
    #endregion Variables;

    [Space(5), Header("CHARACTERCONTROLLER")]
    #region CHARACTERCONTROLLER
    public CharacterController characterController;
    public float speed = 10;
    #endregion CharacterController

    [Space(5), Header("ANIMATIONS")]
    #region ANIMATIONS
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed = 1.5f;
    int[] animatorHashes =
    {
        Animator.StringToHash("Idle"),
        Animator.StringToHash("Walk"),
        Animator.StringToHash("Hit"),
        Animator.StringToHash("Jump"),
        Animator.StringToHash("JumpInPlace")
    };

    enum AnimationClips
    {
        Idle,
        Walk,
        Hit,
        Jump,
        JumpInPlace
    }
    #endregion Animations


    #region MAIN METHODS
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        animator.SetFloat("IdleSpeed", Random.Range(0.75f, 1.25f));
        animator.SetFloat("WalkSpeed", walkSpeed);
        switch (animalDirection)
        {
            case AnimalDirection.Up:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case AnimalDirection.Right:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case AnimalDirection.Down:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case AnimalDirection.Left:
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTapped)
        {
            characterController.Move(speed * Time.deltaTime * transform.forward);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (isTapped && collision.gameObject.CompareTag("Animal"))
    //    {
    //        Debug.Log("HIT");
    //        isTapped = false;
    //        animator.SetBool(animatorHashes[(int)AnimationClips.Walk], false);
    //        animator.SetBool(animatorHashes[(int)AnimationClips.Hit], true);
    //    }

    //}

    private void OnTriggerEnter(Collider other)
    {
        if (isTapped && other.gameObject.CompareTag("Animal"))
        {
            isTapped = false;
            animator.SetBool(animatorHashes[(int)AnimationClips.Walk], false);
            animator.SetBool(animatorHashes[(int)AnimationClips.Hit], true);
        }
    }

    #endregion MainMethods

    #region CUSTOM METHODS
    public void Tap()
    {
        if (!readyToTap)
            return;
        if (isTapped)
            return;
        isTapped = true;
        readyToTap = false;
        animator.SetBool(animatorHashes[(int)AnimationClips.Walk], true);
    }

    

    public void ResetAnimal()
    {

    }


    #endregion CustomMethods

}
