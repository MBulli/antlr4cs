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
using Antlr4.Runtime;
using Sharpen;

namespace Antlr4.Runtime
{
    /// <summary>
    /// Provides an implementation of
    /// <see cref="ITokenSource">ITokenSource</see>
    /// as a wrapper around a list
    /// of
    /// <see cref="IToken">IToken</see>
    /// objects.
    /// <p>If the final token in the list is an
    /// <see cref="IToken.Eof">IToken.Eof</see>
    /// token, it will be used
    /// as the EOF token for every call to
    /// <see cref="NextToken()">NextToken()</see>
    /// after the end of the
    /// list is reached. Otherwise, an EOF token will be created.</p>
    /// </summary>
    public class ListTokenSource : ITokenSource
    {
        /// <summary>
        /// The wrapped collection of
        /// <see cref="IToken">IToken</see>
        /// objects to return.
        /// </summary>
        protected internal readonly IList<IToken> tokens;

        /// <summary>The name of the input source.</summary>
        /// <remarks>
        /// The name of the input source. If this value is
        /// <code>null</code>
        /// , a call to
        /// <see cref="SourceName()">SourceName()</see>
        /// should return the source name used to create the
        /// the next token in
        /// <see cref="tokens">tokens</see>
        /// (or the previous token if the end of
        /// the input has been reached).
        /// </remarks>
        private readonly string sourceName;

        /// <summary>
        /// The index into
        /// <see cref="tokens">tokens</see>
        /// of token to return by the next call to
        /// <see cref="NextToken()">NextToken()</see>
        /// . The end of the input is indicated by this value
        /// being greater than or equal to the number of items in
        /// <see cref="tokens">tokens</see>
        /// .
        /// </summary>
        protected internal int i;

        /// <summary>This field caches the EOF token for the token source.</summary>
        /// <remarks>This field caches the EOF token for the token source.</remarks>
        protected internal IToken eofToken;

        /// <summary>
        /// This is the backing field for
        /// <see cref="TokenFactory()">TokenFactory()</see>
        /// and
        /// <see cref="setTokenFactory">setTokenFactory</see>
        /// .
        /// </summary>
        private ITokenFactory _factory = CommonTokenFactory.Default;

        /// <summary>
        /// Constructs a new
        /// <see cref="ListTokenSource">ListTokenSource</see>
        /// instance from the specified
        /// collection of
        /// <see cref="IToken">IToken</see>
        /// objects.
        /// </summary>
        /// <param name="tokens">
        /// The collection of
        /// <see cref="IToken">IToken</see>
        /// objects to provide as a
        /// <see cref="ITokenSource">ITokenSource</see>
        /// .
        /// </param>
        /// <exception>
        /// NullPointerException
        /// if
        /// <code>tokens</code>
        /// is
        /// <code>null</code>
        /// </exception>
        public ListTokenSource(IList<IToken> tokens)
            : this(tokens, null)
        {
        }

