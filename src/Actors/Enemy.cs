using Godot;
using System;

public class Enemy : Actor
{

	private Energy instancedEnergy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//instancedEnergy = ResourceLoader.Load("res://src/Objects/Energy.tscn").Get();
		SetPhysicsProcess(false);
		velocity.x = -speed.x * 0.5f;
	}

	private void onStompDetectorBodyEntered(PhysicsBody2D body)
	{
		//if (body.GlobalPosition.y > GetNode("StompDetector").GlobalPosition.y)
		//	return;
		//GetNode("CollisionShape2D").Disabled(true);

		//Energy enemyEnergy = new Energy();
		//enemyEnergy.GlobalPosition = this.GlobalPosition;
		//GetParent().AddChild(enemyEnergy);
		
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		velocity.y += gravity * delta;
		if (IsOnWall())
			velocity.x *= -1.0f;
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}

