using UnityEngine;
using UnityEngine.AI;

namespace Playcraft.Navigation
{
	// Prevents NavMeshAgent from controlling transform position while pathfinding 
	public class NavAgentControl : MonoBehaviour 
	{
		NavMeshAgent agent;

		void Start() 
		{
			agent = GetComponent<NavMeshAgent>();
			agent.updatePosition = false;
			agent.updateRotation = false;
		}
		
		// Keeps simulated position synced to actual position
		// So pathfinding begins from correct location
		void Update() 
		{
			agent.nextPosition = transform.position;
		}
	}
}