        /// <summary>
        /// Constructs a new
        /// <see cref="ListTokenSource">ListTokenSource</see>
        /// instance from the specified
        /// collection of
        /// <see cref="IToken">IToken</see>
        /// objects and source name.
        /// </summary>
        /// <param name="tokens">
        /// The collection of
        /// <see cref="IToken">IToken</see>
        /// objects to provide as a
        /// <see cref="ITokenSource">ITokenSource</see>
        /// .
        /// </param>
        /// <param name="sourceName">
        /// The name of the
        /// <see cref="ITokenSource">ITokenSource</see>
        /// . If this value is
        /// <code>null</code>
        /// ,
        /// <see cref="SourceName()">SourceName()</see>
        /// will attempt to infer the name from
        /// the next
        /// <see cref="IToken">IToken</see>
        /// (or the previous token if the end of the input has
        /// been reached).
        /// </param>
        /// <exception>
        /// NullPointerException
        /// if
        /// <code>tokens</code>
        /// is
        /// <code>null</code>
        /// </exception>
        public ListTokenSource(IList<IToken> tokens, string sourceName)
        {
            if (tokens == null)
            {
                throw new ArgumentNullException("tokens cannot be null");
            }
            this.tokens = tokens;
            this.sourceName = sourceName;
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual int Column
        {
            get
            {
                if (i < tokens.Count)
                {
                    return tokens[i].Column;
                }
                else
                {
                    if (eofToken != null)
                    {
                        return eofToken.Column;
                    }
                    else
                    {
                        if (tokens.Count > 0)
                        {
                            // have to calculate the result from the line/column of the previous
                            // token, along with the text of the token.
                            IToken lastToken = tokens[tokens.Count - 1];
                            string tokenText = lastToken.Text;
                            if (tokenText != null)
                            {
                                int lastNewLine = tokenText.LastIndexOf('\n');
                                if (lastNewLine >= 0)
                                {
                                    return tokenText.Length - lastNewLine - 1;
                                }
                            }
                            return lastToken.Column + lastToken.StopIndex - lastToken.StartIndex + 1;
                        }
                    }
                }
                // only reach this if tokens is empty, meaning EOF occurs at the first
                // position in the input
                return 0;
            }
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual IToken NextToken()
        {
            if (i >= tokens.Count)
            {
                if (eofToken == null)
                {
                    int start = -1;
                    if (tokens.Count > 0)
                    {
                        int previousStop = tokens[tokens.Count - 1].StopIndex;
                        if (previousStop != -1)
                        {
                            start = previousStop + 1;
                        }
                    }
                    int stop = Math.Max(-1, start - 1);
                    eofToken = _factory.Create(Tuple.Create((ITokenSource)this, InputStream), TokenConstants.Eof, "EOF", TokenConstants.DefaultChannel, start, stop, Line, Column);
                }
                return eofToken;
            }
            IToken t = tokens[i];
            if (i == tokens.Count - 1 && t.Type == TokenConstants.Eof)
            {
                eofToken = t;
            }
            i++;
            return t;
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual int Line
        {
            get
            {
                if (i < tokens.Count)
                {
                    return tokens[i].Line;
                }
                else
                {
                    if (eofToken != null)
                    {
                        return eofToken.Line;
                    }
                    else
                    {
                        if (tokens.Count > 0)
                        {
                            // have to calculate the result from the line/column of the previous
                            // token, along with the text of the token.
                            IToken lastToken = tokens[tokens.Count - 1];
                            int line = lastToken.Line;
                            string tokenText = lastToken.Text;
                            if (tokenText != null)
                            {
                                for (int j = 0; j < tokenText.Length; j++)
                                {
                                    if (tokenText[j] == '\n')
                                    {
                                        line++;
                                    }
                                }
                            }
                            // if no text is available, assume the token did not contain any newline characters.
                            return line;
                        }
                    }
                }
                // only reach this if tokens is empty, meaning EOF occurs at the first
                // position in the input
                return 1;
            }
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual ICharStream InputStream
        {
            get
            {
                if (i < tokens.Count)
                {
                    return tokens[i].InputStream;
                }
                else
                {
                    if (eofToken != null)
                    {
                        return eofToken.InputStream;
                    }
                    else
                    {
                        if (tokens.Count > 0)
                        {
                            return tokens[tokens.Count - 1].InputStream;
                        }
                    }
                }
                // no input stream information is available
                return null;
            }
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual string SourceName
        {
            get
            {
                if (sourceName != null)
                {
                    return sourceName;
                }
                ICharStream inputStream = InputStream;
                if (inputStream != null)
                {
                    return inputStream.SourceName;
                }
                return "List";
            }
        }

        /// <summary><inheritDoc></inheritDoc></summary>
        /// <summary><inheritDoc></inheritDoc></summary>
        public virtual ITokenFactory TokenFactory
        {
            get
            {
                return _factory;
            }
            set
            {
                ITokenFactory factory = value;
                this._factory = factory;
            }
        }
    }
}
