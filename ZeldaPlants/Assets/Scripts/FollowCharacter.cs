using UnityEngine;
using System.Collections;

public class FollowCharacter : MonoBehaviour
{
    public Transform Character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position = new Vector3(Character.position.x + 6, Character.position.y, 0);
	}
}
