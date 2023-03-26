using Godot;
using System;

public class Player : Actor
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	// Both this and parent function are called automatically
	public override void _PhysicsProcess(float delta)
	{
		Vector2 direction = new Vector2(
			Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			0.0f
		);
		velocity = speed * direction;
	}
}

