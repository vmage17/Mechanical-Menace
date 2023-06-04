using Godot;
using System;

public class Carrot : Enemy
{

	private enum ACTION {
		Hover,
		Plunge,
		Wait,
		Return
	}

	private ACTION action = ACTION.Hover;
	private DIRECTION direction;
	private Timer timer;
	private AnimationPlayer animationPlayer;

	private Vector2 defaultPosition;
	private Vector2 playerPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Creating energy instance
		energy = (Energy)GD.Load<PackedScene>("res://src/Objects/Energy.tscn").Instance();

		animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");

		// Setting physics off until player comes in
		SetPhysicsProcess(false);

		// Setting random direction
		Random rnd = new Random();
		direction = (rnd.Next(0, 2) == 0) ? DIRECTION.Left : DIRECTION.Right;

		// Setting intial action as hovering
		action = ACTION.Hover;

		// Setting timer
		timer = new Timer();
		AddChild(timer);
		timer.OneShot = true;
		timer.Connect("timeout", this, "Return");
	}

	// Function called after time from timer elapsed
	private void Return() {
		action = ACTION.Return;
	}

	private void afterHit(float waitTime) {
		timer.WaitTime = waitTime;
		timer.Start();
	}

	private void onPlayerBodyDetected(PhysicsBody2D body)
	{
		if(body is Player) {
			if(action == ACTION.Hover) {
				action = ACTION.Plunge;
				defaultPosition = this.GlobalPosition;
			}
		}
	}

	private void onPlayerBodyEntered(PhysicsBody2D body)
	{
		if(action == ACTION.Plunge) {
			action = ACTION.Wait;
			if(body is Player) {
				GD.Print("this is player");
				afterHit(0.4f);
			}
			else {
				GD.Print("this is not player");
				afterHit(1.0f);
			}
		}
		
			
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		switch(action) {
			case ACTION.Hover:

				animationPlayer.Play("Rotating");

				// Setting hover speed and direction
				if (velocity.x == 0.0f)
					velocity.x = (speed.x * 0.5f) * (direction == DIRECTION.Left ? -1 : 1);

				// Checking if carrot needs to change direction after hitting a wall
				if (IsOnWall()) {
					velocity.x *= -1.0f;
					direction = (direction == DIRECTION.Left ? DIRECTION.Right : DIRECTION.Left);
				}
					
			break;
			case ACTION.Plunge:
			animationPlayer.Stop(false);
				velocity.y += 2 * gravity * delta;
				velocity.x = 0.0f;
			break;
			case ACTION.Wait:
				velocity.y = 0.0f;
				velocity.x = 0.0f;
			break;
			case ACTION.Return:
				animationPlayer.Play("Rotating");
				velocity.y -= 0.5f * gravity * delta;
				velocity.x = 0.0f;
				if(this.GlobalPosition.y <= defaultPosition.y) {
					action = ACTION.Hover;
					velocity.y = 0.0f;
				}
			break;
		}
		
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}
}

// var spaceState = GetWorld2D().DirectSpaceState;
// use global coordinates, not local to node
// var query = PhysicsRayQueryParameters2D.Create(Vector2.Zero, new Vector2(1000, 0));
// var result = spaceState.IntersectRay(query);
// if (result.size() > 0)
// 	GD.Print("Hit at point: ", result["position"]);
