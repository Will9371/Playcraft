using UnityEngine;

namespace Playcraft
{
    public class GetMouseMovement : MonoBehaviour
    {
        [SerializeField] Vector2 sensitivity = new Vector2(1f, 1f);
        [SerializeField] Vector2Event Output = default;
        
        float x;
        float y;

        void Update()
        {
            x = Input.GetAxis("Mouse X") * sensitivity.x;
            y = Input.GetAxis("Mouse Y") * sensitivity.y;
            Output.Invoke(new Vector2(x, y));
        }
    }
}
