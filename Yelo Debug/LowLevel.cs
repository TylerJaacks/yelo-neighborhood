using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yelo.Debug.LowLevel
{
    public enum HResult : uint
    {
        Success = 0x0,

        Unexpected = 0x8000FFFFU,
        NotImplemented = 0x80004001U,
        OutOfMemory = 0x8007000EU,
        InvalidArg = 0x80070057U,
        NoInterface = 0x80004002U,
        InvalidPointer = 0x80004003U,
        InvalidHandle = 0x80070006U,
        Abort = 0x80004004U,
        Fail = 0x80004005U,
        AccessDenied = 0x80070005U,

        DataPending = 0x8000000AU,
    }

    public class ByteSwap
    {
        public static UInt32 SingleToUInt32(float value)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
        }
    }

    namespace Intel
    {
        public class x86
        {
            public const byte pushad = 0x60;
            public const byte popad = 0x61;

            public const byte push_prefix_dword = 0x68;

            public const byte mov_eax_prefix_dword = 0xB8;
            public const byte mov_ecx_prefix_dword = 0xB9;
            public const byte mov_edx_prefix_dword = 0xBA;
            public const byte mov_ebx_prefix_dword = 0xBB;
            public const byte mov_esp_prefix_dword = 0xBC;
            public const byte mov_ebp_prefix_dword = 0xBD;
            public const byte mov_esi_prefix_dword = 0xBE;
            public const byte mov_edi_prefix_dword = 0xBF;

            public const ushort call_prefix_absolute = 0x00FF | 0x1500;
            public const ushort call_eax = 0x00FF | 0xD000;
        }

        public class SSE
        {
            public const uint movss_xmm0_prefix_addr = 0x05100FF3;
            public const uint movss_xmm1_prefix_addr = 0x0D100FF3;
            public const uint movss_xmm2_prefix_addr = 0x15100FF3;
            public const uint movss_xmm3_prefix_addr = 0x1D100FF3;
            public const uint movss_xmm4_prefix_addr = 0x25100FF3;
            public const uint movss_xmm5_prefix_addr = 0x2D100FF3;
            public const uint movss_xmm6_prefix_addr = 0x35100FF3;
            public const uint movss_xmm7_prefix_addr = 0x3D100FF3;
        }
    }
}
