using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Transform cameraTransform;
    public Transform tracker;

    [Header("Movement")]
    public float moveSpeed;

    CharacterController character;
    Vector3 motion;
    float targetAngle;

    Animator animator;

	void Start () {
        character = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
	}

    void FixedUpdate () {
        //Handle movement
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        if (input.sqrMagnitude > 0)
        {
            motion = input;
            animator.SetBool("Walking", true);
            targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;
        }
        else
        {
            animator.SetBool("Walking", false);
            motion = Vector3.Lerp(motion, Vector3.zero, Time.fixedDeltaTime * moveSpeed);
        }
        transform.localEulerAngles = Vector3.up * Mathf.LerpAngle(transform.localEulerAngles.y, targetAngle, Time.fixedDeltaTime * 25);
        //tracker.localEulerAngles = -transform.localEulerAngles;
        character.Move(motion * moveSpeed * Time.fixedDeltaTime);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position,transform.position,Time.fixedDeltaTime*10);
    }
}
