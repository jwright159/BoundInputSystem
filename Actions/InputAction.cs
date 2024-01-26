using BoundInputSystem.Bindings;
using Godot;
using Godot.Collections;

namespace BoundInputSystem.Actions;

[GlobalClass, Tool]
public abstract partial class InputAction : Resource
{
	private string name = "Input Action";
	[Export] public string Name
	{
		get => name;
		set
		{
			name = value;
			EmitChanged();
		}
	}
	
	private Array<InputBinding> inputBindings = new();
	[Export] public Array<InputBinding> InputBindings
	{
		get => inputBindings;
		set
		{
			inputBindings = value;
			EmitChanged();
		}
	}
	
	public abstract InputActionEmitter InstantiateEmitter();
}