Shader "Custom/UnderWater"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlayerVertexX ("Player Location X", Int) = 0
        _PlayerVertexY ("Player Location Y", Int) = 0
        _PointerX ("Pointer Location X", Int) = 0
        _PointerY ("Pointer Location Y", Int) = 0
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

            float4 _MainTex_TexelSize;
            sampler2D _MainTex;
            int _PlayerVertexX;
            int _PlayerVertexY;
            int _PointerX;
            int _PointerY;

            float2 WaveyDisp(float2 vertex) 
            {
                float2 disp = float2(0,0);
                // waveyness
                float dispY = sin(vertex.x / 20 + _Time[1]) / 200;
                disp.y += dispY;
                return disp;
            }

            float getPlayerLight(float2 vertex)
            {
                if (_PlayerVertexX != -1)
                {
                    float2 playerPos = float2(_PlayerVertexX, _MainTex_TexelSize.w - _PlayerVertexY);
                    float2 toPixel = vertex - playerPos;
                    float dist = length(toPixel);
                    float radius = 100;
                    float light = max(radius - dist, 0) / radius;
                    return light;
                }
                else
                {
                    return 0;
                }
            }

            float getPointerLight(float2 vertex) 
            {
                if (_PlayerVertexX != -1)
                {
                    float2 playerPos = float2(_PlayerVertexX, _MainTex_TexelSize.w - _PlayerVertexY);
                    float2 pointerPos = float2(_PointerX, _MainTex_TexelSize.w - _PointerY);
                    

                    float2 toPixel = vertex - playerPos;
                    float2 toPointer = pointerPos - playerPos;
                    float2 toPixelNorm = normalize(toPixel);
                    float2 toPointerNorm = normalize(toPointer);

                    float light = max(dot(toPixelNorm, toPointerNorm)-0.8, 0)*5;

                    float distance = 600;
                    light *= max(0, (distance - length(toPixel))/ distance);
                    return light;

                }
                else
                {
                    return 0;
                }

            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 dispUV = i.uv;

                // waveyness
                dispUV += WaveyDisp(i.vertex);


                fixed4 col = tex2D(_MainTex, dispUV);

                // darkness
                float baseLight = 0.15f;
                float playerLight = getPlayerLight(i.vertex);
                float pointerLight = getPointerLight(i.vertex);

                float light = max(max(baseLight, playerLight), pointerLight);

                col *= light;

                return col;
            }
            ENDCG
        }
    }
}
