using UnityEngine;

namespace Playcraft
{
    public class RangeIndicator : MonoBehaviour
    {
        #pragma warning disable 0649
        public float maxRange;
        public float currentRange;
        public float expandSpeed;
        [SerializeField] Transform followXZ;
        #pragma warning restore 0649

        Projector projector;

        void Start()
        {
            projector = GetComponent<Projector>();
        }

        void Update()
        {
            transform.localPosition = new Vector3(followXZ.localPosition.x, transform.localPosition.y, followXZ.localPosition.z);

            if (currentRange < maxRange)
            {
                currentRange += expandSpeed * Time.deltaTime;

                if (currentRange > maxRange)
                    currentRange = maxRange;

                if (projector)
                    transform.localScale = VectorMath.EqualVector3(currentRange * 2);
                else
                    projector.orthographicSize = currentRange;
            }
        }
    }
}