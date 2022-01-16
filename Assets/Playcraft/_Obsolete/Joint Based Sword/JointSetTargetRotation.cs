// CREDIT: mstevenson
// SOURCE: https://answers.unity.com/questions/278147/how-to-use-target-rotation-on-a-configurable-joint.html
// Edited by Will Petillo

// ERROR: Swords built with this system pass through each other when pressed

using UnityEngine;

public class JointSetTargetRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ConfigurableJoint joint;
	[SerializeField] Rigidbody rb;

	void FixedUpdate()
    {
	    SetTargetRotation();
	    rb.solverIterations = solverIterations;
    }
    
	Vector3 tertiaryAxis => Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;

	Quaternion worldToJointSpace;
	Quaternion resultRotation;
	
	/// Sets a joint's targetRotation to match a target's rotation
	void SetTargetRotation()
	{
		// Calculate the rotation expressed by the joint's axis and secondary axis
		worldToJointSpace = Quaternion.LookRotation(tertiaryAxis, joint.secondaryAxis);

		// Transform into world space
		resultRotation = Quaternion.Inverse(worldToJointSpace);

		// Transform back into joint space
		resultRotation *= worldToJointSpace;

		// Set target rotation to our newly calculated rotation
		joint.targetRotation = resultRotation;
	}
	
	#region Editor Settings
	
	[Header("Settings")]
	[SerializeField] float torqueForce;
	[SerializeField] float angularDamping;
	[SerializeField] float maxForce;
	[SerializeField] float springForce;
	[SerializeField] float springDamping;
	[SerializeField] int solverIterations;
	
	SoftJointLimitSpring spring;
	JointDrive drive;
    
	//void OnValidate() { SetJointSettings(); }

	void SetJointSettings()
	{
		spring.spring = springForce;
		spring.damper = springDamping;
		joint.linearLimitSpring = spring;

		drive.positionSpring = torqueForce;
		drive.positionDamper = angularDamping;
		drive.maximumForce = maxForce;
		joint.slerpDrive = drive;
	}
	
	#endregion
}

