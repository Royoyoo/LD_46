// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_SpecColor("Specular Color",Color)=(1,1,1,1)
		_DeepColor("DeepColor", Color) = (0.07097722,0.08316531,0.2735849,1)
		_ShallowColor("ShallowColor", Color) = (1,1,1,1)
		_FoamColor("FoamColor", Color) = (1,1,1,1)
		_Depth("Depth", Float) = 3
		_Foam("Foam", Float) = 0.1
		_Spec("Spec", Range( 0 , 1)) = 0.1
		_Gloss("Gloss", Range( 0 , 1)) = 0.1
		_WaveSpeed("WaveSpeed", Range( 0 , 1)) = 0.1
		_Normals("Normals", Range( 0 , 1)) = 0.1
		_SinSpeed("SinSpeed", Float) = 1
		_waves_tex("waves_tex", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _waves_tex;
		uniform float _WaveSpeed;
		uniform float _Normals;
		uniform float4 _ShallowColor;
		uniform float4 _DeepColor;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depth;
		uniform float4 _FoamColor;
		uniform float _Foam;
		uniform float _SinSpeed;
		uniform float _Spec;
		uniform float _Gloss;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime32 = _Time.y * 0.1;
			float mulTime33 = _Time.y * 0.02;
			float2 appendResult31 = (float2(mulTime32 , mulTime33));
			float2 uv_TexCoord30 = v.texcoord.xy * float2( 2,6 ) + ( appendResult31 * _WaveSpeed );
			float4 tex2DNode29 = tex2Dlod( _waves_tex, float4( uv_TexCoord30, 0, 0.0) );
			float3 appendResult36 = (float3(tex2DNode29.r , tex2DNode29.g , tex2DNode29.b));
			v.normal = appendResult36;
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float mulTime32 = _Time.y * 0.1;
			float mulTime33 = _Time.y * 0.02;
			float2 appendResult31 = (float2(mulTime32 , mulTime33));
			float2 uv_TexCoord30 = i.uv_texcoord * float2( 2,6 ) + ( appendResult31 * _WaveSpeed );
			float4 tex2DNode29 = tex2D( _waves_tex, uv_TexCoord30 );
			float3 appendResult36 = (float3(tex2DNode29.r , tex2DNode29.g , tex2DNode29.b));
			float3 lerpResult41 = lerp( appendResult36 , float3( 0,0,1 ) , _Normals);
			o.Normal = lerpResult41;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth9 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth9 = abs( ( screenDepth9 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float clampResult14 = clamp( distanceDepth9 , 0.0 , 1.0 );
			float4 lerpResult13 = lerp( _ShallowColor , _DeepColor , clampResult14);
			o.Albedo = lerpResult13.rgb;
			float mulTime25 = _Time.y * _SinSpeed;
			float clampResult19 = clamp( (0.0 + (clampResult14 - 0.0) * (1.0 - 0.0) / (( _Foam * (0.5 + (sin( mulTime25 ) - -1.0) * (1.0 - 0.5) / (1.0 - -1.0)) ) - 0.0)) , 0.0 , 1.0 );
			float4 lerpResult16 = lerp( _FoamColor , float4( 0,0,0,0 ) , clampResult19);
			o.Emission = lerpResult16.rgb;
			o.Specular = _Spec;
			o.Gloss = _Gloss;
			float clampResult39 = clamp( distanceDepth9 , 0.5 , 1.0 );
			o.Alpha = clampResult39;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
1138.4;512;1784;1095;1444.025;244.7486;1.3;True;False
Node;AmplifyShaderEditor.RangedFloatNode;26;-1457.419,604.7467;Inherit;False;Property;_SinSpeed;SinSpeed;10;0;Create;True;0;0;False;0;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;25;-1249.555,613.061;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;33;-1172.513,908.3793;Inherit;False;1;0;FLOAT;0.02;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;32;-1171.007,819.5616;Inherit;False;1;0;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1101.081,306.6106;Inherit;False;Property;_Depth;Depth;4;0;Create;True;0;0;False;0;3;2.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;24;-1009.621,615.4366;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;31;-947.3785,841.8667;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1037.125,1021.452;Inherit;False;Property;_WaveSpeed;WaveSpeed;8;0;Create;True;0;0;False;0;0.1;0.12;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;9;-882.5286,285.2301;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;22;-832.6399,615.4364;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-792.2549,469.3382;Inherit;False;Property;_Foam;Foam;5;0;Create;True;0;0;False;0;0.1;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-726.4251,834.2515;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;14;-593.8933,284.0424;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-502.6137,780.4213;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,6;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-579.6397,515.6617;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;17;-437.2167,431.3287;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-242.6224,753.9789;Inherit;True;Property;_waves_tex;waves_tex;11;0;Create;True;0;0;False;0;-1;4a2537aa45f9ded449d9360f4808953b;4a2537aa45f9ded449d9360f4808953b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;19;-243.494,431.3287;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;15;-343.2684,231.7795;Inherit;False;Property;_FoamColor;FoamColor;3;0;Create;True;0;0;False;0;1,1,1,1;0.5471698,0.5471698,0.5471698,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-750.6823,51.23477;Inherit;False;Property;_DeepColor;DeepColor;1;0;Create;True;0;0;False;0;0.07097722,0.08316531,0.2735849,1;0.1446244,0.174822,0.245283,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;36;66.57518,782.2514;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;10;-752.8997,-149.278;Inherit;False;Property;_ShallowColor;ShallowColor;2;0;Create;True;0;0;False;0;1,1,1,1;0.28,0.4248707,0.5882353,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;42;-240.2251,601.5515;Inherit;False;Property;_Normals;Normals;9;0;Create;True;0;0;False;0;0.1;0.346;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-67.73724,119.5959;Inherit;False;Property;_Spec;Spec;6;0;Create;True;0;0;False;0;0.1;0.457;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;13;-335.2248,0.0008640289;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;41;126.375,527.4512;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,1;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-64.41212,205.2215;Inherit;False;Property;_Gloss;Gloss;7;0;Create;True;0;0;False;0;0.1;0.045;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;39;-63.42498,293.4514;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;16;96.21513,358.8732;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;430.4461,66.17985;Float;False;True;-1;2;ASEMaterialInspector;0;0;BlinnPhong;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;0;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;26;0
WireConnection;24;0;25;0
WireConnection;31;0;32;0
WireConnection;31;1;33;0
WireConnection;9;0;12;0
WireConnection;22;0;24;0
WireConnection;38;0;31;0
WireConnection;38;1;37;0
WireConnection;14;0;9;0
WireConnection;30;1;38;0
WireConnection;21;0;18;0
WireConnection;21;1;22;0
WireConnection;17;0;14;0
WireConnection;17;2;21;0
WireConnection;29;1;30;0
WireConnection;19;0;17;0
WireConnection;36;0;29;1
WireConnection;36;1;29;2
WireConnection;36;2;29;3
WireConnection;13;0;10;0
WireConnection;13;1;11;0
WireConnection;13;2;14;0
WireConnection;41;0;36;0
WireConnection;41;2;42;0
WireConnection;39;0;9;0
WireConnection;16;0;15;0
WireConnection;16;2;19;0
WireConnection;0;0;13;0
WireConnection;0;1;41;0
WireConnection;0;2;16;0
WireConnection;0;3;27;0
WireConnection;0;4;28;0
WireConnection;0;9;39;0
WireConnection;0;12;36;0
ASEEND*/
//CHKSM=7773F08EF950222EB276F2A9708448AF9ABB26C1