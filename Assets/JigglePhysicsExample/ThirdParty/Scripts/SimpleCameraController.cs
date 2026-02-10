using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTemplateProjects {
    public class SimpleCameraController : MonoBehaviour {
        private Vector3 vel;
        public Transform targetLookat;
        public void SetNewTarget(Transform target) {
            targetLookat = target;
        }
        public float distance = 10f;
        private Vector2 offset;
        void Simulate() {
            if (Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed) {
                var delta = Mouse.current.delta.ReadValue();
                delta.y *= -1;
                offset += delta*0.1f;
            } else {
                //offset += new Vector2(Input.GetAxis("Horizontal"), 0f)*2f;
            }
            transform.rotation = Quaternion.AngleAxis(offset.x,Vector3.up)*Quaternion.AngleAxis(offset.y,Vector3.right);
            transform.position = Vector3.SmoothDamp(transform.position, targetLookat.position - transform.forward*distance, ref vel, 0.04f);
            transform.rotation = Quaternion.LookRotation((targetLookat.position - transform.position).normalized, Vector3.up);
            var scroll = Mouse.current.scroll.ReadValue();
            distance -= scroll.y;
            distance = Mathf.Max(distance, 1f);
        }
        public void LateUpdate() {
            Simulate();
        }
    }

}