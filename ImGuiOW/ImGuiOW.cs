using OWML.Common;
using OWML.ModHelper;
using ImGuiNET;
using UImGui;
using UnityEngine;
using System.Linq;
using System;

namespace ImGuiOW;

public class ImGuiOW : ModBehaviour
{
	public static ImGuiOW Instance;
	public static IModConsole Console => Instance?.ModHelper?.Console;

	private UImGui.UImGui _uImGui = null;

	public void Awake() {
		Instance = this;
	}

	public void Start() {
		_uImGui = gameObject.AddComponent<UImGui.UImGui>();

		if (ModHelper.Assets.LoadBundle("assets/shaders")?.LoadAllAssets()?.SingleOrDefault() is Shader shader) {
			_uImGui?.SetShader(shader); Console.Debug("Loaded UImGui shader.");
		} else Console.Error("Failed to load shaders.");
		
		if (FindObjectOfType<Camera>() is Camera camera) {
			_uImGui?.SetCamera(camera); Console.Debug("Set UImGui camera.");
		}
		else Console.Error($"Could not find camera for UImGui.");

		_uImGui.enabled = true;

		Console.Success("ImGui loaded.");
		
		UImGuiUtility.Layout += OnLayout;
	}

	private void OnLayout(UImGui.UImGui uImGui) {
		//ModHelper.Console.WriteLine($"OnLayout called.", MessageType.Debug);
		ImGui.ShowDemoWindow();
	}
}

