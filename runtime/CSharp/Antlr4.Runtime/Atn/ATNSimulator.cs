/*
 * [The "BSD license"]
 *  Copyright (c) 2013 Terence Parr
 *  Copyright (c) 2013 Sam Harwell
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 *  IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 *  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 *  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 *  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Misc;
using Sharpen;

namespace Antlr4.Runtime.Atn
{
    public abstract class ATNSimulator
    {
        [Obsolete(@"Use ATNDeserializer.SerializedVersion instead.")]
        public static readonly int SerializedVersion = ATNDeserializer.SerializedVersion;

        /// <summary>This is the current serialized UUID.</summary>
        /// <remarks>This is the current serialized UUID.</remarks>
        [Obsolete(@"Use ATNDeserializer.CheckCondition(bool) instead.")]
        public static readonly Guid SerializedUuid = ATNDeserializer.SerializedUuid;

        public const char RuleVariantDelimiter = '$';

        public const string RuleLfVariantMarker = "$lf$";

        public const string RuleNolfVariantMarker = "$nolf$";

        /// <summary>Must distinguish between missing edge and edge we know leads nowhere</summary>
        [NotNull]
        public static readonly DFAState Error =
            new DFAState(new ATNConfigSet(), 0, 0)
            {
                stateNumber = int.MaxValue
            };

        [NotNull]
        public readonly ATN atn;

        public ATNSimulator(ATN atn)
        {
            this.atn = atn;
        }

        public abstract void Reset();

        [Obsolete(@"Use ATNDeserializer.Deserialize(char[]) instead.")]
        public static ATN Deserialize(char[] data)
        {
            return new ATNDeserializer().Deserialize(data);
        }

        [Obsolete(@"Use ATNDeserializer.CheckCondition(bool) instead.")]
        public static void CheckCondition(bool condition)
        {
            new ATNDeserializer().CheckCondition(condition);
        }

        [Obsolete(@"Use ATNDeserializer.CheckCondition(bool, string) instead.")]
        public static void CheckCondition(bool condition, string message)
        {
            new ATNDeserializer().CheckCondition(condition, message);
        }

        [Obsolete(@"Use ATNDeserializer.ToInt(char) instead.")]
        public static int ToInt(char c)
        {
            return ATNDeserializer.ToInt(c);
        }

        [Obsolete(@"Use ATNDeserializer.ToInt32(char[], int) instead.")]
        public static int ToInt32(char[] data, int offset)
        {
            return ATNDeserializer.ToInt32(data, offset);
        }

        [Obsolete(@"Use ATNDeserializer.ToLong(char[], int) instead.")]
        public static long ToLong(char[] data, int offset)
        {
            return ATNDeserializer.ToLong(data, offset);
        }

        [Obsolete(@"Use ATNDeserializer.ToUUID(char[], int) instead.")]
        public static Guid ToUUID(char[] data, int offset)
        {
            return ATNDeserializer.ToUUID(data, offset);
        }

        [return: NotNull]
        [Obsolete(@"Use ATNDeserializer.EdgeFactory(ATN, TransitionType, int, int, int, int, int, System.Collections.Generic.IList{E}) instead.")]
        public static Transition EdgeFactory(ATN atn, TransitionType type, int src, int trg, int arg1, int arg2, int arg3, IList<IntervalSet> sets)
        {
            return new ATNDeserializer().EdgeFactory(atn, type, src, trg, arg1, arg2, arg3, sets);
        }

        [Obsolete(@"Use ATNDeserializer.StateFactory(StateType, int) instead.")]
        public static ATNState StateFactory(StateType type, int ruleIndex)
        {
            return new ATNDeserializer().StateFactory(type, ruleIndex);
        }
    }
}
