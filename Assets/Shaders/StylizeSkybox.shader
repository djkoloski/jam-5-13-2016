Shader "OMEN/StylizeSkybox" {
	Properties{
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_TimeScale("Time Scale", Float) = 1.0
		_NoiseMovement("Noise Movement", Vector) = (0.0, 1.0, 0.0, 0.0)
		_NoiseScale("Noise Scale", Float) = 0.1
		_NoiseFactor("Noise Factor", Float) = 0.1
		_PosterizeLevels("Posterize Levels", Int) = 5
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
		#include "Noise.cginc"

		#pragma surface surf Standard

		struct Input {
			float3 worldPos;
		};

		fixed3 _Color;
		float _TimeScale;
		float3 _NoiseMovement;
		int _PosterizeLevels;
		float _NoiseScale;
		float _NoiseFactor;
		void surf(Input IN, inout SurfaceOutputStandard o) {
			float n = noise(IN.worldPos * _NoiseScale + _NoiseMovement * _Time.y * _TimeScale);
			float3 pos = clamp(normalize(IN.worldPos), 0, 1);
			o.Emission = floor(pos.y * _PosterizeLevels + n * _NoiseFactor) / _PosterizeLevels * fixed4(_Color, 1.0);
		}
		ENDCG
	}
	Fallback "Diffuse"
}