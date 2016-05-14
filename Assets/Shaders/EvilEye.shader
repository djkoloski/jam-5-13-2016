Shader "OMEN/EvilEye" {
	Properties{
		_EyelidColor("Eyelid Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_EyelidThreshold("Eyelid Threshold", Float) = 1.0
		_ScleraColor("Sclera Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_ScleraThreshold("Sclera Threshold", Float) = 1.0
		_IrisColor("Iris Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_IrisThreshold("Iris Threshold", Float) = 1.0
		_PupilColor("Pupil Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_PupilThreshold("Pupil Threshold", Float) = 1.0
		_Scale("Scale", Float) = 1.0
		_Offset("Offset", Float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparnet" }
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			float4 _EyelidColor;
			float _EyelidThreshold;
			float4 _ScleraColor;
			float _ScleraThreshold;
			float4 _IrisColor;
			float _IrisThreshold;
			float4 _PupilColor;
			float _PupilThreshold;
			float _Offset;
			float _Scale;

			fixed4 frag(v2f i) : SV_Target
			{
				float dist = length(float3(i.uv, 0.0) - float3(0.5, 0.5, _Offset)) / _Scale;

				if (dist < _PupilThreshold)
					return _PupilColor;
				else if (dist < _IrisThreshold)
					return _IrisColor;
				else if (dist < _ScleraThreshold)
					return _ScleraColor;
				else if (dist < _EyelidThreshold)
					return _EyelidColor;
				else
					clip(-1);
				return float4(0.0, 0.0, 0.0, 0.0);
			}

			ENDCG
		}
	}
	Fallback "Diffuse"
}