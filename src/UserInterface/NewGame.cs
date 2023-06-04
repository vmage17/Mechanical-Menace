using Godot;
using System;

public class NewGame : TextureButton
{
	private void onButtonUp()
	{
		GetTree().ChangeScene("res://src/Levels/Level1.tscn");
	}
}
