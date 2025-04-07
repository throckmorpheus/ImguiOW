using UImGui.Assets;
using UImGui.Renderer;
using UImGui.Texture;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
#if HAS_URP
using UnityEngine.Rendering.Universal;
#elif HAS_HDRP
using UnityEngine.Rendering.HighDefinition;
#endif

namespace UImGui
{
	internal static class RenderUtility
	{
		public static IRenderer Create(RenderType type, ShaderResourcesAsset shaders, TextureManager textures)
		{
			Assert.IsNotNull(shaders, "Shaders not assigned.");

			switch (type)
			{
				case RenderType.Procedural:
					return new RendererProcedural(shaders, textures);
				default:
					return null;
			}
		}

		public static bool IsUsingURP() => false;

		public static bool IsUsingHDRP() => false;

		public static CommandBuffer GetCommandBuffer(string name) => new() { name = name };

		public static void ReleaseCommandBuffer(CommandBuffer commandBuffer) => commandBuffer.Release();
	}
}