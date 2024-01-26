using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem.Actions;

[Tool]
public partial class InputActionVector2Emitter : InputActionEmitter
{
	[Signal]
	public delegate void ValueChangedEventHandler(Vector2 value);
	
	protected override void EmitEventSignal(InputBinding binding)
	{
		EmitSignal(SignalName.ValueChanged, binding.Vector2Value);
	}
}