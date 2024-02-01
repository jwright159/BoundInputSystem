using Godot;

namespace BoundInputSystem.Bindings;

[GlobalClass, Tool]
public partial class InputBindingVector2 : InputBinding
{
	[Export] public InputBinding? XBinding;
	[Export] public InputBinding? YBinding;
	
	[Export] public bool Normalize;
	
	public override void Update(InputEvent ev)
	{
		XBinding?.Update(ev);
		YBinding?.Update(ev);
	}
	
	public override bool IsMatch(InputEvent ev) => (XBinding?.IsMatch(ev) ?? false) || (YBinding?.IsMatch(ev) ?? false);
	
	// ReSharper disable once RedundantCast
	public override float FloatValue => (float)Vector2Value.Length();
	
	// ReSharper disable once RedundantCast
	public override double DoubleValue => (double)Vector2Value.Length();
	
	public override bool BoolValue => (XBinding?.BoolValue ?? false) || (YBinding?.BoolValue ?? false);
	
	public Vector2 RawVector2Value => new(){ X = XBinding?.DoubleValue ?? 0f, Y = YBinding?.DoubleValue ?? 0f };
	public override Vector2 Vector2Value => Normalize ? RawVector2Value.Normalized() : RawVector2Value;
	
	public Vector3 RawVector3Value => new(){ X = XBinding?.DoubleValue ?? 0f, Y = YBinding?.DoubleValue ?? 0f };
	public override Vector3 Vector3Value => Normalize ? RawVector3Value.Normalized() : RawVector3Value;
	
	public override bool WasPressedThisFrame => (XBinding?.WasPressedThisFrame ?? false) || (YBinding?.WasPressedThisFrame ?? false);
	
	public override bool WasReleasedThisFrame => (XBinding?.WasReleasedThisFrame ?? false) || (YBinding?.WasReleasedThisFrame ?? false);
}