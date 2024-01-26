using BoundInputSystem.Actions;
using Godot;
using Godot.Collections;

namespace BoundInputSystem;

[GlobalClass, Tool]
public partial class InputMap : Resource
{
	private string name = "Input Map";
	[Export] public string Name
	{
		get => name;
		set
		{
			name = value;
			EmitChanged();
		}
	}
	
	private Array<InputAction> inputActions = new();
	[Export] public Array<InputAction> InputActions
	{
		get => inputActions;
		set
		{
			inputActions = value;
			EmitChanged();
		}
	}
}