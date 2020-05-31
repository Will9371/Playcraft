using UnityEngine;

namespace Playcraft
{
    public class SimpleRotation : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float rotationSpeed;
        #pragma warning restore 0649

        public void Rotate(Vector3 rotationAxis)
        {
            rotationAxis = rotationAxis.normalized;
            var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotationAxis, rotationStep);
        }
    }
}
