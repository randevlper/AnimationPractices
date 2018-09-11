Shader "Custom/WorldPosition2" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo 1", 2D) = "white" {}
		_MainTex2 ("Albedo 2", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Radius ("Radius", float) = 0.5
		_VectorPos ("Vector Pos", Vector) = (0,0,0,0)
		_test("test",float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		float _Radius;
		float4 _VectorPos;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color

			float3 dis = distance(_VectorPos.xyz, IN.worldPos);
			float3 sphere = 1 - (saturate(dis/_Radius));

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c2 = tex2D (_MainTex2, IN.uv_MainTex2) * _Color;

			// float3 primary = (step(sphere,0.1) * c.rgb);
			// float3 secondary = (step(0.1,sphere) * c2.rgb);

			//o.Albedo = primary + secondary;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
