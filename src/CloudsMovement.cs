using Godot;
using System;

public class CloudsMovement : ParallaxLayer
{

	[Export]
	private float CLOUD_SPEED = -0.4f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		var motionOffset = this.MotionOffset;
		motionOffset.x += CLOUD_SPEED;
		this.MotionOffset = motionOffset;
	}
}
