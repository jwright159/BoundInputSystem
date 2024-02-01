using Godot;

namespace BoundInputSystem.Bindings;

[GlobalClass, Tool]
public partial class InputBindingVector1 : InputBinding
{
	[Export] public InputBinding? PositiveBinding;
	[Export] public InputBinding? NegativeBinding;
	
	public override void Update(InputEvent ev)
	{
		PositiveBinding?.Update(ev);
		NegativeBinding?.Update(ev);
	}
	
	public override bool IsMatch(InputEvent ev) => (PositiveBinding?.IsMatch(ev) ?? false) || (NegativeBinding?.IsMatch(ev) ?? false);
	
	public override float FloatValue => (PositiveBinding?.FloatValue ?? 0f) - (NegativeBinding?.FloatValue ?? 0f);
	
	public override double DoubleValue => (PositiveBinding?.DoubleValue ?? 0f) - (NegativeBinding?.DoubleValue ?? 0f);
	
	public override bool BoolValue => (PositiveBinding?.BoolValue ?? false) || (NegativeBinding?.BoolValue ?? false);
	
	public override Vector2 Vector2Value => new(){ X = DoubleValue };
	
	public override Vector3 Vector3Value => new(){ X = DoubleValue };
	
	public override bool WasPressedThisFrame => (PositiveBinding?.WasPressedThisFrame ?? false) || (NegativeBinding?.WasPressedThisFrame ?? false);
	
	public override bool WasReleasedThisFrame => (PositiveBinding?.WasReleasedThisFrame ?? false) || (NegativeBinding?.WasReleasedThisFrame ?? false);
}