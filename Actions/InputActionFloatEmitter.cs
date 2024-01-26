using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem.Actions;

[Tool]
public partial class InputActionFloatEmitter : InputActionEmitter
{
	[Signal]
	public delegate void ValueChangedEventHandler(float value);
	
	protected override void EmitEventSignal(InputBinding binding)
	{
		EmitSignal(SignalName.ValueChanged, binding.FloatValue);
	}
}