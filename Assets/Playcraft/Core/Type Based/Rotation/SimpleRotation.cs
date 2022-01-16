using UnityEngine;

namespace Playcraft
{
    public class SimpleRotation : MonoBehaviour
    {
        [SerializeField] float rotationSpeed;

        public void Rotate(Vector3 rotationAxis)
        {
            rotationAxis = rotationAxis.normalized;
            var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotationAxis, rotationStep);
        }
    }
}
