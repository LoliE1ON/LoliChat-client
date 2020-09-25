using UnityEngine;
using UnityEngine.UI;
namespace E1on.Network {
    public class NetworkPlayer : MonoBehaviour {

        private Animator animator;
        private Transform playerTransform;
        public string userName = "";
        public ushort avatarType;
        
        private void Start() {
            animator = GetComponent<Animator>();
            playerTransform = GetComponent<Transform>();
        }
        
        public void SetNameplateName() {
            var nameplate = GetComponentInChildren<Canvas>();
            nameplate.GetComponentInChildren<Text>().text = userName;
        }
        
        public void SetMovePosition(Vector3 position) {
            playerTransform.position = Vector3.Lerp(playerTransform.position, position, 0.1f);
        }
        
        public void SetRotation(Quaternion rotation) {
            playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, rotation, 0.1f);
        }

        public void SetAnimatorParams(bool[] props) {
            animator.SetBool("isWalking", props[0]);
            animator.SetBool("isRunning", props[1]);
            animator.SetBool("isBackWalking", props[2]);
            animator.SetBool("isRightWalking", props[3]);
            animator.SetBool("isLeftWalking", props[4]);
            animator.SetBool("isDirectRightWalking", props[5]);
            animator.SetBool("isDirectLeftWalking", props[6]);
            animator.SetBool("isJumpRun", props[7]);
        }
        
    }
}