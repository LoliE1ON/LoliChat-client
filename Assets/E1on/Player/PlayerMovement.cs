using System.Collections.Generic;
using UnityEngine;

namespace E1on {
    public class PlayerMovement : MonoBehaviour {

        private Rigidbody rigidBody;
        private Transform transform;
        private Animator animator;

        private Dictionary<KeyCode, Vector3> keyDirections;
        
        private void Start() {

            rigidBody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            Movement();
            AnimationController();
        }

        private void Movement() {
            
            this.keyDirections = new Dictionary<KeyCode, Vector3>() {
                { KeyCode.W, Direction(Vector3.forward) },
                { KeyCode.A, Direction(Vector3.left) },
                { KeyCode.S, Direction(Vector3.back) },
                { KeyCode.D, Direction(Vector3.right) }
            };

            foreach (KeyValuePair<KeyCode, Vector3> item in keyDirections) {
                if (Input.GetKey(item.Key) && IsGrounded()) {
                    rigidBody.AddForce(item.Value*15);
                }
            }
            
            // Shift
            if (Input.GetKey(KeyCode.LeftShift) && IsGrounded() && !Input.GetKey(KeyCode.S)) {
                rigidBody.AddForce(Direction(Vector3.forward)*15);
            }
            
            // Jump
            if (Input.GetKey(KeyCode.Space) && IsGrounded() ) {
                Vector3 objectForce = new Vector3(0, 4, 0);
                rigidBody.AddForce(objectForce, ForceMode.Impulse);
            }

            LimitSpeed();
        }

        private void LimitSpeed() {
            if (!IsGrounded()) return;
            
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S)) {
                if(rigidBody.velocity.magnitude > 3) rigidBody.velocity = rigidBody.velocity.normalized * 3;
                return;
            }

            if(rigidBody.velocity.magnitude > 1) rigidBody.velocity = rigidBody.velocity.normalized * 1;
        }

        private void AnimationController() {

            int isWalkingHash = Animator.StringToHash("isWalking");
            int isRunningHash = Animator.StringToHash("isRunning");
            int isBackWalkingHash = Animator.StringToHash("isBackWalking");
            int isRightWalkingHash = Animator.StringToHash("isRightWalking");
            int isLeftWalkingHash = Animator.StringToHash("isLeftWalking");
            int isDirectRightWalkingHash = Animator.StringToHash("isDirectRightWalking");
            int isDirectLeftWalkingHash = Animator.StringToHash("isDirectLeftWalking");
            int isJumpRunHash = Animator.StringToHash("isJumpRun");
            
            bool isWalking = this.animator.GetBool(isWalkingHash);
            bool isRunning = this.animator.GetBool(isRunningHash);
            bool isBackWalking = this.animator.GetBool(isBackWalkingHash);
            bool isRightWalking = this.animator.GetBool(isRightWalkingHash);
            bool isLeftWalking = this.animator.GetBool(isLeftWalkingHash);
            bool isDirectRightWalking = this.animator.GetBool(isDirectRightWalkingHash);
            bool isDirectLeftWalking = this.animator.GetBool(isDirectLeftWalkingHash);
            bool isJumpRun = this.animator.GetBool(isJumpRunHash);
            
            bool forwardPressed = Input.GetKey(KeyCode.W);
            bool runPressed = Input.GetKey(KeyCode.LeftShift);
            bool backPressed = Input.GetKey(KeyCode.S);
            bool rightPressed = Input.GetKey(KeyCode.D);
            bool leftPressed = Input.GetKey(KeyCode.A);
            bool jumpPressed = Input.GetKey(KeyCode.Space);
            
            // Walk
            if(!isWalking && forwardPressed && !rightPressed && !leftPressed) this.animator.SetBool(isWalkingHash, true);
            if(isWalking && !forwardPressed) this.animator.SetBool(isWalkingHash, false);
            if(isWalking && forwardPressed && (rightPressed || leftPressed)) this.animator.SetBool(isWalkingHash, false);
            
            // Run
            if(!isRunning && forwardPressed && runPressed) this.animator.SetBool(isRunningHash, true);
            if(isRunning && (!forwardPressed || !runPressed)) this.animator.SetBool(isRunningHash, false);
            
            // Jump run
            if(!isJumpRun && forwardPressed && runPressed && jumpPressed) this.animator.SetBool(isJumpRunHash, true);
            if(isJumpRun && !jumpPressed) this.animator.SetBool(isJumpRunHash, false);
            
            // Back walk
            if(!isBackWalking && backPressed) this.animator.SetBool(isBackWalkingHash, true);
            if(isBackWalking && !backPressed) this.animator.SetBool(isBackWalkingHash, false);

            if (!isRightWalking && rightPressed && forwardPressed) this.animator.SetBool(isRightWalkingHash, true);
            if((isRightWalking && !forwardPressed) || !rightPressed) this.animator.SetBool(isRightWalkingHash, false);
            if(!isLeftWalking && leftPressed && forwardPressed) this.animator.SetBool(isLeftWalkingHash, true);
            if((isLeftWalking && !forwardPressed) || !leftPressed) this.animator.SetBool(isLeftWalkingHash, false);
            
            if (!isDirectRightWalking && rightPressed && !forwardPressed) this.animator.SetBool(isDirectRightWalkingHash, true);
            if ((isDirectRightWalking && !rightPressed) || rightPressed && forwardPressed) this.animator.SetBool(isDirectRightWalkingHash, false);
            if (!isDirectLeftWalking && leftPressed && !forwardPressed) this.animator.SetBool(isDirectLeftWalkingHash, true);
            if ((isDirectLeftWalking && !leftPressed) || leftPressed && forwardPressed) this.animator.SetBool(isDirectLeftWalkingHash, false);
            
        }

        private Vector3 Direction(Vector3 vector) {
            Vector3 direction = transform.TransformDirection(vector);
            return new Vector3(direction.x, 0, direction.z);
        }

        private bool IsGrounded() {
            return Physics.Raycast(this.transform.position, -Vector3.up, 0.01f);
        }
    }
}