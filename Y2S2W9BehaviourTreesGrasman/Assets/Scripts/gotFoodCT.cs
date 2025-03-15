using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class gotFoodCT : ConditionTask {

		//boolean for if the lemming has collected the fruit it targetted
		public BBParameter<bool> gotFruit;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {

			//if the lemming picked up the fruit, set boolean to false return true
            if (gotFruit.value == true)
            {
				gotFruit.value = false;
				return true;
            }
            return false;
		}
	}
}