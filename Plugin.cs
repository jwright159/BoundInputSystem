#if TOOLS
using BoundInputSystem.Actions;
using BoundInputSystem.Bindings;
using Godot;

namespace BoundInputSystem;

[Tool]
public partial class Plugin : EditorPlugin
{
	public override void _EnterTree()
	{
		AddCustomType<InputSystem, Resource>();
		AddCustomType<InputSystemEmitter, Node>();
		AddCustomType<InputMap, Resource>();
		AddCustomType<InputMapEmitter, Node>();
		
		AddCustomType<InputAction, Resource>("Actions");
		AddCustomType<InputActionEmitter, Node>("Actions");
		AddCustomType<InputActionBool, InputAction>("Actions");
		AddCustomType<InputActionBoolEmitter, InputActionEmitter>("Actions");
		AddCustomType<InputActionFloat, InputAction>("Actions");
		AddCustomType<InputActionFloatEmitter, InputActionEmitter>("Actions");
		AddCustomType<InputActionVector2, InputAction>("Actions");
		AddCustomType<InputActionVector2Emitter, InputActionEmitter>("Actions");
		
		AddCustomType<InputBinding, Resource>("Bindings");
		AddCustomType<InputBindingKey, InputBinding>("Bindings");
		AddCustomType<InputBindingVector1, InputBinding>("Bindings");
		AddCustomType<InputBindingVector2, InputBinding>("Bindings");
	}
	
	public override void _ExitTree()
	{
		RemoveCustomType<InputSystem>();
		RemoveCustomType<InputSystemEmitter>();
		RemoveCustomType<InputMap>();
		RemoveCustomType<InputMapEmitter>();
		
		RemoveCustomType<InputAction>();
		RemoveCustomType<InputActionEmitter>();
		RemoveCustomType<InputActionBool>();
		RemoveCustomType<InputActionBoolEmitter>();
		RemoveCustomType<InputActionFloat>();
		RemoveCustomType<InputActionFloatEmitter>();
		RemoveCustomType<InputActionVector2>();
		RemoveCustomType<InputActionVector2Emitter>();
		
		RemoveCustomType<InputBinding>();
		RemoveCustomType<InputBindingKey>();
		RemoveCustomType<InputBindingVector1>();
		RemoveCustomType<InputBindingVector2>();
	}
	
	private void AddCustomType<TType, TBase>(string path = ".")
	{
		GD.Print($"Loading {typeof(TType).Name} under {typeof(TBase).Name}");
		AddCustomType(typeof(TType).Name, typeof(TBase).Name, GD.Load<Script>($"res://addons/BoundInputSystem/{path}/{typeof(TType).Name}.cs"), null);
	}
	
	private void RemoveCustomType<TType>()
	{
		GD.Print($"Unloading {typeof(TType).Name}");
		RemoveCustomType(typeof(TType).Name);
	}
}
#endif
