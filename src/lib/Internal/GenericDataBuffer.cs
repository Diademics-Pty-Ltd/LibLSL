using System;
using System.Runtime.InteropServices;

namespace LibLSL.Internal
{
    [StructLayout(LayoutKind.Explicit, Pack = 2)]
    internal class GenericSampleBuffer
    {
        [FieldOffset(0)]
        public int _numberOfBytes;
        [FieldOffset(8)]
        private readonly byte[] _byteBuffer;
        [FieldOffset(8)]
        private readonly float[] _floatBuffer;
        [FieldOffset(8)]
        private readonly double[] _doubleBuffer;
        [FieldOffset(8)]
        private readonly int[] _intBuffer;
        [FieldOffset(8)]
        private readonly short[] _shortBuffer;
        [FieldOffset(8)]
        private readonly char[] _charBuffer;
        [FieldOffset(8)]
        private readonly long[] _longBuffer;

        public GenericSampleBuffer(int sizeToAllocateInBytes)
        {
            int aligned4Bytes = sizeToAllocateInBytes % 4;
            sizeToAllocateInBytes = (aligned4Bytes == 0) ? sizeToAllocateInBytes : sizeToAllocateInBytes + 4 - aligned4Bytes;
            // Allocating the byteBuffer is co-allocating the floatBuffer and the intBuffer
            _byteBuffer = new byte[sizeToAllocateInBytes];
            _numberOfBytes = _byteBuffer.Length;
        }

        public static implicit operator byte[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._byteBuffer;
        }
        public static implicit operator float[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._floatBuffer;
        }
        public static implicit operator double[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._doubleBuffer;
        }
        public static implicit operator int[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._intBuffer;
        }
        public static implicit operator short[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._shortBuffer;
        }
        public static implicit operator char[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._charBuffer;
        }
        public static implicit operator long[](GenericSampleBuffer genericSampleBuffer)
        {
            return genericSampleBuffer._longBuffer;
        }

        public byte[] ByteBuffer => _byteBuffer;
        public float[] FloatBuffer => _floatBuffer;
        public double[] DoubleBuffer => _doubleBuffer;
        public int[] IntBuffer => _intBuffer;
        public short[] ShortBuffer => _shortBuffer;
        public char[] CharBuffer => _charBuffer;
        public long[] LongBuffer => _longBuffer;

        public int ByteBufferCount
        {
            get => _numberOfBytes;
            set => _numberOfBytes = CheckValidityCount("ByteBufferCount", value, 1);
        }
        public int FloatBufferCount
        {
            get => _numberOfBytes / 4;
            set => _numberOfBytes = CheckValidityCount("FloatBufferCount", value, 4);
        }
        public int DoubleBufferCount
        {
            get => _numberOfBytes / 8;
            set => _numberOfBytes = CheckValidityCount("DoubleBufferCount", value, 4);
        }
        public int IntBufferCount
        {
            get => _numberOfBytes / 4;
            set => _numberOfBytes = CheckValidityCount("IntBufferCount", value, 4);
        }
        public int ShortBufferCount
        {
            get => _numberOfBytes / 2;
            set => _numberOfBytes = CheckValidityCount("ShortBufferCount", value, 2);
        }
        public int CharBufferCount
        {
            get => _numberOfBytes;
            set => _numberOfBytes = CheckValidityCount("CharBufferCount", value, 1);
        }
        public int LongBufferCount
        {
            get => _numberOfBytes / 8;
            set => _numberOfBytes = CheckValidityCount("LongBufferCount", value, 8);
        }

        public void Clear()
        {
            Array.Clear(_byteBuffer, 0, _byteBuffer.Length);
        }

        public void Copy(Array destinationArray)
        {
            Array.Copy(_byteBuffer, destinationArray, _numberOfBytes);
        }

        private int CheckValidityCount(string argName, int value, int sizeOfValue)
        {
            int newNumberOfBytes = value * sizeOfValue;
            if ((newNumberOfBytes % 4) != 0)
            {
                throw new ArgumentOutOfRangeException(argName, $"{argName} cannot set a count ({newNumberOfBytes}) that is not 4 bytes aligned ");
            }

            if (value < 0 || value > (_byteBuffer.Length / sizeOfValue))
            {
                throw new ArgumentOutOfRangeException(argName, $"{argName} cannot set a count that exceed max count { _byteBuffer.Length / sizeOfValue}");
            }
            return newNumberOfBytes;
        }
    }
}
