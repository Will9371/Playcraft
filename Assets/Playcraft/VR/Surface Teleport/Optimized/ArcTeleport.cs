using System.Collections.Generic;
using Playcraft.VR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace Playcraft.Optimized.VR
{
    public class ArcTeleport : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform rig;
        [SerializeField] Transform head;
        [SerializeField] XRController controller;
        [SerializeField] Vector2 inputThresholds;
        [SerializeField] float range = 5f;
        [SerializeField] float gravity = 9.8f;
        [SerializeField] int resolution = 40;
        [SerializeField] float maxDrop = 5f;
        [SerializeField] float maxSlope = 45f;
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] string shaderColorId;
        [SerializeField] Color validColor;
        [SerializeField] Color blockedColor;
        [SerializeField] Color invalidColor;
        [SerializeField] RendererInterface endMarker;
        [SerializeField] Vector3Event OnSuccess;
        #pragma warning restore 0649
        
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
        
        private void Start()
        {
            binaryThreshold = new BinaryThreshold(inputThresholds);
            parabolicArc = new CalculateParabolicArc(range, gravity, resolution, maxDrop);
            findHitsInPath = new FindHitsInPath(transform);
            calculateCutoff = new TeleportCutoffPath(transform, maxSlope);
            line = new LineRendererInterface(lineRenderer, shaderColorId);
        }
        
        private void Update()
        {
            axisInput = XRStatics.Get2DAxisValue(controller.inputDevice);      
            
            inputActive = binaryThreshold.Input(axisInput);
            lineRenderer.enabled = inputActive;
            endMarker.SetVisible(inputActive);
            
            if (teleportCondition)
            {
                rig.transform.position = destination;
                OnSuccess.Invoke(destination);
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
        
        private Color GetColorFromResult(Trinary result)
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
