﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundPlayer : MonoBehaviour
{
	[SerializeField] private Rigidbody2D playerRB;
	[SerializeField] private float speed;
	private float ObjectPositionX;
	private float playerPositionX;
	private float playerDirectionSpeed;
	private float DestPoint = -8.56f;
	private float RightRespownPoint = 9f;
	private float LeftRespownPoint = -9f;


    void Start()
    {
		transform.position = new Vector2(transform.position.x + playerRB.transform.position.x, transform.position.y);
	}

    void Update()
	{
		playerDirectionSpeed = playerRB.velocity.x;
		ObjectPositionX = transform.position.x;

		if (playerPositionX != playerRB.transform.position.x) {
			if		(playerDirectionSpeed > 0 )
				ObjectPositionX += speed * Time.deltaTime;
			else if (playerDirectionSpeed < 0 )
				ObjectPositionX -= speed * Time.deltaTime;
		}
		transform.position = new Vector2(ObjectPositionX, transform.position.y);
		playerPositionX = playerRB.transform.position.x;
	}
    private void FixedUpdate()
	{
		if (playerDirectionSpeed > 0 && ObjectPositionX <= DestPoint + playerPositionX)
		{
			ObjectPositionX = RightRespownPoint + playerPositionX;
			transform.position = new Vector2(ObjectPositionX, transform.position.y);
		}
		if (playerDirectionSpeed < 0 && ObjectPositionX >= -DestPoint + playerPositionX)
		{
			ObjectPositionX = LeftRespownPoint + playerPositionX;
			transform.position = new Vector2(ObjectPositionX, transform.position.y);
		}

	}
}
