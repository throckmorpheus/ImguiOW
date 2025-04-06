using OWML.Common;
using OWML.ModHelper;
using ImGuiNET;
using UImGui;
using UnityEngine;

namespace ImGuiOW;

public class ImGuiOW : ModBehaviour
{
	public static ImGuiOW Instance;
	public static IModConsole Console => Instance?.ModHelper?.Console;

	//private UImGui.UImGui _uImGui;
	//private TestBehaviour _testBehavour;

	public void Awake() => Instance = this;

	public void Start() {
		/*Console.LoadedMessage(this);

		_testBehavour = gameObject.AddComponent<TestBehaviour>();
		Console.LoadedMessage(_testBehavour);

		_uImGui = gameObject.AddComponent<UImGui.UImGui>();
		Console.LoadedMessage(_uImGui);
		
		if (FindObjectOfType<Camera>() is Camera camera) {
			Console.WriteLine($"Set imgui camera to {camera}.", MessageType.Success);
			_uImGui?.SetCamera(camera);
		} else Console.WriteLine($"Could not find camera for imgui.", MessageType.Error);
		
		UImGuiUtility.Layout += OnLayout;*/
	}

	/*private void OnLayout(UImGui.UImGui uImGui) {
		//ModHelper.Console.WriteLine($"OnLayout called.", MessageType.Debug);
		ImGui.ShowDemoWindow();
	}*/
}

