using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class MarchAT : ActionTask {

		//the fruit the lemming is approaching
		public BBParameter<GameObject> targetFruit;

		//boolean that turns true when the lemming gets to the fruit
		public BBParameter<bool> gotFruit;

		//variables to make the lemming march
		float marchTimer;
		bool marching = true;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            marchTimer = 0;

			//set lemming destination to the fruit
            agent.GetComponent<NavMeshAgent>().SetDestination(targetFruit.value.transform.position);
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			//march timer goes up
			marchTimer += Time.deltaTime;

			//if march timer hits 0.5 seconds, start or stop the movement to simulate marching strides
			if (marchTimer > 0.5f) 
			{
				marchTimer = 0;
				if (marching) 
				{
					agent.GetComponent<NavMeshAgent>().speed = 0;
                }
                if (!marching)
                {
                    agent.GetComponent<NavMeshAgent>().speed = 3.5f;
                }

				marching = !marching;
            }

			//if the lemming gets to the fruit, pick it up
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

			//ensure the navmesh speed is normal upon leaving the task
            agent.GetComponent<NavMeshAgent>().speed = 3.5f;
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}