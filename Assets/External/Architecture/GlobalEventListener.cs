using UnityEngine;
using UnityEngine.Events;

// Triggers a global event, confirgable via the inspector. Meant for game-wide events.
// 1) Create a new GlobalEvent Scriptable Object in Project Window.
// 2) On the script which is going to trigger the event, add a public GlobalEvent field. Drag/drop the Scriptable Object you created in Step 1
// 3) To trigger the event, call GlobalEvent.Invoke();
// 4) Add a GlobalEventListener component to the GameObject that will receive the Invoke call. Configure the UnityEvent

[System.Serializable]
public class UnityEventGameObjectFloat : UnityEvent<GameObject, float> {}

public class GlobalEventListener : MonoBehaviour {

    public GlobalEvent globalEvent;
    public UnityEvent response;
    public UnityEventGameObjectFloat responseGameObjectFloat;

    private void OnEnable()
    {
        globalEvent.Subscribe(this);
    }

    private void OnDisable()
    {
        globalEvent.Unsubscribe(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }

    public void OnEventRaised(GameObject gameObject, float value)
    {
        responseGameObjectFloat.Invoke(gameObject, value);
    }
}
