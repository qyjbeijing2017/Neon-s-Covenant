// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|emission-5640-OUT,custl-3155-OUT;n:type:ShaderForge.SFN_Tex2d,id:6533,x:32795,y:32958,ptovrint:False,ptlb:node_6533,ptin:_node_6533,varname:node_6533,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:15594204831a7964388759325a60e481,ntxv:0,isnm:False|UVIN-4809-UVOUT;n:type:ShaderForge.SFN_Color,id:1311,x:32743,y:32676,ptovrint:False,ptlb:node_1311,ptin:_node_1311,varname:node_1311,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:5640,x:33031,y:32712,varname:node_5640,prsc:2|A-1311-RGB,B-6533-RGB;n:type:ShaderForge.SFN_Panner,id:4809,x:32586,y:32926,varname:node_4809,prsc:2,spu:1,spv:1|UVIN-1749-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1749,x:32295,y:32901,varname:node_1749,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Fresnel,id:3502,x:32558,y:33291,varname:node_3502,prsc:2;n:type:ShaderForge.SFN_Color,id:672,x:32558,y:33152,ptovrint:False,ptlb:node_672,ptin:_node_672,varname:node_672,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:3155,x:32846,y:33239,varname:node_3155,prsc:2|A-672-RGB,B-3502-OUT;proporder:6533-1311-672;pass:END;sub:END;*/

Shader "Shader Forge/boss_attack_ball" {
    Properties {
        _node_6533 ("node_6533", 2D) = "white" {}
        [HDR]_node_1311 ("node_1311", Color) = (0.5,0.5,0.5,1)
        [HDR]_node_672 ("node_672", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal 
            #pragma target 3.0
            uniform sampler2D _node_6533; uniform float4 _node_6533_ST;
            uniform float4 _node_1311;
            uniform float4 _node_672;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_9237 = _Time;
                float2 node_4809 = (i.uv0+node_9237.g*float2(1,1));
                float4 _node_6533_var = tex2D(_node_6533,TRANSFORM_TEX(node_4809, _node_6533));
                float3 emissive = (_node_1311.rgb*_node_6533_var.rgb);
                float3 finalColor = emissive + (_node_672.rgb*(1.0-max(0,dot(normalDirection, viewDirection))));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
