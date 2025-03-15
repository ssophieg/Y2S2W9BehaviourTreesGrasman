using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class MarchAT : ActionTask {

		public BBParameter<GameObject> targetFruit;
		public BBParameter<bool> gotFruit;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			agent.GetComponent<NavMeshAgent>().SetDestination(targetFruit.value.transform.position);
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (Vector3.Distance(agent.transform.position, targetFruit.value.transform.position) <= 0.5)
			{
				Debug.Log("fruit grabbed");
				GameObject.Destroy(targetFruit.value);
				gotFruit.value = true;
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}