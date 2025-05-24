using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_rotation : MonoBehaviour
{
	public float RotateSpeed = 25.0f;
	void Update()
	{
		transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
	}
}
