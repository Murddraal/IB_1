using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IB_1
{
    class MD5
    {
        private static uint A = 0x67452301, B = 0xEFCDAB89, C = 0x98BADCFE, D = 0x10325476;

        private static uint F(uint x, uint y, uint z)
        {
            return (x & y) | ((~x) & z);
        }
        private static uint G(uint x, uint y, uint z)
        {
            return (x & z) | ((~z) & y);
        }        
        private static uint H(uint x, uint y, uint z)
        {
            return x ^ y ^ z;
        }
        private static uint I(uint x, uint y, uint z)
        {
            return y ^ ((~z) | x);
        }

        private static uint T(uint i)
        {
            return (uint)(4294967296 * Math.Abs(Math.Sin(i % 64)));
        }

        List<List<UInt32>> M = new List<List<UInt32>>();
        List<uint> X = new List<uint>();

        static List<string> mess_str = new List<string>();
        static List<byte[]> mess_byte = new List<byte[]>();

        public List<UInt32[]> log_hash = new List<UInt32[]>();

        public static BitArray Append(BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }
        public static void Reverse(ref BitArray message)
        {
            for (int i = 0; i < message.Count / 2; i++)
            {

                var temp = message[i];
                message[i] = message[message.Count - 1 - i];
                message[message.Count - 1 - i] = temp;
            }
        }
        public static void Reverse_Byte(ref BitArray message)
        {
            for (int i = 0; i < message.Count; i += 8)
            {
                for (int j = i, k = 0; j < i + 4; j++, k++)
                {
                    var temp = message[j];
                    message[j] = message[i + 7 - k];
                    message[i + 7 - k] = temp;
                }
            }
        }
        public static void Reverse_Bytes_In_Words(ref BitArray message)
        {
            int step = 0;
            for (int i = 0; i < message.Length; i += step)
            {
                if (i + 32 <= message.Length)
                {
                    step = 32;
                    for (int j = i, k = 0; j < i + step / 2; ++j, ++k)
                    {
                        var el = i + 32 - 8 * (k / 8 + 1) + k % 8;
                        var temp = message[j];
                        message[j] = message[el];
                        message[el] = temp;
                    }
                }
                else
                {
                    step = message.Length - i == 8 ? 0 : 8;
                    for (int j = i, k = 0; j < i + step; ++j, ++k)
                    {
                        var el = message.Length - 8 * (k / 8 + 1) + k % 8;
                        var temp = message[j];
                        message[j] = message[el];
                        message[el] = temp;
                    }
                    return;
                }
            }
        }
        public static void Reverse_Words(ref BitArray message)
        {
            if (message.Length % 32 == 0)
            {
                for (int i = 0; i < (message.Length / 64) * 32; i += 32)
                {
                    for (int j = i, k = 0; k < 32; ++j, ++k)
                    {
                        var el = message.Length - i - 32 + k - 1; ;
                        var temp = message[j];
                        message[j] = message[el];
                        message[el] = temp;
                    }
                }
            }
        }

        void Create_bloks(BitArray message)
        {
            List<UInt32> block = new List<UInt32>();
            List<bool> word = new List<bool>();
            for (int i = 0; i < message.Count; i++)
            {
                word.Add(message[i]);
                if ((i + 1) % 32 == 0 && i != 0)
                {
                    //block.Add(BitArrayToLong(new BitArray(word.ToArray())));
                    block.Add(BitArrayToUInt32(new BitArray(word.ToArray())));
                    word = new List<bool>();
                }
                if ((i + 1) % 512 == 0 && i != 0)
                {
                    M.Add(new List<UInt32>(block));
                    block.Clear();
                }
            }
        }

        public BitArray prepear(BitArray message)
        {
            //step 1
            int orig_lngth = message.Count;

            var temp1 = new List<bool>();
            temp1.Add(true);
            int new_lngth = orig_lngth + 1;

            while ((new_lngth - 448) % 512 != 0)
            {
                temp1.Add(false);
                ++new_lngth;
            }
            message = Append(message, new BitArray(temp1.ToArray()));
            //Reverse_Bytes_In_Words(ref message);
            //Reverse_Words(ref message);
            //Reverse_Byte(ref message);
            //Reverse(ref message);

            //step 2
            var temp2 = new BitArray(new int[] { orig_lngth });
            Reverse(ref temp2);
            Reverse_Bytes_In_Words(ref temp2);
            Reverse_Words(ref temp2);

            temp2 = Append(temp2, new BitArray(new int[1]));
            //Reverse_Bytes_In_Words(ref temp2);

            message = Append(message, temp2);
            //Reverse_Bytes_In_Words(ref message);
            //Reverse_Words(ref message);

            Create_bloks(message);
            return message;
        }

        uint rot_l(uint num, int pos)
        {
            return (num << pos) | (num >>(32 - pos));
        }

        private void transform1(ref uint a, uint b, uint c, uint d, int k, int s, int i)
        {
            a = b + rot_l(a + F(b, c, d) + X[k] + T((uint)i), s);
        }
        private void transform2(ref uint a, uint b, uint c, uint d, int k, int s, int i)
        {
            a = b + rot_l(a + G(b, c, d) + X[k] + T((uint)i), s);
        }
        private void transform3(ref uint a, uint b, uint c, uint d, int k, int s, int i)
        {
            a = b + rot_l(a + H(b, c, d) + X[k] + T((uint)i), s);
        }
        private void transform4(ref uint a, uint b, uint c, uint d, int k, int s, int i)
        {
            a = b + rot_l(a + I(b, c, d) + X[k] + T((uint)i), s);
        }

        public uint[] Hashing()
        {
            var N = M.Count;
            var h = new uint[4];
            for (int i = 0; i < M.Count; ++i)
            {
                for (int j = 0; j < M[i].Count; ++j)
                {
                    X.Add(M[i][j]);
                }
            }

            uint AA = A, BB = B, CC = C, DD = D;
            // round 1
            for (int k = 0, i = 1; i <= 16; ++k, ++i)
            {
                if (i % 4 == 1)
                    transform1(ref AA, BB, CC, DD, k, 7, i);
                if (i % 4 == 2)
                    transform1(ref DD, AA, BB, CC, k, 12, i);
                if (i % 4 == 3)
                    transform1(ref CC, DD, AA, BB, k, 17, i);
                if (i % 4 == 0)
                    transform1(ref BB, CC, DD, AA, k, 22, i);
            }
            // round 2
            for (int k = 1, i = 17; i <= 32; k = (k + 5) % 16, ++i)
            {
                if (i % 4 == 1)
                    transform2(ref AA, BB, CC, DD, k, 5, i);
                if (i % 4 == 2)
                    transform2(ref DD, AA, BB, CC, k, 9, i);
                if (i % 4 == 3)
                    transform2(ref CC, DD, AA, BB, k, 14, i);
                if (i % 4 == 0)
                    transform2(ref BB, CC, DD, AA, k, 20, i);
            }
            // round 3
            for (int k = 5, i = 33; i <= 48; k = (k + 3) % 16, ++i)
            {
                if (i % 4 == 1)
                    transform3(ref AA, BB, CC, DD, k, 4, i);
                if (i % 4 == 2)
                    transform3(ref DD, AA, BB, CC, k, 11, i);
                if (i % 4 == 3)
                    transform3(ref CC, DD, AA, BB, k, 16, i);
                if (i % 4 == 0)
                    transform3(ref BB, CC, DD, AA, k, 23, i);
            }
            // round 4
            for (int k = 0, i = 49; i <= 64; k = (k + 7) % 16, ++i)
            {
                if (i % 4 == 1)
                    transform4(ref AA, BB, CC, DD, k, 6, i);
                if (i % 4 == 2)
                    transform4(ref DD, AA, BB, CC, k, 10, i);
                if (i % 4 == 3)
                    transform4(ref CC, DD, AA, BB, k, 15, i);
                if (i % 4 == 0)
                    transform4(ref BB, CC, DD, AA, k, 21, i);
            }

            h[0] = A + AA; h[1] = B + BB; h[2] = C + CC; h[3] = D + DD;

            return h;
        }

        public static UInt32 BitArrayToUInt32(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];

            Reverse(ref bits);            

            bits.CopyTo(ret, 0);
            Array.Reverse(ret);

            var a = BitConverter.ToUInt32(ret, 0);
            string Hex = String.Format("{0:X}", a);
            mess_str.Add(Hex);
            mess_byte.Add(ret);

            return a;
        }

        
    }


}
