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
using System.Reflection;
using System.IO;

namespace de.christianleberfinger.dotnet.pocketknife.Reflection
{
    /// <summary>
    /// Provides several static methods that can be used to find Types that implement a 
    /// certain interface. Useful for dynamic loading of Types with a certain interface,
    /// e.g. for the purpose of a plugin.
    /// </summary>
    public class InterfaceQuery
    {
        /// <summary>
        /// Returns all valid (loadable) dll files in the given path.
        /// </summary>
        /// <param name="path">The path the dll files are located in.</param>
        /// <param name="searchOption">Search option for finding the dll files.</param>
        /// <returns>The filenames of all loadable assemblies in the given path.</returns>
        public static string[] getAssemblyFiles(string path, SearchOption searchOption)
        {
            string[] dllFiles = Directory.GetFiles(path, "*.dll", searchOption);

            List<string> assemblyFiles = new List<string>();
            foreach (string dllFile in dllFiles)
            {
                if (!File.Exists(dllFile))
                    continue;

                try
                {
                    // try to load the dll file as assembly
                    Assembly asm = Assembly.LoadFrom(dllFile);

                    // store file in list of valid assemblies.
                    assemblyFiles.Add(dllFile);
                }
                catch
                {
                    // Error loading the assembly... could be a dll-file that isn't a C# assembly
                }
            }

            return assemblyFiles.ToArray();
        }

        /// <summary>
        /// Returns all Types in a given Assembly file that implement the given interface.
        /// </summary>
        /// <param name="interfaceToFind">The interface of interest.</param>
        /// <param name="assemblyFile">The assembly file that should be searched in.</param>
        /// <returns>All Types in a given Assembly file that implement the given interface.</returns>
        public static Type[] getImplementingTypes(Type interfaceToFind, string assemblyFile)
        {
            return getImplementingTypes(interfaceToFind, new string[] { assemblyFile });
        }

        /// <summary>
        /// Returns all Types in a given list of Assembly files that implement the given interface.
        /// </summary>
        /// <param name="interfaceToFind">The interface of interest.</param>
        /// <param name="assemblyFiles">The assembly files that should be searched in.</param>
        /// <returns>All Types in the given Assembly files that implement the given interface.</returns>
        public static Type[] getImplementingTypes(Type interfaceToFind, string[] assemblyFiles)
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (string assemblyFile in assemblyFiles)
            {
                if (!File.Exists(assemblyFile))
                    continue;
                
                try
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyFile);
                    assemblies.Add(assembly);
                }
                catch
                {
                    // Assembly couldn't be loaded.
                }
            }

            return getImplementingTypes(interfaceToFind, assemblies.ToArray());
        }

        /// <summary>
        /// Returns all Types in a given list of Assemblies that implement the given interface.
        /// </summary>
        /// <param name="interfaceToFind">The interface of interest.</param>
        /// <param name="assemblies">The assemblies that should be searched in.</param>
        /// <returns>All Types in the given Assemblies that implement the given interface.</returns>
        public static Type[] getImplementingTypes(Type interfaceToFind, Assembly[] assemblies)
        {
            List<Type> implementingTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] typesOfAssembly = assembly.GetTypes();
                foreach (Type type in typesOfAssembly)
                {
                    // der aktuelle Type implementiert das gesuchte Interface
                    //
                    if (null != type.GetInterface(interfaceToFind.FullName))
                    {
                        implementingTypes.Add(type);
                    }
                }
            }

            return implementingTypes.ToArray();
        }

    }
}
