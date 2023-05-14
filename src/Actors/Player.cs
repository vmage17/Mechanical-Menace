using Godot;
using System;

public class Player : Actor
{

	// Jump impulse gain by player after killing slime
	private float stompImpulse = 1800.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// When player enters stomp area
	public void onEnemyDetectorAreaEntered(Area2D area)
	{
		// Adjusting vertical velocity
		velocity = calculateStompVelocity(velocity, stompImpulse);
	}

	// When enemy enters player area
	private void onEnemyDetectorBodyEntered(Node body)
	{
		// Killing the player
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		//GD.Print("_PhysicsProcess of Player");
		//base._PhysicsProcess(delta);

		//
		bool isJumpInterrupted = Input.IsActionJustReleased("jump") && velocity.y < 0.0f;
		Vector2 direction = getDirection();
		velocity = calculateMoveVelocity(velocity, direction, speed, isJumpInterrupted);
		velocity = MoveAndSlide(velocity, FLOOR_NORMAL);
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

	public Vector2 calculateMoveVelocity(
		Vector2 linearVelocity,
		Vector2 direction,
		Vector2 speed,
		bool isJumpInterrupted
	) {
		var newVelocity = linearVelocity;
		newVelocity.x = speed.x * direction.x;
		newVelocity.y += gravity * GetPhysicsProcessDeltaTime();
		if (direction.y == -1.0f)
			newVelocity.y = speed.y * direction.y;
		if (isJumpInterrupted)
			newVelocity.y = 0.0f;
		return newVelocity;
	}

	private Vector2 calculateStompVelocity(Vector2 linearVelocity, float impulse)
	{
		var newVelocity = linearVelocity;
		newVelocity.y = -impulse;
		//GD.Print("New volcity: (" + newVelocity.x + " ," + newVelocity.y + ")");
		return newVelocity;
	}
}
