using Godot;
using System;

public class Carrot : Enemy
{

	private bool playerDetected = false;
	private bool canDetect = true;
	private Vector2 defaultPosition;
	private Vector2 playerPosition;

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

	private void onPlayerBodyDetected(PhysicsBody2D body)
	{
		if(body is Player) {
			if(canDetect) {
				playerDetected = true;
				canDetect = false;
				defaultPosition = this.GlobalPosition;
			}
		}
	}

	private void onPlayerBodyEntered(PhysicsBody2D body)
	{
		if(body is Player) {
			playerDetected = false;
			canDetect = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		if(playerDetected) {
			// var spaceState = GetWorld2D().DirectSpaceState;
			// use global coordinates, not local to node
			// var query = PhysicsRayQueryParameters2D.Create(Vector2.Zero, new Vector2(1000, 0));
			// var result = spaceState.IntersectRay(query);
			// if (result.size() > 0)
			// 	GD.Print("Hit at point: ", result["position"]);
			velocity.y += 2 * gravity * delta;
			velocity.x = 0.0f;
		}
		else if(!canDetect) {
			velocity.y -= gravity * delta;
			velocity.x = 0.0f;
			if(this.GlobalPosition.y <= defaultPosition.y) {
				canDetect = true;
			}
		}
		else {
			// Reseting
			velocity.y += 0.0f;

			// Checkiing if carrot needs to change direction after hitting a wall
			if (IsOnWall())
				velocity.x *= -1.0f;
		}
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}
