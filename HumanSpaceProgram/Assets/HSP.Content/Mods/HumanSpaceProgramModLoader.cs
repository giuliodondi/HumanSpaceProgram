﻿using System.IO;
using System.Reflection;
using UnityEngine;

namespace HSP.Content.Mods
{
    /// <summary>
    /// Constants regarding mods for HSP.
    /// </summary>
    public class HumanSpaceProgramModLoader
    {
        private static bool _modsLoaded = false; // can be replaced by an array of mod metadatas and exposed publicly later.

        // TODO - Later, a mod should be located in an appropriate folder, along with a `_mod.json` file containing ModMetadata (name, author, etc, and a version info for compatibility checking)
        //        If ModMetadata is not present, the mod should be skipped. Also things that load from GameData should enumerate the list of found mods, not the raw directories.

        private static void LoadAssembliesRecursive( string path )
        {
            foreach( var dllPath in Directory.GetFiles( path, "*.dll" ) )
            {
                byte[] assemblyBytes = File.ReadAllBytes( dllPath );
                Assembly.Load( assemblyBytes );
            }

            foreach( var subfolder in Directory.GetDirectories( path ) )
            {
                LoadAssembliesRecursive( subfolder );
            }
        }

        /// <summary>
        /// Loads all of the assemblies (.dll) in the mod directory.
        /// </summary>
        public static void LoadModAssemblies()
        {
            string modDirectory = HumanSpaceProgramContent.GetContentDirectoryPath();

            if( !Directory.Exists( modDirectory ) )
                Directory.CreateDirectory( modDirectory );

            Debug.Log( $"The mod directory is: '{modDirectory}'" );

            _modsLoaded = true;

            LoadAssembliesRecursive( modDirectory );
        }
    }
}