using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;

// Triggers a local event, configurable via inspector. Meant for instances of prefabs.
// 1) Create a new GameObject, call it Event. Add a LocalEvent component.
// 2) In your script that needs to invoke the event, create a public LocalEvent field. 
// 3) In the inspector, drag/drop the GameObject you created in Step 1 into the LocalEvent field.
// 4) Add a LocalEventListener component on the GameObject that needs to receieve the Invoke call. Configure the UnityEvent.
// 5) In your script that needs to invoke the event, call LocalEvent.Invoke();

public class LocalEventListener : MonoBehaviour
{

    public LocalEvent localEvent;
    public UnityEvent response;

    private void OnEnable()
    {
        localEvent.Subscribe(InvokeUnityEvent);
    }

    private void OnDisable()
    {
        localEvent.UnSubscribe(InvokeUnityEvent);
    }

    public void InvokeUnityEvent()
    {
        response.Invoke();
    }
}
