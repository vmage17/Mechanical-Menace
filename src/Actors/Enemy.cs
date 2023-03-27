using Godot;
using System;

public class Enemy : Actor
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetPhysicsProcess(false);
		velocity = -speed * 0.5f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		velocity.y += gravity * delta;
		if (IsOnWall())
			velocity.x *= -1.0f;
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}
