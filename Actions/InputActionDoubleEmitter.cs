using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem.Actions;

[Tool]
public partial class InputActionDoubleEmitter : InputActionEmitter
{
	[Signal]
	public delegate void ValueChangedEventHandler(double value);
	
	protected override void EmitEventSignal(InputBinding binding)
	{
		EmitSignal(SignalName.ValueChanged, binding.DoubleValue);
	}
}