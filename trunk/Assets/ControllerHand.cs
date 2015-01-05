using UnityEngine;
using System.Collections;

public class ControllerHand : MonoBehaviour {

    public Transform bonePrefab;

    public Transform[] positions;

    Transform[] bones;
      
    int arrLength;

	// Use this for initialization
	void Start () {
        arrLength = positions.Length;

        bones = new Transform[arrLength];

        for (int i = 1; i < arrLength; i++) {
            if (i == 2 || i == 5 || i == 8 || i == 11 || i == 14) {
                positions[i].parent = positions[1];
            }
            else {
                if (i == 0)
                    break;

                int li = i - 1; // Previous i value

                if (positions[i] == null)
                    Debug.Log("No Reference at:" + i.ToString());

                if (positions[li] == null)
                    Debug.Log("No Reference at:" + li.ToString());

                positions[i].parent = positions[li];
            }
        }

        /* Initialise Bones */
        /* 1. Make a cylinder prefab.
         * 2. Instantiate cylinder, use one of the points as the point of instantiation.
         * 3. Look at Transform.LookAt to make the cylinder face the other point.
         * 4. Scale the cylinder based on the distance between the two points.
         */
        for (int i = 0; i < arrLength; i++) {
            bones[i] = Instantiate(bonePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity) as Transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
        Transform cylinderRef;
        Transform spawn;
        Transform target;

        /* Update positions of Bones */
        for (int i = 1; i < arrLength; i++) {

            if (bones[i] == null)
                Debug.Log("No bone at:" + i.ToString());
            
            cylinderRef = bones[i];

            if (i == 2 || i == 5 || i == 8 || i == 11 || i == 14) {
                spawn = positions[i];
                target = positions[1];
            }
            else {
                int li = i - 1; // Previous i value

                spawn = positions[i];
                target = positions[li];
            }

            // Find the distance between 2 points
            Vector3 newScale = cylinderRef.localScale;
            newScale.z = Vector3.Distance(spawn.position, target.position) / 2;

            cylinderRef.localScale = newScale;
            cylinderRef.position = spawn.position;        // place bond here
            cylinderRef.LookAt(target);                   // aim bond at positiion
        }


        /*
        // Create a bond between 2 points
        // cylinderRef is a gameObject mesh cylinder with the cylinder pivot at the base pointing along the +Z.
        Transform cylinderRef;
        Transform atarget;
        Transform spawn;
 
        // Find the distance between 2 points
        Vector2 bondDistance;
        cylinderRef.localScale.z = bondDistance.Distance(spawn.position,aTarget.position)/2;
   
        cylinderRef.position = spawn.position;        // place bond here
        cylinderRef.LookAt(aTarget);            // aim bond at atom
         */


        


        /* Update joint positions
         * This is where you can update the objects positions    
         * Due to the setup, the positions will be relative to their parents
         * e.g. positions[5].position = new Vector3(4, 5, 2);
         *      // The above code would move joint 5 to 4x, 5y, 2z in world space
         *      // Children would be moved as well
         */

    }
}
