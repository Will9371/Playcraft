using UnityEngine;

namespace Playcraft
{
	public class FollowInstantPartial : MonoBehaviour 
	{
		public Transform target;
		public void SetTarget(Transform value) { target = value; }
		public void ClearTarget() { target = null; }
		
		enum FollowStyle { All, Partial, None }

		#pragma warning disable 0649
		[SerializeField] FollowStyle followPosition = FollowStyle.All;
		
	    [SerializeField] bool followXPosition;
	    [SerializeField] bool followYPosition;
	    [SerializeField] bool followZPosition;

		[SerializeField] Vector3 positionOffset = Vector3.zero;

	    [SerializeField] FollowStyle followRotation = FollowStyle.None;
	    [SerializeField] bool followXRotation;
	    [SerializeField] bool followYRotation;
	    [SerializeField] bool followZRotation;

		[SerializeField] Vector3 rotationOffset = Vector3.zero;
		#pragma warning restore 0649
		
		
		void Update() 
		{
			if (!target)
				return;

			if (followPosition == FollowStyle.All)
				transform.position = target.position + positionOffset;
			else if (followPosition == FollowStyle.Partial)
			{
				var x = followXPosition ? target.position.x + positionOffset.x : transform.position.x;
				var y = followYPosition ? target.position.y + positionOffset.y : transform.position.y;
			    var z = followZPosition ? target.position.z + positionOffset.z : transform.position.z;
			    transform.position = new Vector3(x, y, z);
			}

		    if (followRotation == FollowStyle.All)
				transform.eulerAngles = target.eulerAngles + rotationOffset;
			else if (followRotation == FollowStyle.Partial)
			{
				var x = followXRotation ? target.rotation.x + rotationOffset.x : transform.rotation.x;
				var y = followYRotation ? target.rotation.y + rotationOffset.y : transform.rotation.y;
			    var z = followZRotation ? target.rotation.z + rotationOffset.z : transform.rotation.z;
			    transform.eulerAngles = new Vector3(x, y, z);
			}
		}
	}
}
