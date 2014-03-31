using UnityEngine;
using System.Collections;

public class FollowCharacter : MonoBehaviour
{
    public Transform Character;
    public int CameraAdjustment = 6;
    public float CameraZ = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Character.position.x + CameraAdjustment, Character.position.y, CameraZ);
	}
}
