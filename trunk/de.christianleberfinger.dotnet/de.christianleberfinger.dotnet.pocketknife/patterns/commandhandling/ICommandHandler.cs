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

namespace de.christianleberfinger.dotnet.pocketknife.patterns.commandhandling
{
    /// <summary>
    /// Provides an interface that specifies a commandhandler.
    /// A commandhandler is a small class that encapsulates the handling of an incoming message.
    /// It should be specific to one or few message ids in order to keep the complexity low.
    /// </summary>
    /// <typeparam name="ID">Defines the type of the message IDs, e.g. byte, string</typeparam>
    /// <typeparam name="CMD">Defines the type of the incoming messages.</typeparam>
    public interface ICommandHandler<ID, CMD> where CMD:ICommand<ID>
    {
        /// <summary>
        /// Defines to which message IDs the commandhandler reacts.
        /// </summary>
        ID[] CommandIdentifiers { get; }

        /// <summary>
        /// Handles a given commmand.
        /// </summary>
        /// <param name="command"></param>
        void handleCommand(CMD command);
    }
}
