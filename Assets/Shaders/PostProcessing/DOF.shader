Shader "PostProcessing/DOF"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TargetDistance ("Target Distance", Range(0.001, 100)) = 10
    }
    SubShader
    {
        Cull Back ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
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

            float deviation_fn(float x, float a) {
                return x - a * a / x;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float depth = tex2D(_CameraDepthTexture, i.uv);
                float linDepth = LinearEyeDepth(depth);

                float diff = deviation_fn(linDepth, _TargetDistance);// * 0.00005;

                fixed3 p4 = tex2D(_MainTex, i.uv).rgb;
                float2 uvOffset = float2(0, sqrt(abs(diff)) * 0.002);
                if (depth - tex2D(_CameraDepthTexture, i.uv - uvOffset).x < -0.01 || depth - tex2D(_CameraDepthTexture, i.uv + uvOffset).x < -0.01) uvOffset *= 0.1;

                fixed3 p1 = tex2D(_MainTex, i.uv - uvOffset).rgb;
                fixed3 p2 = tex2D(_MainTex, i.uv - uvOffset * 0.667).rgb;
                fixed3 p3 = tex2D(_MainTex, i.uv - uvOffset * 0.333).rgb;
                fixed3 p5 = tex2D(_MainTex, i.uv + uvOffset * 0.333).rgb;
                fixed3 p6 = tex2D(_MainTex, i.uv + uvOffset * 0.667).rgb;
                fixed3 p7 = tex2D(_MainTex, i.uv + uvOffset).rgb;

                p1.b *= 0.;
                p2.b *= 0.33;
                p3.b *= 0.66;
                p5.b *= 1.33;
                p6.b *= 1.66;
                p7.b *= 2.0;

                p7.r *= 0.;
                p6.r *= 0.33;
                p5.r *= 0.66;
                p3.r *= 1.33;
                p2.r *= 1.66;
                p1.r *= 2.0;

                return fixed4(p1 * 0.031251 + p2 * 0.106235 + p3 * 0.221252 + p4 * 0.282524 + p5 * 0.221252 + p6 * 0.106235 + p7 * 0.031251, 1);
            }
            ENDHLSL
        }

        Pass
        {
            HLSLPROGRAM
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

            float deviation_fn(float x, float a) {
                return x - a * a / x;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float depth = tex2D(_CameraDepthTexture, i.uv);
                float linDepth = LinearEyeDepth(depth);

                float diff = deviation_fn(linDepth, _TargetDistance);// * 0.00005;

                fixed3 p4 = tex2D(_MainTex, i.uv).rgb;
                float2 uvOffset = float2(sqrt(abs(diff)) * 0.002, 0);
                if (depth - tex2D(_CameraDepthTexture, i.uv - uvOffset).x < -0.01 || depth - tex2D(_CameraDepthTexture, i.uv + uvOffset).x < -0.01) uvOffset *= 0.1;

                fixed3 p1 = tex2D(_MainTex, i.uv - uvOffset).rgb;
                fixed3 p2 = tex2D(_MainTex, i.uv - uvOffset * 0.667).rgb;
                fixed3 p3 = tex2D(_MainTex, i.uv - uvOffset * 0.333).rgb;
                fixed3 p5 = tex2D(_MainTex, i.uv + uvOffset * 0.333).rgb;
                fixed3 p6 = tex2D(_MainTex, i.uv + uvOffset * 0.667).rgb;
                fixed3 p7 = tex2D(_MainTex, i.uv + uvOffset).rgb;

                
                p1.g *= 0.;
                p2.g *= 0.33;
                p3.g *= 0.66;
                p5.g *= 1.33;
                p6.g *= 1.66;
                p7.g *= 2.0;

                p7.r *= 0.;
                p6.r *= 0.33;
                p5.r *= 0.66;
                p3.r *= 1.33;
                p2.r *= 1.66;
                p1.r *= 2.0;

                return fixed4(p1 * 0.031251 + p2 * 0.106235 + p3 * 0.221252 + p4 * 0.282524 + p5 * 0.221252 + p6 * 0.106235 + p7 * 0.031251, 1);
            }
            ENDHLSL
        }
    }
}
