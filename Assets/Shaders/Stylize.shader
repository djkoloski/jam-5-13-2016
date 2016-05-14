Shader "OMEN/Stylize" {
	Properties {
		_Albedo ("Albedo", Color) = (1.0, 1.0, 1.0, 1.0)
//		_Normal ("Normal", 2D) = "White" {}
//		_Emission ("Emission", Color) = (0.0, 0.0, 0.0, 0.0)
		_Metallic ("Metallic", Float) = 0.0
		_Smoothness ("Smoothness", Float) = 1.0
		_Occlusion ("Occlusion", Float) = 1.0
		_Alpha ("Alpha", Float) = 1.0
		_TimeScale("Time Scale", Float) = 1.0
		_NoiseMovement("Noise Movement", Vector) = (0.0, 1.0, 0.0, 0.0)
		_NoiseScale ("Noise Scale", Float) = 0.1
		_NoiseFactor ("Noise Factor", Float) = 0.1
		_PosterizeLevels ("Posterize Levels", Int) = 5
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#include "Noise.cginc"

		#pragma surface surf Standard finalcolor:posterize

		struct Input {
			float3 worldPos;
		};

		fixed3 _Albedo;
		half _Metallic;
		half _Smoothness;
		half _Occlusion;
		fixed _Alpha;
		float _TimeScale;
		float3 _NoiseMovement;
		int _PosterizeLevels;
		float _NoiseScale;
		float _NoiseFactor;
		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Albedo;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
			o.Occlusion = _Occlusion;
			o.Alpha = _Alpha;
		}

		void posterize(Input IN, SurfaceOutputStandard o, inout fixed4 color) {
			float n = noise(IN.worldPos * _NoiseScale + _NoiseMovement * _Time.y * _TimeScale);
			color = normalize(color) * floor(length(color) * _PosterizeLevels + n * _NoiseFactor) / _PosterizeLevels;
		}
		ENDCG
	}
	Fallback "Diffuse"
}