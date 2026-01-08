Shader "UI/GradientProgressBar_3Color_Shine"
{
    Properties
    {
        _ColorA ("Start Color", Color) = (0.6,0.6,0.6,1)     // Pending
        _ColorB ("Middle Color", Color) = (1.0,0.7,0.2,1)   // In Progress
        _ColorC ("End Color", Color) = (0.2,0.8,0.4,1)      // Completed

        _Split1 ("Start → Middle Split", Range(0,1)) = 0.33
        _Split2 ("Middle → End Split", Range(0,1)) = 0.66

        _Progress ("Progress", Range(0,1)) = 0.5

        _ShinePos ("Shine Position", Range(0,1)) = 0
        _ShineWidth ("Shine Width", Range(0.01,0.5)) = 0.15
        _ShineIntensity ("Shine Intensity", Range(0,2)) = 0.8
        _ShineEnabled ("Shine Enabled", Float) = 1


        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }   

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        ColorMask [_ColorMask]
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _ColorA, _ColorB, _ColorC;
            float _Split1, _Split2;
            float _Progress;

            float _ShinePos;
            float _ShineWidth;
            float _ShineIntensity;
            float _ShineEnabled;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Mask by progress
                if (i.uv.x > _Progress)
                    return fixed4(0,0,0,0);

                fixed4 baseCol;
                if (i.uv.x < _Split1)
                {
                    baseCol = lerp(_ColorA, _ColorB, i.uv.x / _Split1);
                }
                else if (i.uv.x < _Split2)
                {
                    baseCol = lerp(_ColorB, _ColorC,
                        (i.uv.x - _Split1) / (_Split2 - _Split1));
                }
                else
                {
                    baseCol = _ColorC;
                }

                float shine = 0;
                if (_ShineEnabled > 0.5)
                {
                    float shineX = _ShinePos * _Progress;
                    float dist = abs(i.uv.x - shineX);
                    shine = smoothstep(_ShineWidth, 0, dist) * _ShineIntensity;
                }

                baseCol.rgb += shine;
                return baseCol;
            }
            ENDCG
        }
    }
}
