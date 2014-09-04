using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision other)
	{
		DestroyObject(gameObject);
	}
}
