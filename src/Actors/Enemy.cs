using Godot;
using System;

public class Enemy : Actor
{

	protected Energy energy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	}
}

