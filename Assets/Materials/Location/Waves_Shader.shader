// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waves_Shader"
{
	Properties
	{
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_ShallowColor("ShallowColor", Color) = (0.1466263,0.6893123,0.8308824,1)
		_DeepColor("DeepColor", Color) = (0.1466263,0.6893123,0.8308824,1)
		_Depth("Depth", Float) = 5
		_Base_Waves("Base_Waves", 2D) = "gray" {}
		_Wave_Height("Wave_Height", Float) = 10
		_Offsets("Offsets", Vector) = (0,0,0,0)
		_FoamColor("FoamColor", Color) = (1,1,1,1)
		_FoamDistance("FoamDistance", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True" }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 3.5
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 screenPos;
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float4 _FoamColor;
		uniform float4 _ShallowColor;
		uniform float4 _DeepColor;
		uniform sampler2D _CameraDepthTexture;
		uniform float _Depth;
		uniform float _FoamDistance;
		uniform float _Smoothness;
		uniform sampler2D _Base_Waves;
		uniform float2 _Offsets;
		uniform float _Wave_Height;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_TexCoord17 = v.texcoord.xy * float2( 1,1 ) + _Offsets;
			float4 tex2DNode2 = tex2Dlod( _Base_Waves, float4( uv_TexCoord17, 0, 0) );
			float3 appendResult5 = (float3(0.0 , ( tex2DNode2.a * _Wave_Height ) , 0.0));
			v.vertex.xyz += appendResult5;
			float3 appendResult35 = (float3((-1 + (tex2DNode2.r - 0) * (1 - -1) / (1 - 0)) , tex2DNode2.b , (-1 + (tex2DNode2.g - 0) * (1 - -1) / (1 - 0))));
			float3 normalizeResult41 = normalize( appendResult35 );
			v.normal = normalizeResult41;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth21 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float distanceDepth21 = abs( ( screenDepth21 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float clampResult25 = clamp( distanceDepth21 , 0 , 1 );
			float4 lerpResult23 = lerp( _ShallowColor , _DeepColor , clampResult25);
			float screenDepth30 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float distanceDepth30 = abs( ( screenDepth30 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _FoamDistance ) );
			float clampResult31 = clamp( distanceDepth30 , 0 , 1 );
			float4 lerpResult33 = lerp( _FoamColor , lerpResult23 , clampResult31);
			o.Albedo = lerpResult33.rgb;
			o.Smoothness = _Smoothness;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float2 uv_TexCoord17 = i.uv_texcoord * float2( 1,1 ) + _Offsets;
			float4 tex2DNode2 = tex2D( _Base_Waves, uv_TexCoord17 );
			float3 appendResult35 = (float3((-1 + (tex2DNode2.r - 0) * (1 - -1) / (1 - 0)) , tex2DNode2.b , (-1 + (tex2DNode2.g - 0) * (1 - -1) / (1 - 0))));
			float3 normalizeResult41 = normalize( appendResult35 );
			float dotResult44 = dot( ase_worldlightDir , normalizeResult41 );
			o.Occlusion = (0 + (dotResult44 - -1) * (1 - 0) / (1 - -1));
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15001
0;92;1115;375;1236.88;377.9865;3.247221;True;False
Node;AmplifyShaderEditor.Vector2Node;16;-724.8691,479.0403;Float;False;Property;_Offsets;Offsets;6;0;Create;True;0;0;False;0;0,0;0.8634118,0.4317059;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-454.268,437.4395;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-107.243,408.7877;Float;True;Property;_Base_Waves;Base_Waves;4;0;Create;True;0;0;False;0;None;4a2537aa45f9ded449d9360f4808953b;True;0;False;gray;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;36;238.4474,414.2817;Float;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;22;-64.43089,224.7138;Float;False;Property;_Depth;Depth;3;0;Create;True;0;0;False;0;5;6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;38;575.384,530.5176;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;37;571.384,332.5137;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;35;822.5261,438.2325;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DepthFade;21;93.1379,228.9773;Float;False;True;1;0;FLOAT;9.56;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-195.6165,-521.5099;Float;False;Property;_FoamDistance;FoamDistance;8;0;Create;True;0;0;False;0;5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;350.2925,834.8608;Float;False;Property;_Wave_Height;Wave_Height;5;0;Create;True;0;0;False;0;10;1.4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;25;319.9247,228.619;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;189.148,-166.2599;Float;False;Property;_ShallowColor;ShallowColor;1;0;Create;True;0;0;False;0;0.1466263,0.6893123,0.8308824,1;0.09758868,0.5493302,0.6985294,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;30;67.11964,-515.224;Float;False;True;1;0;FLOAT;9.56;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;172.432,25.22364;Float;False;Property;_DeepColor;DeepColor;2;0;Create;True;0;0;False;0;0.1466263,0.6893123,0.8308824,1;0.03584559,0.1517875,0.2867647,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;41;974.1576,429.2783;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;42;812.6694,175.9099;Float;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;4;670.952,924.1439;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;27;195.8398,-359.1919;Float;False;Property;_FoamColor;FoamColor;7;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;31;293.9059,-515.5823;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;620.6911,806.2556;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;44;1158.667,264.7049;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;23;560.3982,-96.44283;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;33;802.0945,-282.1609;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;40;828.459,-68.507;Float;True;Property;_SecondaryWaves;SecondaryWaves;9;0;Create;True;0;0;False;0;None;4a2537aa45f9ded449d9360f4808953b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;5;914.7156,787.1302;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;45;1299.302,262.7383;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;1370.629,151.9033;Float;False;Property;_Smoothness;Smoothness;0;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1729.266,115.8634;Float;False;True;3;Float;ASEMaterialInspector;0;0;Standard;Waves_Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;1;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;0;15;False;0.48;True;2;SrcAlpha;OneMinusSrcAlpha;0;One;DstColor;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;1;16;0
WireConnection;2;1;17;0
WireConnection;36;0;2;0
WireConnection;38;0;36;1
WireConnection;37;0;36;0
WireConnection;35;0;37;0
WireConnection;35;1;36;2
WireConnection;35;2;38;0
WireConnection;21;0;22;0
WireConnection;25;0;21;0
WireConnection;30;0;32;0
WireConnection;41;0;35;0
WireConnection;31;0;30;0
WireConnection;7;0;2;4
WireConnection;7;1;6;0
WireConnection;44;0;42;0
WireConnection;44;1;41;0
WireConnection;23;0;1;0
WireConnection;23;1;24;0
WireConnection;23;2;25;0
WireConnection;33;0;27;0
WireConnection;33;1;23;0
WireConnection;33;2;31;0
WireConnection;5;0;4;0
WireConnection;5;1;7;0
WireConnection;5;2;4;0
WireConnection;45;0;44;0
WireConnection;0;0;33;0
WireConnection;0;4;26;0
WireConnection;0;5;45;0
WireConnection;0;11;5;0
WireConnection;0;12;41;0
ASEEND*/
//CHKSM=05B956714DCF01BDEA9EF88DD2901BD3BFE1DBC1