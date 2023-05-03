using Godot;
using System;

public class Energy : KinematicBody2D
{

	public static Vector2 FLOOR_NORMAL = new Vector2(0.0f, -1.0f);
	private AnimationPlayer animationPlayer;
	private const float gravity = 400.0f;
	protected Vector2 velocity = new Vector2(0.0f, 0.0f);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GD.Print(GetChildren());
		animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		velocity.y += gravity * delta;
		velocity.y = MoveAndSlide(velocity, FLOOR_NORMAL).y;
	}

	private void onPlayerEntered(PhysicsBody2D body)
	{
		if(body.Name == "Player")
			animationPlayer.Play("fade_out");
	}
}
