using Godot;
using System;

public class Continue : TextureButton
{
	private void onButtonUp()
	{
		GetTree().ChangeScene("res://src/Levels/Level1.tscn");
	}
}
