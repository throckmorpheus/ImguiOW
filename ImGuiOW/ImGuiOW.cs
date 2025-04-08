using OWML.Common;
using OWML.ModHelper;
using ImGuiNET;
using UImGui;
using UnityEngine;
using System.Linq;
using System;
using OWML.Utils;
using UnityEngine.SceneManagement;

namespace ImGuiOW;

public class ImGuiOW : ModBehaviour
{
	public static ImGuiOW Instance;
	public static IModConsole Console => Instance?.ModHelper?.Console;

	private UImGui.UImGui _uImGui = null;
	private Camera _activeCamera = null;
	private Camera ActiveCamera {
		get => _activeCamera;
		set {
			_activeCamera = value;
			if (_activeCamera is not null) _uImGui?.SetCamera(_activeCamera);
			_uImGui.enabled = _activeCamera is not null;
		}
	}

	public void Awake() {
		Instance = this;
	}

	public void Start() {
		Console.Success("ImGui loaded.");

		_uImGui = gameObject.AddComponent<UImGui.UImGui>();

		if (ModHelper.Assets.LoadBundle("assets/shaders")?.LoadAllAssets()?.SingleOrDefault() is Shader shader) {
			_uImGui?.SetShader(shader); Console.Debug("Loaded UImGui shader.");
		} else Console.Error("Failed to load shaders.");

		OnCompleteSceneChange(OWScene.None, OWScene.TitleScreen);
		ModHelper.Events.Scenes.OnCompleteSceneChange += OnCompleteSceneChange;
		GlobalMessenger<OWCamera>.AddListener("SwitchActiveCamera", OnSwitchActiveCamera);
		GlobalMessenger.AddListener("WakeUp", OnPlayerWakeUp);
		
		UImGuiUtility.Layout += OnLayout;
	}

	public void OnDestroy() {
		GlobalMessenger<OWCamera>.RemoveListener("SwitchActiveCamera", OnSwitchActiveCamera);
		GlobalMessenger.RemoveListener("WakeUp", OnPlayerWakeUp);
	}

	private void OnLayout(UImGui.UImGui uImGui) {
		ImGui.ShowDemoWindow();
	}

	private void FindCamera() {
		if (Locator._playerCamera is OWCamera playerCamera) ActiveCamera = playerCamera._mainCamera ?? playerCamera._farCamera;
		else if (FindObjectOfType<OWCamera>() is OWCamera owCamera) ActiveCamera = owCamera._mainCamera ?? owCamera._farCamera;
		else ActiveCamera = FindObjectOfType<Camera>();
	}

	private void OnCompleteSceneChange(OWScene oldScene, OWScene newScene) => FindCamera();
	private void OnPlayerWakeUp() => FindCamera();

	private void OnSwitchActiveCamera(OWCamera camera) {
		Console.Message($"Switch active camera to {camera}");
		ActiveCamera = camera?._mainCamera ?? camera?._farCamera;
	}
}

