using Godot;
using System;

public class Slime : Actor
{

	private Energy energy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Creating energy instance
		energy = (Energy)GD.Load<PackedScene>("res://src/Objects/Energy.tscn").Instance();

		// Setting physics off until player comes in
		SetPhysicsProcess(false);
		
		// Setting vertical speed
		velocity.x = -speed.x * 0.5f;
	}

	private void onStompDetectorBodyEntered(PhysicsBody2D body)
	{
		// Doesn't work xd
		//if (body.GlobalPosition.y > GetNode("StompDetector").GlobalPosition.y)
		//	return;
		//GetNode("CollisionShape2D").Disabled(true);

		// Setting position of dropped energy
		energy.Position = this.Position;

		//Adding energy to the scene
		GetParent().AddChild(energy);

		// Cannot find parent or something idk
		//GetParent().CallDeferred("AddChild", energy);

		// Deleting the enemy
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		// Gravity
		velocity.y += gravity * delta;

		// Checkiing if slime needs to change direction after hitting a wall
		if (IsOnWall())
			velocity.x *= -1.0f;
		
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}

