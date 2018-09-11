Shader "Custom/SimpleWater" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Normal ("Normal", 2D) = "white" {}
		_Normal2 ("Normal2", 2D) = "white" {}

		_WaveSpeed ("Wave Speed", Float) = 30
		_WaveAmp ("Wave Amplitude", Float) = 1
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_MoveDir("Move Direction", Vector) = (0,0,0,0)
		_MoveDir2("Move Direction 2", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _WaveAmp;
		float _WaveSpeed;
		sampler2D _Normal;
		sampler2D _Normal2;
		fixed4 _MoveDir;
		fixed4 _MoveDir2;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
			float2 uv_Normal2;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void vert(inout appdata_full v){
			float noiseSample = tex2Dlod(_NoiseTex, float4(v.texcoord.xy,0,0));
			v.vertex.y += sin((_Time * _WaveSpeed) * noiseSample) * _WaveAmp;
			v.normal.y += sin((_Time * _WaveSpeed) * noiseSample) * _WaveAmp;
		}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 mainTexUV = IN.uv_MainTex;
			mainTexUV += _MoveDir * _Time;

			
			fixed4 c = tex2D (_MainTex, mainTexUV) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			float2 normalUV = IN.uv_Normal;
			normalUV += _MoveDir * _Time;
			float2 normalUV2 = IN.uv_Normal2;
			normalUV2 += _MoveDir2 * _Time;
			fixed3 n = UnpackNormal((tex2D (_Normal, normalUV) + tex2D(_Normal2, normalUV2) ) / 2);



			o.Normal = n;


		}
		ENDCG
	}
	FallBack "Diffuse"
}
