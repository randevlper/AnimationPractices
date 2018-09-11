Shader "Custom/BasicKobeShaderLinPhon" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo 2", 2D) = "white" {}
		_MainTexLerp ("Albedo Lerp", Range(0,1)) = 0.0
		_Shininess  ("Shininess", Range(0.3,1)) = 0.5
		_SpecColor ("Specular Color", Color) = (1,1,1,1)
		_Normal ("Normal", 2D) = "white" {}
		_EmissionColor ("Emission Color", Color) = (1,1,1,1)
		_EmissionMagnitude("Emission Magnitude", Float) = 1.0
		_Emission ("Emission", 2D) = "black" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _Emission;
		sampler2D _Normal;

		struct Input {
			float2 uv_MainTex;
			float2 uv_MainTex2;
			float2 uv_Emission;
			float2 uv_Normal;
		};

		float _Shininess;
		half _MainTexLerp;
		fixed4 _Color;
		fixed4 _EmissionColor;
		half _EmissionMagnitude;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			// float2 uvModMainTex = IN.uv_MainTex + _Time;
			// uvModMainTex = uvModMainTex.x / _SinTime;
			// uvModMainTex = uvModMainTex.y / _CosTime;
			// float2 uvModMainTex2 = IN.uv_MainTex2 - _Time;
			// uvModMainTex2 = uvModMainTex2.x / _CosTime;
			// uvModMainTex2 = uvModMainTex2.y / _SinTime;

			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 c2 = tex2D (_MainTex2, IN.uv_MainTex2);
			//half mod = ((sin(_Time * 100) + 1)/2);
			half mod = _MainTexLerp;
			fixed4 col = lerp(c,c2, mod);
			//fixed4 col = lerp(c,c2, _MainTexLerp);
			o.Albedo = col * _Color;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Specular = _Shininess;
			o.Gloss = col.a;
			o.Alpha = 1.0;

			fixed4 e = tex2D (_Emission, IN.uv_Emission) * _EmissionColor;
			o.Emission = e * _EmissionMagnitude;

			fixed3 n = UnpackNormal( tex2D(_Normal, IN.uv_Normal));
			o.Normal = n;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
