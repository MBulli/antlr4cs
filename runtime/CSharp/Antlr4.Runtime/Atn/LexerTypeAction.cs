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
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Sharpen;

namespace Antlr4.Runtime.Atn
{
    /// <summary>
    /// Implements the
    /// <code>type</code>
    /// lexer action by calling
    /// <see cref="Antlr4.Runtime.Lexer.Type">Antlr4.Runtime.Lexer.Type</see>
    /// with the assigned type.
    /// </summary>
    /// <author>Sam Harwell</author>
    /// <since>4.2</since>
    public class LexerTypeAction : ILexerAction
    {
        private readonly int type;

        /// <summary>
        /// Constructs a new
        /// <code>type</code>
        /// action with the specified token type value.
        /// </summary>
        /// <param name="type">
        /// The type to assign to the token using
        /// <see cref="Antlr4.Runtime.Lexer.Type">Antlr4.Runtime.Lexer.Type</see>
        /// .
        /// </param>
        public LexerTypeAction(int type)
        {
            this.type = type;
        }

        /// <summary>Gets the type to assign to a token created by the lexer.</summary>
        /// <remarks>Gets the type to assign to a token created by the lexer.</remarks>
        /// <returns>The type to assign to a token created by the lexer.</returns>
        public virtual int GetType()
        {
            return type;
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        /// <returns>
        /// This method returns
        /// <see cref="LexerActionType.Type">LexerActionType.Type</see>
        /// .
        /// </returns>
        public virtual LexerActionType GetActionType()
        {
            return LexerActionType.Type;
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        /// <returns>
        /// This method returns
        /// <code>false</code>
        /// .
        /// </returns>
        public virtual bool IsPositionDependent()
        {
            return false;
        }

        /// <summary>
        /// <inheritDoc></inheritDoc>
        /// <p>This action is implemented by calling
        /// <see cref="Antlr4.Runtime.Lexer.Type(int)">Antlr4.Runtime.Lexer.Type(int)</see>
        /// with the
        /// value provided by
        /// <see cref="GetType()">GetType()</see>
        /// .</p>
        /// </summary>
        public virtual void Execute(Lexer lexer)
        {
            lexer.Type = type;
        }

        public override int GetHashCode()
        {
            int hash = MurmurHash.Initialize();
            hash = MurmurHash.Update(hash, (int)(GetActionType()));
            hash = MurmurHash.Update(hash, type);
            return MurmurHash.Finish(hash, 2);
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }
            else
            {
                if (!(obj is Antlr4.Runtime.Atn.LexerTypeAction))
                {
                    return false;
                }
            }
            return type == ((Antlr4.Runtime.Atn.LexerTypeAction)obj).type;
        }

        public override string ToString()
        {
            return string.Format("type(%d)", type);
        }
    }
}
