using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPlataform : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private int plataformID;

	[SerializeField]
	private bool flip;

	[SerializeField]
	private float MaxX;

	[SerializeField]
	private float MinX;

	[SerializeField]
	private float MaxY;

	[SerializeField]
	private float MinY;

	Transform player;
	void Start()
    {
		player = transform;
		
	}

	// Update is called once per frame
	void Update()
	{
		Mov();
	}

	


	void Mov()
	{

		if (plataformID == 0)
		{
			if (flip == true)
			{
				transform.Translate(Vector3.right * speed * Time.deltaTime);
				
				if (transform.position.x > MaxX)
				{
					transform.position = new Vector2(MaxX, transform.position.y);
					flip = false;
				}

			}
			else
			{
				transform.Translate(Vector3.left * speed * Time.deltaTime);
				if (transform.position.x < MinX)
				{
					transform.position = new Vector2(MinX, transform.position.y);
					flip = true;
				}

			}
			
		}
		else if (plataformID == 1)
		{
			if (flip == true)
			{
				transform.Translate(Vector3.up * speed * Time.deltaTime);

				if (transform.position.y > MaxY)
				{
					transform.position = new Vector2(transform.position.x, MaxY);
					flip = false;
				}

			}
			else
			{
				transform.Translate(Vector3.down * speed * Time.deltaTime);
				if (transform.position.y < MinY)
				{
					transform.position = new Vector2(transform.position.x, MinY);
					flip = true;
				}

			}

		}
	}





}
