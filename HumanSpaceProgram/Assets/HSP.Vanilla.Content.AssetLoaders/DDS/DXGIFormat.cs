﻿namespace HSP.Vanilla.Content.AssetLoaders.DDS
{
    public enum DXGIFormat : uint
    {
        // https://learn.microsoft.com/en-us/windows/win32/api/dxgiformat/ne-dxgiformat-dxgi_format
        UNKNOWN = 0u,
        R32G32B32A32_TYPELESS = 1u,
        R32G32B32A32_FLOAT = 2u,
        R32G32B32A32_UINT = 3u,
        R32G32B32A32_SINT = 4u,
        R32G32B32_TYPELESS = 5u,
        R32G32B32_FLOAT = 6u,
        R32G32B32_UINT = 7u,
        R32G32B32_SINT = 8u,
        R16G16B16A16_TYPELESS = 9u,
        R16G16B16A16_FLOAT = 10u,
        R16G16B16A16_UNORM = 11u,
        R16G16B16A16_UINT = 12u,
        R16G16B16A16_SNORM = 13u,
        R16G16B16A16_SINT = 14u,
        R32G32_TYPELESS = 15u,
        R32G32_FLOAT = 16u,
        R32G32_UINT = 17u,
        R32G32_SINT = 18u,
        R32G8X24_TYPELESS = 19u,
        D32_FLOAT_S8X24_UINT = 20u,
        R32_FLOAT_X8X24_TYPELESS = 21u,
        X32_TYPELESS_G8X24_UINT = 22u,
        R10G10B10A2_TYPELESS = 23u,
        R10G10B10A2_UNORM = 24u,
        R10G10B10A2_UINT = 25u,
        R11G11B10_FLOAT = 26u,
        R8G8B8A8_TYPELESS = 27u,
        R8G8B8A8_UNORM = 28u,
        R8G8B8A8_UNORM_SRGB = 29u,
        R8G8B8A8_UINT = 30u,
        R8G8B8A8_SNORM = 31u,
        R8G8B8A8_SINT = 32u,
        R16G16_TYPELESS = 33u,
        R16G16_FLOAT = 34u,
        R16G16_UNORM = 35u,
        R16G16_UINT = 36u,
        R16G16_SNORM = 37u,
        R16G16_SINT = 38u,
        R32_TYPELESS = 39u,
        D32_FLOAT = 40u,
        R32_FLOAT = 41u,
        R32_UINT = 42u,
        R32_SINT = 43u,
        R24G8_TYPELESS = 44u,
        D24_UNORM_S8_UINT = 45u,
        R24_UNORM_X8_TYPELESS = 46u,
        X24_TYPELESS_G8_UINT = 47u,
        R8G8_TYPELESS = 48u,
        R8G8_UNORM = 49u,
        R8G8_UINT = 50u,
        R8G8_SNORM = 51u,
        R8G8_SINT = 52u,
        R16_TYPELESS = 53u,
        R16_FLOAT = 54u,
        D16_UNORM = 55u,
        R16_UNORM = 56u,
        R16_UINT = 57u,
        R16_SNORM = 58u,
        R16_SINT = 59u,
        R8_TYPELESS = 60u,
        R8_UNORM = 61u,
        R8_UINT = 62u,
        R8_SNORM = 63u,
        R8_SINT = 64u,
        A8_UNORM = 65u,
        R1_UNORM = 66u,
        R9G9B9E5_SHAREDEXP = 67u,
        R8G8_B8G8_UNORM = 68u,
        G8R8_G8B8_UNORM = 69u,
        BC1_TYPELESS = 70u,
        BC1_UNORM = 71u,
        BC1_UNORM_SRGB = 72u,
        BC2_TYPELESS = 73u,
        BC2_UNORM = 74u,
        BC2_UNORM_SRGB = 75u,
        BC3_TYPELESS = 76u,
        BC3_UNORM = 77u,
        BC3_UNORM_SRGB = 78u,
        BC4_TYPELESS = 79u,
        BC4_UNORM = 80u,
        BC4_SNORM = 81u,
        BC5_TYPELESS = 82u,
        BC5_UNORM = 83u,
        BC5_SNORM = 84u,
        B5G6R5_UNORM = 85u,
        B5G5R5A1_UNORM = 86u,
        B8G8R8A8_UNORM = 87u,
        B8G8R8X8_UNORM = 88u,
        R10G10B10_XR_BIAS_A2_UNORM = 89u,
        B8G8R8A8_TYPELESS = 90u,
        B8G8R8A8_UNORM_SRGB = 91u,
        B8G8R8X8_TYPELESS = 92u,
        B8G8R8X8_UNORM_SRGB = 93u,
        BC6H_TYPELESS = 94u,
        BC6H_UF16 = 95u,
        BC6H_SF16 = 96u,
        BC7_TYPELESS = 97u,
        BC7_UNORM = 98u,
        BC7_UNORM_SRGB = 99u,
        AYUV = 100u,
        Y410 = 101u,
        Y416 = 102u,
        NV12 = 103u,
        P010 = 104u,
        P016 = 105u,
        _420_OPAQUE = 106u,
        YUY2 = 107u,
        Y210 = 108u,
        Y216 = 109u,
        NV11 = 110u,
        AI44 = 111u,
        IA44 = 112u,
        P8 = 113u,
        A8P8 = 114u,
        B4G4R4A4_UNORM = 115u,
        P208 = 130u,
        V208 = 131u,
        V408 = 132u,
        SAMPLER_FEEDBACK_MIN_MIP_OPAQUEu,
        SAMPLER_FEEDBACK_MIP_REGION_USED_OPAQUEu,
        FORCE_UINT = 0xffffffffu
    }
}