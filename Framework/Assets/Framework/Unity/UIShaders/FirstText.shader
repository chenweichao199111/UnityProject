Shader "CustomUI/FirstText"
{
	Properties
	{
		_MainTex("Font Texture", 2D) = "white" {}
	//MASK SUPPORT ADD
	_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
		//MASK SUPPORT END
	}


		SubShader{
		Tags{ "Queue" = "Overlay"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True" }

		//MASK SUPPORT ADD
		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}
		ColorMask[_ColorMask]
		//MASK SUPPORT END


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest Always
		Fog{ Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass{
		//Color[_Color]
		SetTexture[_MainTex]{
		combine primary, texture * primary
	}
	}
	}
}