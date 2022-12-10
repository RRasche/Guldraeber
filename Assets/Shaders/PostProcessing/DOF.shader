Shader "PostProcessing/DOF"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetDistance ("Target Distance", Range(0.001, 100)) = 10
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _TargetDistance;

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                float depth = tex2D(_CameraDepthTexture, i.uv).x;
                
                //float diff = pow(abs(depth - _TargetDistance), 0.5);
                //return fixed4(diff, 0, 0, 1);
                //float deviation = log(
                //    abs(sqrt(depth*1000) - _TargetDistance) / 10000);
                float td = _TargetDistance;
                float linDepth = sqrt(depth);
                float deviation = td - (linDepth * linDepth) / td;
                deviation *= 1;
                //deviation = saturate(deviation);

                float3 col = float3(0, 0, 0);
                col.x += tex2D(_MainTex, i.uv).x * 0.66;
                col.xy += tex2D(_MainTex, i.uv*(0.995+deviation)) * 0.33;
                col.y += tex2D(_MainTex, i.uv*(0.99+deviation)).y*.33;
                col.yz += tex2D(_MainTex, i.uv*(0.985+deviation)).yz*.33;
                col.z += tex2D(_MainTex, i.uv*(0.98+deviation)).z*.66;

                /*float3 col = tex2D(_MainTex, i.uv).xyz * 0.66;
                col += tex2D(_MainTex, i.uv + float2(deviation, 0)).xyz * 0.33;
                col += tex2D(_MainTex, i.uv + float2(0, deviation)).xyz * 0.33;*/

                
                //col = float3(1.7,1.8,1.9)*col/(1.0+col);
                
	            //return fixed4(pow(col,(1.0)/(_TargetDistance)), 1.0);
                return fixed4(saturate(abs(deviation)), 0, 0, 1);
            }
            ENDCG
        }
    }
}
