using Godot;
using System;

public class Actor : KinematicBody2D
{
	
	public Vector2 speed = new Vector2(300.0f, 1000.0f);
	public float gravity = 1000.0f;
	public Vector2 velocity = new Vector2(0.0f, 0.0f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		velocity.y += gravity * delta;
		//if (velocity.y > speed.y)
		//	velocity.y = speed.y;
		velocity = MoveAndSlide(velocity);
	}
}
