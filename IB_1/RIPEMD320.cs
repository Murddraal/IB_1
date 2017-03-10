using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace IB_1
{
    class RIPEMD320
    {
        

        static private class constants
        {
            public static UInt32 f(UInt32 j, UInt32 x, UInt32 y, UInt32 z)
            {
                return
                (
                    j <= 15
                    ? (x ^ y ^ z)
                    : (
                        j <= 31
                        ? ((x & y) | (~x) & z)
                        : (
                            j <= 47
                            ? ((x | (~y)) ^ z)
                            : (
                                j <= 63
                                ? (x & z) | (y & (~z))
                                : x ^ (y | (~z))
                              )
                          )
                      )
                  );
            }

            static public int[] R1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                                                 7, 4, 13, 1, 10, 6, 15, 3, 12, 0, 9, 5, 2, 14, 11, 8,
                                                 3, 10, 14, 4, 9, 15, 8, 1, 2, 7, 0, 6, 13, 11, 5, 12,
                                                 1, 9, 11, 10, 0, 8, 12, 4, 13, 3, 7, 15, 14, 5, 6, 2,
                                                 4, 0, 5, 9, 7, 12, 2, 10, 14, 1, 3, 8, 11, 6, 15, 13 };
            static public int[] R2 = new int[] { 5, 14, 7, 0, 9, 2, 11, 4, 13, 6, 15, 8, 1, 10, 3, 12,
                                                 6, 11, 3, 7, 0, 13, 5, 10, 14, 15, 8, 12, 4, 9, 1, 2,
                                                 15, 5, 1, 3, 7, 14, 6, 9, 11, 8, 12, 2, 10, 0, 4, 13,
                                                 8, 6, 4, 1, 3, 11, 15, 0, 5, 12, 2, 13, 9, 7, 10, 14,
                                                 12, 15, 10, 4, 1, 5, 8, 7, 6, 2, 13, 14, 0, 3, 9, 11 };

            static public int[] S1 = new int[] {11, 14, 15, 12, 5, 8, 7, 9, 11, 13, 14, 15, 6, 7, 9, 8,
                                                7, 6, 8, 13, 11, 9, 7, 15, 7, 12, 15, 9, 11, 7, 13, 12,
                                                11, 13, 6, 7, 14, 9, 13, 15, 14, 8, 13, 6, 5, 12, 7, 5,
                                                11, 12, 14, 15, 14, 15, 9, 8, 9, 14, 5, 6, 8, 6, 5, 12,
                                                9, 15, 5, 11, 6, 8, 13, 12, 5, 12, 13, 14, 11, 8, 5, 6};

            static public int[] S2 = new int[] {8, 9, 9, 11, 13, 15, 15, 5, 7, 7, 8, 11, 14, 14, 12, 6,
                                                9, 13, 15, 7, 12, 8, 9, 11, 7, 7, 12, 7, 6, 15, 13, 11,
                                                9, 7, 15, 11, 8, 6, 6, 14, 12, 13, 5, 14, 13, 13, 7, 5,
                                                15, 5, 8, 11, 14, 14, 6, 14, 6, 9, 12, 9, 12, 5, 15, 8,
                                                8, 5, 12, 9, 12, 5, 14, 6, 8, 13, 6, 5, 15, 13, 11, 11};

            static public UInt32 K1(int j)
            {
                if (j <= 15) return 0x00000000;
                else if (j <= 31) return 0x5a827999;
                else if (j <= 47) return 0x6ed9eba1;
                else if (j <= 63) return 0x8f1bbcdc;
                else return 0xa953fd4e;
            }

            static public UInt32 K2(int j)
            {
                if (j <= 15) return 0x50a28be6;
                else if (j <= 31) return 0x5c4dd124;
                else if (j <= 47) return 0x6d703ef3;
                else if (j <= 63) return 0x7a6d76e9;
                else return 0x00000000;
            }
            static public UInt32[] h = new UInt32[] { 0x67452301, 0xefcdab89, 0x98badcfe, 0x10325476, 0xc3d2e1f0,
                                                    0x76543210, 0xfedcba98, 0x89abcdef, 0x01234567, 0x3c2d1e0f};
        }

        List<List<UInt32>> M = new List<List<UInt32>>();
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
            Reverse_Bytes_In_Words(ref message);

            //step 2
            var temp2 = new BitArray(new int[] { orig_lngth });
            Reverse(ref temp2);
            //Reverse_Bytes_In_Words(ref temp2);

            temp2 = Append(temp2, new BitArray(new int[1]));
            //Reverse_Bytes_In_Words(ref temp2);

            message = Append(message, temp2);
            //Reverse_Bytes_In_Words(ref message);
            //Reverse_Words(ref message);

            Create_bloks(message);
            return message;
        }

        UInt32 rot_l(UInt32 num, int pos)
        {
            return (num << pos) | (num >>(32 - pos));
        }

        public UInt32[] Hashing()
        {

            int block_count = M.Count;
            var h = new UInt32[10];

            constants.h.CopyTo(h, 0);

            for (int i = 0; i < block_count; ++i)
            {
                UInt32 A1 = h[0], B1 = h[1], C1 = h[2], D1 = h[3], E1 = h[4];
                UInt32 A2 = h[5], B2 = h[6], C2 = h[7], D2 = h[8], E2 = h[9];
                UInt32 T = 0;

                for (int j = 0; j < 80; ++j)
                {
                    T = rot_l(A1 + constants.f((uint)j, B1, C1, D1) + M[i][constants.R1[j]] + constants.K1(j), constants.S1[j]) + E1;
                    A1 = E1; E1 = D1; D1 = rot_l(C1, 10); C1 = B1; B1 = T;

                    T = rot_l(A2 + constants.f((uint)(79 - j), B2, C2, D2) + M[i][constants.R2[j]] + constants.K2(j), constants.S2[j]) + E2;
                    A2 = E2; E2 = D2; D2 = rot_l(C2, 10); C2 = B2; B2 = T;

                    if (j == 15)
                    { T = B1; B1 = B2; B2 = T; }
                    if (j == 31)
                    { T = D1; D1 = D2; D2 = T; }
                    if (j == 47)
                    { T = A1; A1 = A2; A2 = T; }
                    if (j == 63)
                    { T = C1; C1 = C2; C2 = T; }
                    if (j == 79)
                    { T = E1; E1 = E2; E2 = T; }
                }
                h[0] += A1; h[1] += B1; h[2] += C1; h[3] += D1;
                h[4] += E1; h[5] += A2; h[6] += B2; h[7] += C2;
                h[8] += D2; h[9] += E2;
            }            

            return h;
        }

        public static UInt32 BitArrayToUInt32(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];

            Reverse(ref bits);            

            bits.CopyTo(ret, 0);
            //Array.Reverse(ret);

            var a = BitConverter.ToUInt32(ret, 0);
            string Hex = String.Format("{0:X}", a);
            mess_str.Add(Hex);
            mess_byte.Add(ret);

            return a;
        }

        
    }


}
