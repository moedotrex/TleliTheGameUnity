Shader "Unlit Master UI Flame"
{
	Properties
	{
		[NoScaleOffset] _MainTex("Main Tex", 2D) = "white" {}
		_LevelUI("Level", Range(0, 100)) = 100
		_ColorUI("Color", Color) = (1, 0.2987257, 0, 0)
		_DistortionUI("Distortion", Range(0, 1)) = 0.61

		_Stencil("Stencil ID", Float) = 0
		_StencilComp("StencilComp", Float) = 8
		_StencilOp("StencilOp", Float) = 0
		_StencilReadMask("StencilReadMask", Float) = 255
		_StencilWriteMask("StencilWriteMask", Float) = 255
		_ColorMask("ColorMask", Float) = 15

	}
		SubShader
		{
			Tags
			{
				"RenderPipeline" = "UniversalPipeline"
				"RenderType" = "Transparent"
				"Queue" = "Transparent+0"
			}

			Pass
			{
				Name "Pass"
				Tags
				{
			// LightMode: <None>
		}

		// Render State
		Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
		Cull Back
		ZTest[unity_GUIZTestMode]
		ZWrite Off
			// ColorMask: <None>

		Stencil{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
			}
			ColorMask[_ColorMask]

			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Debug
			// <None>

			// --------------------------------------------------
			// Pass

			// Pragmas
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0
			#pragma multi_compile_fog
			#pragma multi_compile_instancing

			// Keywords
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma shader_feature _ _SAMPLE_GI
			// GraphKeywords: <None>

			// Defines
			#define _SURFACE_TYPE_TRANSPARENT 1
			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define ATTRIBUTES_NEED_TEXCOORD0
			#define ATTRIBUTES_NEED_COLOR
			#define VARYINGS_NEED_TEXCOORD0
			#define VARYINGS_NEED_COLOR
			#define SHADERPASS_UNLIT

			// Includes
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

			// --------------------------------------------------
			// Graph

			// Graph Properties
			CBUFFER_START(UnityPerMaterial)
			float _LevelUI;
			float4 _ColorUI;
			float _DistortionUI;
			CBUFFER_END
			TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex); float4 _MainTex_TexelSize;
			SAMPLER(_SampleTexture2D_24179E5C_Sampler_3_Linear_Repeat);

			// Graph Functions

			void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
			{
				Out = A * B;
			}

			void Unity_Preview_float(float In, out float Out)
			{
				Out = In;
			}

			void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
			{
				Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
			}

			void Unity_Subtract_float(float A, float B, out float Out)
			{
				Out = A - B;
			}

			void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
			{
				Out = A * B;
			}

			void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
			{
				Out = UV * Tiling + Offset;
			}


			float2 Unity_GradientNoise_Dir_float(float2 p)
			{
				// Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
				p = p % 289;
				float x = (34 * p.x + 1) * p.x % 289 + p.y;
				x = (34 * x + 1) * x % 289;
				x = frac(x / 41) * 2 - 1;
				return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
			}

			void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
			{
				float2 p = UV * Scale;
				float2 ip = floor(p);
				float2 fp = frac(p);
				float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
				float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
				float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
				float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
				fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
				Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
			}


			inline float2 Unity_Voronoi_RandomVector_float(float2 UV, float offset)
			{
				float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
				UV = frac(sin(mul(UV, m)) * 46839.32);
				return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
			}

			void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
			{
				float2 g = floor(UV * CellDensity);
				float2 f = frac(UV * CellDensity);
				float t = 8.0;
				float3 res = float3(8.0, 0.0, 0.0);

				for (int y = -1; y <= 1; y++)
				{
					for (int x = -1; x <= 1; x++)
					{
						float2 lattice = float2(x,y);
						float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
						float d = distance(lattice + offset, f);

						if (d < res.x)
						{
							res = float3(d, offset.x, offset.y);
							Out = res.x;
							Cells = res.y;
						}
					}
				}
			}

			void Unity_Power_float(float A, float B, out float Out)
			{
				Out = pow(A, B);
			}

			void Unity_Multiply_float(float A, float B, out float Out)
			{
				Out = A * B;
			}

			void Unity_Clamp_float(float In, float Min, float Max, out float Out)
			{
				Out = clamp(In, Min, Max);
			}

			void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
			{
				Out = smoothstep(Edge1, Edge2, In);
			}

			void Unity_Flip_float(float In, float Flip, out float Out)
			{
				Out = (Flip * -2 + 1) * In;
			}

			void Unity_Lerp_float(float A, float B, float T, out float Out)
			{
				Out = lerp(A, B, T);
			}

			// Graph Vertex
			// GraphVertex: <None>

			// Graph Pixel
			struct SurfaceDescriptionInputs
			{
				float4 uv0;
				float4 VertexColor;
				float3 TimeParameters;
			};

			struct SurfaceDescription
			{
				float3 Color;
				float Alpha;
				float AlphaClipThreshold;
			};

			SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
			{
				SurfaceDescription surface = (SurfaceDescription)0;
				float4 _Property_5986B554_Out_0 = _ColorUI;
				float4 _Multiply_20A3A887_Out_2;
				Unity_Multiply_float(IN.VertexColor, _Property_5986B554_Out_0, _Multiply_20A3A887_Out_2);
				float4 _UV_A3FF9E13_Out_0 = IN.uv0;
				float _Split_2D81FF4C_R_1 = _UV_A3FF9E13_Out_0[0];
				float _Split_2D81FF4C_G_2 = _UV_A3FF9E13_Out_0[1];
				float _Split_2D81FF4C_B_3 = _UV_A3FF9E13_Out_0[2];
				float _Split_2D81FF4C_A_4 = _UV_A3FF9E13_Out_0[3];
				float _Preview_4BD64336_Out_1;
				Unity_Preview_float(_Split_2D81FF4C_G_2, _Preview_4BD64336_Out_1);
				float _Property_E4879BA6_Out_0 = _LevelUI;
				float _Remap_D94D54AB_Out_3;
				Unity_Remap_float(_Property_E4879BA6_Out_0, float2 (0, 100), float2 (0, 1), _Remap_D94D54AB_Out_3);
				float _Subtract_D861869C_Out_2;
				Unity_Subtract_float(_Preview_4BD64336_Out_1, _Remap_D94D54AB_Out_3, _Subtract_D861869C_Out_2);
				float2 _Vector2_3F56602_Out_0 = float2(0.1, -0.35);
				float2 _Multiply_2A9A7FFA_Out_2;
				Unity_Multiply_float(_Vector2_3F56602_Out_0, (IN.TimeParameters.x.xx), _Multiply_2A9A7FFA_Out_2);
				float2 _TilingAndOffset_C7EDED72_Out_3;
				Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_2A9A7FFA_Out_2, _TilingAndOffset_C7EDED72_Out_3);
				float _Vector1_7852A19C_Out_0 = 5;
				float _GradientNoise_4DCB19BE_Out_2;
				Unity_GradientNoise_float(_TilingAndOffset_C7EDED72_Out_3, _Vector1_7852A19C_Out_0, _GradientNoise_4DCB19BE_Out_2);
				float2 _Vector2_50642A88_Out_0 = float2(-0.1, -0.3);
				float2 _Multiply_E7975680_Out_2;
				Unity_Multiply_float(_Vector2_50642A88_Out_0, (IN.TimeParameters.x.xx), _Multiply_E7975680_Out_2);
				float2 _TilingAndOffset_90ED16CE_Out_3;
				Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_E7975680_Out_2, _TilingAndOffset_90ED16CE_Out_3);
				float _Vector1_EB5CC39C_Out_0 = 6;
				float _Voronoi_49F98ABC_Out_3;
				float _Voronoi_49F98ABC_Cells_4;
				Unity_Voronoi_float(_TilingAndOffset_90ED16CE_Out_3, 2.5, _Vector1_EB5CC39C_Out_0, _Voronoi_49F98ABC_Out_3, _Voronoi_49F98ABC_Cells_4);
				float _Vector1_3C7C2399_Out_0 = 0.75;
				float _Power_F12441A2_Out_2;
				Unity_Power_float(_Voronoi_49F98ABC_Out_3, _Vector1_3C7C2399_Out_0, _Power_F12441A2_Out_2);
				float _Multiply_B1CE1EB4_Out_2;
				Unity_Multiply_float(_GradientNoise_4DCB19BE_Out_2, _Power_F12441A2_Out_2, _Multiply_B1CE1EB4_Out_2);
				float _Clamp_38746272_Out_3;
				Unity_Clamp_float(_Multiply_B1CE1EB4_Out_2, 0, 1, _Clamp_38746272_Out_3);
				float _Preview_E7B424C_Out_1;
				Unity_Preview_float(_Clamp_38746272_Out_3, _Preview_E7B424C_Out_1);
				float _Smoothstep_1B1A15AF_Out_3;
				Unity_Smoothstep_float(_Subtract_D861869C_Out_2, 1.7, _Preview_E7B424C_Out_1, _Smoothstep_1B1A15AF_Out_3);
				float _Flip_B243F868_Out_1;
				float _Flip_B243F868_Flip = float(1
			);    Unity_Flip_float(_Subtract_D861869C_Out_2, _Flip_B243F868_Flip, _Flip_B243F868_Out_1);
				float _Multiply_85399D70_Out_2;
				Unity_Multiply_float(_Flip_B243F868_Out_1, _Preview_E7B424C_Out_1, _Multiply_85399D70_Out_2);
				float _Lerp_8528632F_Out_3;
				Unity_Lerp_float(_Multiply_85399D70_Out_2, _Flip_B243F868_Out_1, 0.6, _Lerp_8528632F_Out_3);
				float _Property_AFCB5334_Out_0 = _DistortionUI;
				float _Lerp_5B4F005C_Out_3;
				Unity_Lerp_float(_Smoothstep_1B1A15AF_Out_3, _Lerp_8528632F_Out_3, _Property_AFCB5334_Out_0, _Lerp_5B4F005C_Out_3);
				float4 _SampleTexture2D_24179E5C_RGBA_0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv0.xy);
				float _SampleTexture2D_24179E5C_R_4 = _SampleTexture2D_24179E5C_RGBA_0.r;
				float _SampleTexture2D_24179E5C_G_5 = _SampleTexture2D_24179E5C_RGBA_0.g;
				float _SampleTexture2D_24179E5C_B_6 = _SampleTexture2D_24179E5C_RGBA_0.b;
				float _SampleTexture2D_24179E5C_A_7 = _SampleTexture2D_24179E5C_RGBA_0.a;
				float _Multiply_F1200F5B_Out_2;
				Unity_Multiply_float(_Lerp_5B4F005C_Out_3, _SampleTexture2D_24179E5C_R_4, _Multiply_F1200F5B_Out_2);
				float _Multiply_C2827FA1_Out_2;
				Unity_Multiply_float(_Multiply_F1200F5B_Out_2, 4, _Multiply_C2827FA1_Out_2);
				float _Clamp_9C05ACD4_Out_3;
				Unity_Clamp_float(_Multiply_C2827FA1_Out_2, 0, 1, _Clamp_9C05ACD4_Out_3);
				surface.Color = (_Multiply_20A3A887_Out_2.xyz);
				surface.Alpha = _Clamp_9C05ACD4_Out_3;
				surface.AlphaClipThreshold = 0;
				return surface;
			}

			// --------------------------------------------------
			// Structs and Packing

			// Generated Type: Attributes
			struct Attributes
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 tangentOS : TANGENT;
				float4 uv0 : TEXCOORD0;
				float4 color : COLOR;
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : INSTANCEID_SEMANTIC;
				#endif
			};

			// Generated Type: Varyings
			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float4 texCoord0;
				float4 color;
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : CUSTOM_INSTANCE_ID;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
				#endif
			};

			// Generated Type: PackedVaryings
			struct PackedVaryings
			{
				float4 positionCS : SV_POSITION;
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : CUSTOM_INSTANCE_ID;
				#endif
				float4 interp00 : TEXCOORD0;
				float4 interp01 : TEXCOORD1;
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
				#endif
			};

			// Packed Type: Varyings
			PackedVaryings PackVaryings(Varyings input)
			{
				PackedVaryings output = (PackedVaryings)0;
				output.positionCS = input.positionCS;
				output.interp00.xyzw = input.texCoord0;
				output.interp01.xyzw = input.color;
				#if UNITY_ANY_INSTANCING_ENABLED
				output.instanceID = input.instanceID;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				output.cullFace = input.cullFace;
				#endif
				return output;
			}

			// Unpacked Type: Varyings
			Varyings UnpackVaryings(PackedVaryings input)
			{
				Varyings output = (Varyings)0;
				output.positionCS = input.positionCS;
				output.texCoord0 = input.interp00.xyzw;
				output.color = input.interp01.xyzw;
				#if UNITY_ANY_INSTANCING_ENABLED
				output.instanceID = input.instanceID;
				#endif
				#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
				output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
				#endif
				#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
				output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
				#endif
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				output.cullFace = input.cullFace;
				#endif
				return output;
			}

			// --------------------------------------------------
			// Build Graph Inputs

			SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
			{
				SurfaceDescriptionInputs output;
				ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





				output.uv0 = input.texCoord0;
				output.VertexColor = input.color;
				output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
			#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
			#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
			#else
			#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
			#endif
			#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

				return output;
			}


			// --------------------------------------------------
			// Main

			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"

			ENDHLSL
		}

		Pass
		{
			Name "ShadowCaster"
			Tags
			{
				"LightMode" = "ShadowCaster"
			}

				// Render State
				Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
				Cull Back
				ZTest LEqual
				ZWrite On
				// ColorMask: <None>


				HLSLPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				// Debug
				// <None>

				// --------------------------------------------------
				// Pass

				// Pragmas
				#pragma prefer_hlslcc gles
				#pragma exclude_renderers d3d11_9x
				#pragma target 2.0
				#pragma multi_compile_instancing

				// Keywords
				#pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
				// GraphKeywords: <None>

				// Defines
				#define _SURFACE_TYPE_TRANSPARENT 1
				#define ATTRIBUTES_NEED_NORMAL
				#define ATTRIBUTES_NEED_TANGENT
				#define ATTRIBUTES_NEED_TEXCOORD0
				#define VARYINGS_NEED_TEXCOORD0
				#define SHADERPASS_SHADOWCASTER

				// Includes
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
				#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

				// --------------------------------------------------
				// Graph

				// Graph Properties
				CBUFFER_START(UnityPerMaterial)
				float _LevelUI;
				float4 _ColorUI;
				float _DistortionUI;
				CBUFFER_END
				TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex); float4 _MainTex_TexelSize;
				SAMPLER(_SampleTexture2D_24179E5C_Sampler_3_Linear_Repeat);

				// Graph Functions

				void Unity_Preview_float(float In, out float Out)
				{
					Out = In;
				}

				void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
				{
					Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
				}

				void Unity_Subtract_float(float A, float B, out float Out)
				{
					Out = A - B;
				}

				void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
				{
					Out = A * B;
				}

				void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
				{
					Out = UV * Tiling + Offset;
				}


				float2 Unity_GradientNoise_Dir_float(float2 p)
				{
					// Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
					p = p % 289;
					float x = (34 * p.x + 1) * p.x % 289 + p.y;
					x = (34 * x + 1) * x % 289;
					x = frac(x / 41) * 2 - 1;
					return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
				}

				void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
				{
					float2 p = UV * Scale;
					float2 ip = floor(p);
					float2 fp = frac(p);
					float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
					float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
					float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
					float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
					fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
					Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
				}


				inline float2 Unity_Voronoi_RandomVector_float(float2 UV, float offset)
				{
					float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
					UV = frac(sin(mul(UV, m)) * 46839.32);
					return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
				}

				void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
				{
					float2 g = floor(UV * CellDensity);
					float2 f = frac(UV * CellDensity);
					float t = 8.0;
					float3 res = float3(8.0, 0.0, 0.0);

					for (int y = -1; y <= 1; y++)
					{
						for (int x = -1; x <= 1; x++)
						{
							float2 lattice = float2(x,y);
							float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
							float d = distance(lattice + offset, f);

							if (d < res.x)
							{
								res = float3(d, offset.x, offset.y);
								Out = res.x;
								Cells = res.y;
							}
						}
					}
				}

				void Unity_Power_float(float A, float B, out float Out)
				{
					Out = pow(A, B);
				}

				void Unity_Multiply_float(float A, float B, out float Out)
				{
					Out = A * B;
				}

				void Unity_Clamp_float(float In, float Min, float Max, out float Out)
				{
					Out = clamp(In, Min, Max);
				}

				void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
				{
					Out = smoothstep(Edge1, Edge2, In);
				}

				void Unity_Flip_float(float In, float Flip, out float Out)
				{
					Out = (Flip * -2 + 1) * In;
				}

				void Unity_Lerp_float(float A, float B, float T, out float Out)
				{
					Out = lerp(A, B, T);
				}

				// Graph Vertex
				// GraphVertex: <None>

				// Graph Pixel
				struct SurfaceDescriptionInputs
				{
					float4 uv0;
					float3 TimeParameters;
				};

				struct SurfaceDescription
				{
					float Alpha;
					float AlphaClipThreshold;
				};

				SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
				{
					SurfaceDescription surface = (SurfaceDescription)0;
					float4 _UV_A3FF9E13_Out_0 = IN.uv0;
					float _Split_2D81FF4C_R_1 = _UV_A3FF9E13_Out_0[0];
					float _Split_2D81FF4C_G_2 = _UV_A3FF9E13_Out_0[1];
					float _Split_2D81FF4C_B_3 = _UV_A3FF9E13_Out_0[2];
					float _Split_2D81FF4C_A_4 = _UV_A3FF9E13_Out_0[3];
					float _Preview_4BD64336_Out_1;
					Unity_Preview_float(_Split_2D81FF4C_G_2, _Preview_4BD64336_Out_1);
					float _Property_E4879BA6_Out_0 = _LevelUI;
					float _Remap_D94D54AB_Out_3;
					Unity_Remap_float(_Property_E4879BA6_Out_0, float2 (0, 100), float2 (0, 1), _Remap_D94D54AB_Out_3);
					float _Subtract_D861869C_Out_2;
					Unity_Subtract_float(_Preview_4BD64336_Out_1, _Remap_D94D54AB_Out_3, _Subtract_D861869C_Out_2);
					float2 _Vector2_3F56602_Out_0 = float2(0.1, -0.35);
					float2 _Multiply_2A9A7FFA_Out_2;
					Unity_Multiply_float(_Vector2_3F56602_Out_0, (IN.TimeParameters.x.xx), _Multiply_2A9A7FFA_Out_2);
					float2 _TilingAndOffset_C7EDED72_Out_3;
					Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_2A9A7FFA_Out_2, _TilingAndOffset_C7EDED72_Out_3);
					float _Vector1_7852A19C_Out_0 = 5;
					float _GradientNoise_4DCB19BE_Out_2;
					Unity_GradientNoise_float(_TilingAndOffset_C7EDED72_Out_3, _Vector1_7852A19C_Out_0, _GradientNoise_4DCB19BE_Out_2);
					float2 _Vector2_50642A88_Out_0 = float2(-0.1, -0.3);
					float2 _Multiply_E7975680_Out_2;
					Unity_Multiply_float(_Vector2_50642A88_Out_0, (IN.TimeParameters.x.xx), _Multiply_E7975680_Out_2);
					float2 _TilingAndOffset_90ED16CE_Out_3;
					Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_E7975680_Out_2, _TilingAndOffset_90ED16CE_Out_3);
					float _Vector1_EB5CC39C_Out_0 = 6;
					float _Voronoi_49F98ABC_Out_3;
					float _Voronoi_49F98ABC_Cells_4;
					Unity_Voronoi_float(_TilingAndOffset_90ED16CE_Out_3, 2.5, _Vector1_EB5CC39C_Out_0, _Voronoi_49F98ABC_Out_3, _Voronoi_49F98ABC_Cells_4);
					float _Vector1_3C7C2399_Out_0 = 0.75;
					float _Power_F12441A2_Out_2;
					Unity_Power_float(_Voronoi_49F98ABC_Out_3, _Vector1_3C7C2399_Out_0, _Power_F12441A2_Out_2);
					float _Multiply_B1CE1EB4_Out_2;
					Unity_Multiply_float(_GradientNoise_4DCB19BE_Out_2, _Power_F12441A2_Out_2, _Multiply_B1CE1EB4_Out_2);
					float _Clamp_38746272_Out_3;
					Unity_Clamp_float(_Multiply_B1CE1EB4_Out_2, 0, 1, _Clamp_38746272_Out_3);
					float _Preview_E7B424C_Out_1;
					Unity_Preview_float(_Clamp_38746272_Out_3, _Preview_E7B424C_Out_1);
					float _Smoothstep_1B1A15AF_Out_3;
					Unity_Smoothstep_float(_Subtract_D861869C_Out_2, 1.7, _Preview_E7B424C_Out_1, _Smoothstep_1B1A15AF_Out_3);
					float _Flip_B243F868_Out_1;
					float _Flip_B243F868_Flip = float(1
				);    Unity_Flip_float(_Subtract_D861869C_Out_2, _Flip_B243F868_Flip, _Flip_B243F868_Out_1);
					float _Multiply_85399D70_Out_2;
					Unity_Multiply_float(_Flip_B243F868_Out_1, _Preview_E7B424C_Out_1, _Multiply_85399D70_Out_2);
					float _Lerp_8528632F_Out_3;
					Unity_Lerp_float(_Multiply_85399D70_Out_2, _Flip_B243F868_Out_1, 0.6, _Lerp_8528632F_Out_3);
					float _Property_AFCB5334_Out_0 = _DistortionUI;
					float _Lerp_5B4F005C_Out_3;
					Unity_Lerp_float(_Smoothstep_1B1A15AF_Out_3, _Lerp_8528632F_Out_3, _Property_AFCB5334_Out_0, _Lerp_5B4F005C_Out_3);
					float4 _SampleTexture2D_24179E5C_RGBA_0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv0.xy);
					float _SampleTexture2D_24179E5C_R_4 = _SampleTexture2D_24179E5C_RGBA_0.r;
					float _SampleTexture2D_24179E5C_G_5 = _SampleTexture2D_24179E5C_RGBA_0.g;
					float _SampleTexture2D_24179E5C_B_6 = _SampleTexture2D_24179E5C_RGBA_0.b;
					float _SampleTexture2D_24179E5C_A_7 = _SampleTexture2D_24179E5C_RGBA_0.a;
					float _Multiply_F1200F5B_Out_2;
					Unity_Multiply_float(_Lerp_5B4F005C_Out_3, _SampleTexture2D_24179E5C_R_4, _Multiply_F1200F5B_Out_2);
					float _Multiply_C2827FA1_Out_2;
					Unity_Multiply_float(_Multiply_F1200F5B_Out_2, 4, _Multiply_C2827FA1_Out_2);
					float _Clamp_9C05ACD4_Out_3;
					Unity_Clamp_float(_Multiply_C2827FA1_Out_2, 0, 1, _Clamp_9C05ACD4_Out_3);
					surface.Alpha = _Clamp_9C05ACD4_Out_3;
					surface.AlphaClipThreshold = 0;
					return surface;
				}

				// --------------------------------------------------
				// Structs and Packing

				// Generated Type: Attributes
				struct Attributes
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					float4 tangentOS : TANGENT;
					float4 uv0 : TEXCOORD0;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

				// Generated Type: Varyings
				struct Varyings
				{
					float4 positionCS : SV_POSITION;
					float4 texCoord0;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : CUSTOM_INSTANCE_ID;
					#endif
					#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
					#endif
					#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
					#endif
					#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
					#endif
				};

				// Generated Type: PackedVaryings
				struct PackedVaryings
				{
					float4 positionCS : SV_POSITION;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : CUSTOM_INSTANCE_ID;
					#endif
					float4 interp00 : TEXCOORD0;
					#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
					#endif
					#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
					#endif
					#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
					#endif
				};

				// Packed Type: Varyings
				PackedVaryings PackVaryings(Varyings input)
				{
					PackedVaryings output = (PackedVaryings)0;
					output.positionCS = input.positionCS;
					output.interp00.xyzw = input.texCoord0;
					#if UNITY_ANY_INSTANCING_ENABLED
					output.instanceID = input.instanceID;
					#endif
					#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
					#endif
					#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
					#endif
					#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					output.cullFace = input.cullFace;
					#endif
					return output;
				}

				// Unpacked Type: Varyings
				Varyings UnpackVaryings(PackedVaryings input)
				{
					Varyings output = (Varyings)0;
					output.positionCS = input.positionCS;
					output.texCoord0 = input.interp00.xyzw;
					#if UNITY_ANY_INSTANCING_ENABLED
					output.instanceID = input.instanceID;
					#endif
					#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
					output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
					#endif
					#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
					output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
					#endif
					#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					output.cullFace = input.cullFace;
					#endif
					return output;
				}

				// --------------------------------------------------
				// Build Graph Inputs

				SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
				{
					SurfaceDescriptionInputs output;
					ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





					output.uv0 = input.texCoord0;
					output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
				#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
				#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
				#else
				#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
				#endif
				#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

					return output;
				}


				// --------------------------------------------------
				// Main

				#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
				#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"

				ENDHLSL
			}

			Pass
			{
				Name "DepthOnly"
				Tags
				{
					"LightMode" = "DepthOnly"
				}

					// Render State
					Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
					Cull Back
					ZTest LEqual
					ZWrite On
					ColorMask 0


					HLSLPROGRAM
					#pragma vertex vert
					#pragma fragment frag

					// Debug
					// <None>

					// --------------------------------------------------
					// Pass

					// Pragmas
					#pragma prefer_hlslcc gles
					#pragma exclude_renderers d3d11_9x
					#pragma target 2.0
					#pragma multi_compile_instancing

					// Keywords
					// PassKeywords: <None>
					// GraphKeywords: <None>

					// Defines
					#define _SURFACE_TYPE_TRANSPARENT 1
					#define ATTRIBUTES_NEED_NORMAL
					#define ATTRIBUTES_NEED_TANGENT
					#define ATTRIBUTES_NEED_TEXCOORD0
					#define VARYINGS_NEED_TEXCOORD0
					#define SHADERPASS_DEPTHONLY

					// Includes
					#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
					#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
					#include "Packages/com.unity.shadergraph/ShaderGraphLibrary/ShaderVariablesFunctions.hlsl"

					// --------------------------------------------------
					// Graph

					// Graph Properties
					CBUFFER_START(UnityPerMaterial)
					float _LevelUI;
					float4 _ColorUI;
					float _DistortionUI;
					CBUFFER_END
					TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex); float4 _MainTex_TexelSize;
					SAMPLER(_SampleTexture2D_24179E5C_Sampler_3_Linear_Repeat);

					// Graph Functions

					void Unity_Preview_float(float In, out float Out)
					{
						Out = In;
					}

					void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
					{
						Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
					}

					void Unity_Subtract_float(float A, float B, out float Out)
					{
						Out = A - B;
					}

					void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
					{
						Out = A * B;
					}

					void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
					{
						Out = UV * Tiling + Offset;
					}


					float2 Unity_GradientNoise_Dir_float(float2 p)
					{
						// Permutation and hashing used in webgl-nosie goo.gl/pX7HtC
						p = p % 289;
						float x = (34 * p.x + 1) * p.x % 289 + p.y;
						x = (34 * x + 1) * x % 289;
						x = frac(x / 41) * 2 - 1;
						return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
					}

					void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
					{
						float2 p = UV * Scale;
						float2 ip = floor(p);
						float2 fp = frac(p);
						float d00 = dot(Unity_GradientNoise_Dir_float(ip), fp);
						float d01 = dot(Unity_GradientNoise_Dir_float(ip + float2(0, 1)), fp - float2(0, 1));
						float d10 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 0)), fp - float2(1, 0));
						float d11 = dot(Unity_GradientNoise_Dir_float(ip + float2(1, 1)), fp - float2(1, 1));
						fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
						Out = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
					}


					inline float2 Unity_Voronoi_RandomVector_float(float2 UV, float offset)
					{
						float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
						UV = frac(sin(mul(UV, m)) * 46839.32);
						return float2(sin(UV.y * +offset) * 0.5 + 0.5, cos(UV.x * offset) * 0.5 + 0.5);
					}

					void Unity_Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells)
					{
						float2 g = floor(UV * CellDensity);
						float2 f = frac(UV * CellDensity);
						float t = 8.0;
						float3 res = float3(8.0, 0.0, 0.0);

						for (int y = -1; y <= 1; y++)
						{
							for (int x = -1; x <= 1; x++)
							{
								float2 lattice = float2(x,y);
								float2 offset = Unity_Voronoi_RandomVector_float(lattice + g, AngleOffset);
								float d = distance(lattice + offset, f);

								if (d < res.x)
								{
									res = float3(d, offset.x, offset.y);
									Out = res.x;
									Cells = res.y;
								}
							}
						}
					}

					void Unity_Power_float(float A, float B, out float Out)
					{
						Out = pow(A, B);
					}

					void Unity_Multiply_float(float A, float B, out float Out)
					{
						Out = A * B;
					}

					void Unity_Clamp_float(float In, float Min, float Max, out float Out)
					{
						Out = clamp(In, Min, Max);
					}

					void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
					{
						Out = smoothstep(Edge1, Edge2, In);
					}

					void Unity_Flip_float(float In, float Flip, out float Out)
					{
						Out = (Flip * -2 + 1) * In;
					}

					void Unity_Lerp_float(float A, float B, float T, out float Out)
					{
						Out = lerp(A, B, T);
					}

					// Graph Vertex
					// GraphVertex: <None>

					// Graph Pixel
					struct SurfaceDescriptionInputs
					{
						float4 uv0;
						float3 TimeParameters;
					};

					struct SurfaceDescription
					{
						float Alpha;
						float AlphaClipThreshold;
					};

					SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
					{
						SurfaceDescription surface = (SurfaceDescription)0;
						float4 _UV_A3FF9E13_Out_0 = IN.uv0;
						float _Split_2D81FF4C_R_1 = _UV_A3FF9E13_Out_0[0];
						float _Split_2D81FF4C_G_2 = _UV_A3FF9E13_Out_0[1];
						float _Split_2D81FF4C_B_3 = _UV_A3FF9E13_Out_0[2];
						float _Split_2D81FF4C_A_4 = _UV_A3FF9E13_Out_0[3];
						float _Preview_4BD64336_Out_1;
						Unity_Preview_float(_Split_2D81FF4C_G_2, _Preview_4BD64336_Out_1);
						float _Property_E4879BA6_Out_0 = _LevelUI;
						float _Remap_D94D54AB_Out_3;
						Unity_Remap_float(_Property_E4879BA6_Out_0, float2 (0, 100), float2 (0, 1), _Remap_D94D54AB_Out_3);
						float _Subtract_D861869C_Out_2;
						Unity_Subtract_float(_Preview_4BD64336_Out_1, _Remap_D94D54AB_Out_3, _Subtract_D861869C_Out_2);
						float2 _Vector2_3F56602_Out_0 = float2(0.1, -0.35);
						float2 _Multiply_2A9A7FFA_Out_2;
						Unity_Multiply_float(_Vector2_3F56602_Out_0, (IN.TimeParameters.x.xx), _Multiply_2A9A7FFA_Out_2);
						float2 _TilingAndOffset_C7EDED72_Out_3;
						Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_2A9A7FFA_Out_2, _TilingAndOffset_C7EDED72_Out_3);
						float _Vector1_7852A19C_Out_0 = 5;
						float _GradientNoise_4DCB19BE_Out_2;
						Unity_GradientNoise_float(_TilingAndOffset_C7EDED72_Out_3, _Vector1_7852A19C_Out_0, _GradientNoise_4DCB19BE_Out_2);
						float2 _Vector2_50642A88_Out_0 = float2(-0.1, -0.3);
						float2 _Multiply_E7975680_Out_2;
						Unity_Multiply_float(_Vector2_50642A88_Out_0, (IN.TimeParameters.x.xx), _Multiply_E7975680_Out_2);
						float2 _TilingAndOffset_90ED16CE_Out_3;
						Unity_TilingAndOffset_float(IN.uv0.xy, float2 (1, 1), _Multiply_E7975680_Out_2, _TilingAndOffset_90ED16CE_Out_3);
						float _Vector1_EB5CC39C_Out_0 = 6;
						float _Voronoi_49F98ABC_Out_3;
						float _Voronoi_49F98ABC_Cells_4;
						Unity_Voronoi_float(_TilingAndOffset_90ED16CE_Out_3, 2.5, _Vector1_EB5CC39C_Out_0, _Voronoi_49F98ABC_Out_3, _Voronoi_49F98ABC_Cells_4);
						float _Vector1_3C7C2399_Out_0 = 0.75;
						float _Power_F12441A2_Out_2;
						Unity_Power_float(_Voronoi_49F98ABC_Out_3, _Vector1_3C7C2399_Out_0, _Power_F12441A2_Out_2);
						float _Multiply_B1CE1EB4_Out_2;
						Unity_Multiply_float(_GradientNoise_4DCB19BE_Out_2, _Power_F12441A2_Out_2, _Multiply_B1CE1EB4_Out_2);
						float _Clamp_38746272_Out_3;
						Unity_Clamp_float(_Multiply_B1CE1EB4_Out_2, 0, 1, _Clamp_38746272_Out_3);
						float _Preview_E7B424C_Out_1;
						Unity_Preview_float(_Clamp_38746272_Out_3, _Preview_E7B424C_Out_1);
						float _Smoothstep_1B1A15AF_Out_3;
						Unity_Smoothstep_float(_Subtract_D861869C_Out_2, 1.7, _Preview_E7B424C_Out_1, _Smoothstep_1B1A15AF_Out_3);
						float _Flip_B243F868_Out_1;
						float _Flip_B243F868_Flip = float(1
					);    Unity_Flip_float(_Subtract_D861869C_Out_2, _Flip_B243F868_Flip, _Flip_B243F868_Out_1);
						float _Multiply_85399D70_Out_2;
						Unity_Multiply_float(_Flip_B243F868_Out_1, _Preview_E7B424C_Out_1, _Multiply_85399D70_Out_2);
						float _Lerp_8528632F_Out_3;
						Unity_Lerp_float(_Multiply_85399D70_Out_2, _Flip_B243F868_Out_1, 0.6, _Lerp_8528632F_Out_3);
						float _Property_AFCB5334_Out_0 = _DistortionUI;
						float _Lerp_5B4F005C_Out_3;
						Unity_Lerp_float(_Smoothstep_1B1A15AF_Out_3, _Lerp_8528632F_Out_3, _Property_AFCB5334_Out_0, _Lerp_5B4F005C_Out_3);
						float4 _SampleTexture2D_24179E5C_RGBA_0 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv0.xy);
						float _SampleTexture2D_24179E5C_R_4 = _SampleTexture2D_24179E5C_RGBA_0.r;
						float _SampleTexture2D_24179E5C_G_5 = _SampleTexture2D_24179E5C_RGBA_0.g;
						float _SampleTexture2D_24179E5C_B_6 = _SampleTexture2D_24179E5C_RGBA_0.b;
						float _SampleTexture2D_24179E5C_A_7 = _SampleTexture2D_24179E5C_RGBA_0.a;
						float _Multiply_F1200F5B_Out_2;
						Unity_Multiply_float(_Lerp_5B4F005C_Out_3, _SampleTexture2D_24179E5C_R_4, _Multiply_F1200F5B_Out_2);
						float _Multiply_C2827FA1_Out_2;
						Unity_Multiply_float(_Multiply_F1200F5B_Out_2, 4, _Multiply_C2827FA1_Out_2);
						float _Clamp_9C05ACD4_Out_3;
						Unity_Clamp_float(_Multiply_C2827FA1_Out_2, 0, 1, _Clamp_9C05ACD4_Out_3);
						surface.Alpha = _Clamp_9C05ACD4_Out_3;
						surface.AlphaClipThreshold = 0;
						return surface;
					}

					// --------------------------------------------------
					// Structs and Packing

					// Generated Type: Attributes
					struct Attributes
					{
						float3 positionOS : POSITION;
						float3 normalOS : NORMAL;
						float4 tangentOS : TANGENT;
						float4 uv0 : TEXCOORD0;
						#if UNITY_ANY_INSTANCING_ENABLED
						uint instanceID : INSTANCEID_SEMANTIC;
						#endif
					};

					// Generated Type: Varyings
					struct Varyings
					{
						float4 positionCS : SV_POSITION;
						float4 texCoord0;
						#if UNITY_ANY_INSTANCING_ENABLED
						uint instanceID : CUSTOM_INSTANCE_ID;
						#endif
						#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
						uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
						#endif
						#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
						uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
						#endif
						#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
						FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
						#endif
					};

					// Generated Type: PackedVaryings
					struct PackedVaryings
					{
						float4 positionCS : SV_POSITION;
						#if UNITY_ANY_INSTANCING_ENABLED
						uint instanceID : CUSTOM_INSTANCE_ID;
						#endif
						float4 interp00 : TEXCOORD0;
						#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
						uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
						#endif
						#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
						uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
						#endif
						#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
						FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
						#endif
					};

					// Packed Type: Varyings
					PackedVaryings PackVaryings(Varyings input)
					{
						PackedVaryings output = (PackedVaryings)0;
						output.positionCS = input.positionCS;
						output.interp00.xyzw = input.texCoord0;
						#if UNITY_ANY_INSTANCING_ENABLED
						output.instanceID = input.instanceID;
						#endif
						#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
						output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
						#endif
						#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
						output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
						#endif
						#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
						output.cullFace = input.cullFace;
						#endif
						return output;
					}

					// Unpacked Type: Varyings
					Varyings UnpackVaryings(PackedVaryings input)
					{
						Varyings output = (Varyings)0;
						output.positionCS = input.positionCS;
						output.texCoord0 = input.interp00.xyzw;
						#if UNITY_ANY_INSTANCING_ENABLED
						output.instanceID = input.instanceID;
						#endif
						#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
						output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
						#endif
						#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
						output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
						#endif
						#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
						output.cullFace = input.cullFace;
						#endif
						return output;
					}

					// --------------------------------------------------
					// Build Graph Inputs

					SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
					{
						SurfaceDescriptionInputs output;
						ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





						output.uv0 = input.texCoord0;
						output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
					#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
					#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
					#else
					#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
					#endif
					#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

						return output;
					}


					// --------------------------------------------------
					// Main

					#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
					#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

					ENDHLSL
				}

		}
			FallBack "Hidden/Shader Graph/FallbackError"
}