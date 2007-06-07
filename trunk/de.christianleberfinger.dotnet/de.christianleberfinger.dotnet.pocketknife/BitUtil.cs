/*
 * 
 * Copyright (c) 2007 Christian Leberfinger
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a 
 * copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation 
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software 
 * is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
 * FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN 
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
 * WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.pocketknife
{
    /// <summary>
    /// A class that makes it easier to care about single bits of code in your programs.
    /// BitUtil offers static methods to mask single bits of integer types.
    /// </summary>
    public class BitUtil
    {
        /// <summary>
        /// Masks a single bit of a value of type 'long'. That means it picks one bit out of your long value.
        /// If you define an index that's out of range, an ArgumentException is thrown.
        /// </summary>
        /// <param name="value">The value that contains the bit that's of interest for you.</param>
        /// <param name="index">The zero based position of the bit you are interested in.</param>
        /// <returns>The selected bit.</returns>
        public static bool getBit(long value, byte index)
        {
            if(index > 63)
                throw new ArgumentException("The index is out of range for the offered datatype.");

            long mask = (long) Math.Pow(2, index);
            long masked = value & mask;
            long shifted = masked >> index;
            return shifted == 1;
        }

        /// <summary>
        /// Masks a single bit of a byte. That means it picks one bit out of your byte.
        /// If you define an index that's out of range, an ArgumentException is thrown.
        /// </summary>
        /// <param name="value">The value that contains the bit that's of interest for you.</param>
        /// <param name="index">The zero based position of the bit you are interested in.</param>
        /// <returns>The selected bit.</returns>
        public static bool getBit(int value, byte index)
        {
            if (index > 31)
                throw new ArgumentException("The index is out of range for the offered datatype.");

            return getBit((long)value, index); ;
        }  

        /// <summary>
        /// Masks a single bit of a byte. That means it picks one bit out of your byte.
        /// If you define an index that's out of range, an ArgumentException is thrown.
        /// </summary>
        /// <param name="value">The value that contains the bit that's of interest for you.</param>
        /// <param name="index">The zero based position of the bit you are interested in.</param>
        /// <returns>The selected bit.</returns>
        public static bool getBit(byte value, byte index)
        {
            if (index > 7)
                throw new ArgumentException("The index is out of range for the offered datatype.");

            return getBit((long)value, index); ;
        }

        /// <summary>
        /// Masks every bit in a given byte value and returns the eight bits in an array.
        /// </summary>
        /// <param name="b">The byte of interest.</param>
        /// <returns>The single bits of the given byte.</returns>
        public static bool[] toBitArray(byte b)
        {
            bool[] list = new bool[8];
            for (byte i = 0; i < list.Length; i++)
            {
                list[i] = getBit(b, i);
            }
            return list;
        }

        /// <summary>
        /// Masks every bit in a given integer value and returns the 32 bits in an array.
        /// </summary>
        /// <param name="integer">The integer of interest.</param>
        /// <returns>The single bits of the given integer value.</returns>
        public static bool[] toBitArray(int integer)
        {
            bool[] list = new bool[32];
            for (byte i = 0; i < list.Length; i++)
            {
                list[i] = getBit(integer, i);
            }
            return list;
        }

        /// <summary>
        /// Masks every bit in a given long value and returns the 64 bits in an array.
        /// </summary>
        /// <param name="l">The long value.</param>
        /// <returns>The single bits of the given long value.</returns>
        public static bool[] toBitArray(long l)
        {
            bool[] list = new bool[64];
            for (byte i = 0; i < list.Length; i++)
            {
                list[i] = getBit(l, i);
            }
            return list;
        }
    }
}
