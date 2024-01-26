using Godot;

namespace BoundInputSystem.Actions;

[GlobalClass, Tool]
public partial class InputActionFloat : InputAction
{
	public override InputActionEmitter InstantiateEmitter() => new InputActionFloatEmitter { InputAction = this };
}