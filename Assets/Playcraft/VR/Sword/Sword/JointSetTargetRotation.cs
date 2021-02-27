// CREDIT: Fraser Hill

using UnityEngine;

public class JointSetTargetRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ConfigurableJoint joint;
    [SerializeField] Transform target;

    [Header("Settings")]
    [SerializeField] float torqueForce;
    [SerializeField] float angularDamping;
    [SerializeField] float maxForce;
    [SerializeField] float springForce;
    [SerializeField] float springDamping;
    [SerializeField] int solverIterations;

    Quaternion startRotation;
    Quaternion localToJointSpace;
    Rigidbody rb;

    void Start()
    {
        Vector3 forward = Vector3.Cross(joint.axis, joint.secondaryAxis);
        Vector3 up = joint.secondaryAxis;
        localToJointSpace = Quaternion.LookRotation(forward, up);
		startRotation = target.localRotation * localToJointSpace;
		startRotation = transform.localRotation * localToJointSpace;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        SetJointSettings();
		SetTargetRotation(joint, target.rotation, startRotation);
    }

    void SetJointSettings()
    {
        SoftJointLimitSpring spring = new SoftJointLimitSpring();
        spring.spring = springForce;
        spring.damper = springDamping;
        joint.linearLimitSpring = spring;

        JointDrive drive = new JointDrive();
        drive.positionSpring = torqueForce;
        drive.positionDamper = angularDamping;
        drive.maximumForce = maxForce;
        joint.slerpDrive = drive;
        rb.solverIterations = solverIterations;
    }

	/// Sets a joint's targetRotation to match a given local rotation.
	/// The joint transform's local rotation must be cached on Start and passed into this method.
	void SetTargetRotationLocal(ConfigurableJoint joint, Quaternion targetLocalRotation, Quaternion startLocalRotation)
	{
		if (joint.configuredInWorldSpace)
			Debug.LogError("SetTargetRotationLocal should not be used with joints that are configured in world space. For world space joints, use SetTargetRotation.", joint);

		SetTargetRotationInternal(joint, targetLocalRotation, startLocalRotation, Space.Self);
	}

	/// Sets a joint's targetRotation to match a given world rotation.
	/// The joint transform's world rotation must be cached on Start and passed into this method.
	void SetTargetRotation(ConfigurableJoint joint, Quaternion targetWorldRotation, Quaternion startWorldRotation)
	{
		if (!joint.configuredInWorldSpace)
			Debug.LogError("SetTargetRotation must be used with joints that are configured in world space. For local space joints, use SetTargetRotationLocal.", joint);

		SetTargetRotationInternal(joint, targetWorldRotation, startWorldRotation, Space.World);
	}

	void SetTargetRotationInternal(ConfigurableJoint joint, Quaternion targetRotation, Quaternion startRotation, Space space)
	{
		// Calculate the rotation expressed by the joint's axis and secondary axis
		var right = joint.axis;
		var forward = Vector3.Cross(joint.axis, joint.secondaryAxis).normalized;
		var up = Vector3.Cross(forward, right).normalized;
		Quaternion worldToJointSpace = Quaternion.LookRotation(forward, up);

		// Transform into world space
		Quaternion resultRotation = Quaternion.Inverse(worldToJointSpace);

		// Counter-rotate and apply the new local rotation.
		// Joint space is the inverse of world space, so we need to invert our value
		if (space == Space.World)
			resultRotation *= startRotation * Quaternion.Inverse(targetRotation);
		else
			resultRotation *= Quaternion.Inverse(targetRotation) * startRotation;

		// Transform back into joint space
		resultRotation *= worldToJointSpace;

		// Set target rotation to our newly calculated rotation
		joint.targetRotation = resultRotation;
	}
}

