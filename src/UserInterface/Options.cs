using Godot;
using System;

public class Options : TextureButton
{
	private void onButtonUp()
	{
		GetTree().ChangeScene("res://src/Screens/OptionsScreen.tscn");
	}
}
