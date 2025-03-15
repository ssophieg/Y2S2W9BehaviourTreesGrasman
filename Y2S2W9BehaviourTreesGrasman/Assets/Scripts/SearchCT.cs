using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class SearchCT : ConditionTask {

		public GameObject fruitPrefab;
		Vector3 fruitSpawnPosition;
		public float baseSearchRadius;
		float currentSearchRadius;
		public float radiusIncrease;
		public BBParameter<Vector3> currentFruitPosition;

		public BBParameter<GameObject> currentFruit;

		public LayerMask fruitLayerMask;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
            currentSearchRadius = baseSearchRadius;
            fruitSpawnPosition = new Vector3(Random.Range(6.95f, -6.87f), 0.16f, Random.Range(9.08f, -9.04f));
            GameObject.Instantiate(fruitPrefab, fruitSpawnPosition, Quaternion.identity);
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

            Collider[] fruitsFound = Physics.OverlapSphere(agent.transform.position, currentSearchRadius, fruitLayerMask);

            //store rodent location and object when rodent is found
            foreach (Collider fruitCollider in fruitsFound)
            {
                currentFruit.value = fruitCollider.gameObject;
            }

            currentFruitPosition.value = fruitSpawnPosition;

            if (Vector3.Distance(fruitSpawnPosition, agent.transform.position) <= currentSearchRadius)
			{
                currentSearchRadius = baseSearchRadius;
                fruitSpawnPosition = new Vector3(Random.Range(6.95f, -6.87f), 0.16f, Random.Range(9.08f, -9.04f));
                GameObject.Instantiate(fruitPrefab, fruitSpawnPosition, Quaternion.identity);
                Debug.Log("fruit found");
				return true;
			}

			currentSearchRadius += radiusIncrease * Time.deltaTime;
			return false;
		}
	}
}