using Godot;
using System;

public class Player : Actor
{

	// Jump impulse gain by player after killing slime
	private float impulse = 1800.0f;

	//
	private bool isStaved = false;

	DIRECTION stavedDirection = DIRECTION.Left;

	private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Setting timer
		timer = new Timer();
		AddChild(timer);
		timer.OneShot = true;
		timer.Connect("timeout", this, "turnIsStavedFalse");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		//
		bool isJumpInterrupted = Input.IsActionJustReleased("jump") && velocity.y < 0.0f;
		Vector2 direction = getDirection();
		velocity = calculateMoveVelocity(velocity, direction, speed, isJumpInterrupted);
		velocity = MoveAndSlide(velocity, FLOOR_NORMAL);
	}

	private void turnIsStavedFalse() {
		isStaved = false;
	}

	// When player enters stomp area
	public void onEnemyDetectorAreaEntered(Area2D area)
	{
		// Adjusting vertical velocity after stomp
		velocity = calculateNewVelocity(velocity, impulse, DIRECTION.Up);
	}

	// When enemy enters player area
	private void onEnemyDetectorBodyEntered(PhysicsBody2D body)
	{

		DIRECTION direction = (body.GlobalPosition >= this.GlobalPosition) ? DIRECTION.Left : DIRECTION.Right;

		// Adjusting horizontal stave velocity
		velocity = calculateNewVelocity(velocity, impulse, direction);

		// Killing the player
		GetTree().ChangeScene("res://src/Levels/Level1.tscn");
		//GetTree().ReloadCurrentScene()
	}

	// Returns direction vector
	public Vector2 getDirection() {
		return new Vector2(
			// Checking if player is not pressing left and right movement keys
			Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left"),
			// Checking if player is on the ground
			Input.IsActionJustPressed("jump") && IsOnFloor() ? -1.0f : 1.0f
		);
	}

	// Returns velocity vector
	public Vector2 calculateMoveVelocity(
		Vector2 linearVelocity,
		Vector2 direction,
		Vector2 speed,
		bool isJumpInterrupted
	) {
		var newVelocity = linearVelocity;
		if (isStaved) {
			newVelocity.x = speed.x * ((stavedDirection == DIRECTION.Left) ? -1.0f : 1.0f);
		}
		else {
			newVelocity.x = speed.x * direction.x;
		}
		newVelocity.y += gravity * GetPhysicsProcessDeltaTime();
		if (direction.y == -1.0f)
			newVelocity.y = speed.y * direction.y;
		if (isJumpInterrupted)
			newVelocity.y = 0.0f;
		return newVelocity;
	}

	private Vector2 calculateNewVelocity(Vector2 linearVelocity, float impulse, DIRECTION direction)
	{
		var newVelocity = linearVelocity;
		switch (direction) {
			case DIRECTION.Left:
				GD.Print("Impulse left");
				isStaved = true;
				stavedDirection = DIRECTION.Left;
				timer.WaitTime = 0.5f;
				timer.Start();
			break;
			case DIRECTION.Right:
				GD.Print("Impulse right");
				isStaved = true;
				stavedDirection = DIRECTION.Right;
				timer.WaitTime = 0.5f;
				timer.Start();
			break;
			case DIRECTION.Up:
				GD.Print("Impulse up");
				newVelocity.y = -impulse;
				
			break;
			case DIRECTION.Down:
				GD.Print("Impulse down");
				newVelocity.y = impulse;
			break;
		}
		return newVelocity;
	}

}
