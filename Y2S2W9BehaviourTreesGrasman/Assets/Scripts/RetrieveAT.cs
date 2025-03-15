using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class RetrieveAT : ActionTask {

		//center of the area
		Vector3 returnPoint;

		//the fruit dummy is an object that looks the same as a regular fruit, but is on a different layer mask so that the lemming cannot detect it.
		//this is done so the lemming can make a pile of them without getting mixed up between fruits it collected and fruits it can still find.
        public GameObject fruitDummy;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			returnPoint = new Vector3(0, 0.63f, 0);
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//set destination to center of area
			agent.GetComponent<NavMeshAgent>().SetDestination(returnPoint);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			//if arrived at center, end action
			if (Vector3.Distance(agent.transform.position, returnPoint) <= 1)
			{
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {

			//drop off fruit it found around the middle
			GameObject.Instantiate(fruitDummy, new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0.16f, 0.5f), Random.Range(-1.5f, 1.5f)), Quaternion.identity);
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}