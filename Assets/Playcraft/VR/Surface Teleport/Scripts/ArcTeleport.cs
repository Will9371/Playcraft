using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace Playcraft.VR
{
    public class ArcTeleport : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Transform rig;
        [SerializeField] Transform head;
        [SerializeField] XRController controller;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] RendererInterface endMarker;
        [SerializeField] SO teleportTag;
        
        [Header("Properties")]
        [SerializeField] Vector2 inputThresholds;
        [SerializeField] float range = 5f;
        [SerializeField] float gravity = 9.8f;
        [SerializeField] int resolution = 40;
        [SerializeField] float maxDrop = 5f;
        [SerializeField] float maxSlope = 45f;
        [SerializeField] string shaderColorId;
        [SerializeField] Color validColor;
        [SerializeField] Color blockedColor;
        [SerializeField] Color invalidColor;
        
        [Header("Output")]
        [SerializeField] Vector3Event OnSuccess;
        [SerializeField] SO landMessage;
        
        BinaryThreshold binaryThreshold;
        CalculateParabolicArc parabolicArc;
        FindHitsInPath findHitsInPath;
        TeleportCutoffPath calculateCutoff;
        LineRendererInterface line;
        
        Vector2 axisInput;
        bool inputActive;
        bool priorInputActive;
        Vector3[] arcPoints;
        List<IndexedRaycastHit> hitsInPath;
        TeleportResult result;
        Color color;
        
        Transform source => controller.transform;
        Vector3 forward => new Vector3(0f, source.eulerAngles.y, 0f); 
        float angle => -source.localEulerAngles.x;
        Vector3 destination => result.cutoffPoint + new Vector3(offset.x, 0f, offset.z);
        Vector3 offset => rig.transform.position - head.transform.position;
        bool hasValidTarget => result.success == Trinary.True;
        bool teleportCondition => !inputActive && priorInputActive && hasValidTarget;
        
        void Start()
        {
            binaryThreshold = new BinaryThreshold(inputThresholds, false);
            parabolicArc = new CalculateParabolicArc(range, gravity, resolution, maxDrop);
            findHitsInPath = new FindHitsInPath(transform);
            calculateCutoff = new TeleportCutoffPath(transform, maxSlope, teleportTag);
            line = new LineRendererInterface(lineRenderer, shaderColorId);
        }
        
        void Update()
        {
            axisInput = XRStatics.Get2DAxisValue(controller.inputDevice);      
            
            inputActive = binaryThreshold.Input(axisInput);
            lineRenderer.enabled = inputActive;
            
            if (inputActive != priorInputActive)
                endMarker.SetVisible(inputActive);
            
            if (teleportCondition)
            {
                rig.transform.position = destination;
                OnSuccess.Invoke(destination);
                
                if (landMessage)
                {
                    var response = result.surface.GetComponent<RespondToMessage>();
                    if (response) response.Message(landMessage);
                }
            }

            priorInputActive = inputActive;
            if (!inputActive) return;
            
            transform.eulerAngles = forward;
            arcPoints = parabolicArc.Calculate(angle);
            hitsInPath = findHitsInPath.Input(arcPoints);
            result = calculateCutoff.Input(ref arcPoints, hitsInPath);
            
            line.SetPositionsAndCount(arcPoints);
            color = GetColorFromResult(result.success);
            line.SetMaterialColor(color);
            endMarker.transform.position = result.cutoffPoint;
            endMarker.SetVisible(result.success == Trinary.True);
            endMarker.SetColor(color);
        }
        
        Color GetColorFromResult(Trinary result)
        {
            switch (result)
            {
                case Trinary.True: return validColor;
                case Trinary.False: return blockedColor;
                case Trinary.Unknown: return invalidColor;
            }
            
            return invalidColor;
        }    
    }
}
