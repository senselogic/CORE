#if UNITY_EDITOR
// -- IMPORTS

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CORE;

// -- TYPES

namespace CORE
{
    [InitializeOnLoad]
    public class UPDATE_SORTER
    {
        // -- TYPES

        public class SCRIPT
        {
            // -- ATTRIBUTES

            public MonoScript
                MonoScript_;
            public int
                OldExecutionOrder,
                ExecutionOrder;
            public Type
                Type;
            public string
                TypeName;
            public int
                ExecutionOrderSign;
            public string[]
                PriorTypeNameArray,
                NextTypeNameArray;

            // -- INQUIRIES

            public void Log(
                )
            {
                Debug.Log(
                    "Script : "
                    + TypeName
                    + " "
                    + OldExecutionOrder
                    + " "
                    + ExecutionOrder
                    + " "
                    + ExecutionOrderSign
                    + " [ "
                    + String.Join( " ", PriorTypeNameArray )
                    + " ] / [ "
                    + String.Join( " ", NextTypeNameArray )
                    + " ]"
                    );
            }
        }

        // ~~

        public class SCRIPT_DEPENDENCY
        {
            // -- ATTRIBUTES

            public SCRIPT
                PriorScript,
                NextScript;

            // -- CONSTRUCTORS

            public SCRIPT_DEPENDENCY(
                SCRIPT prior_script,
                SCRIPT next_script
                )
            {
                PriorScript = prior_script;
                NextScript = next_script;
            }

            // -- INQUIRIES

            public void Log(
                )
            {
                Debug.Log(
                    "Dependency : "
                    + PriorScript.TypeName
                    + " "
                    + PriorScript.ExecutionOrder
                    + " "
                    + PriorScript.ExecutionOrderSign
                    + " < "
                    + NextScript.TypeName
                    + " "
                    + NextScript.ExecutionOrder
                    + " "
                    + NextScript.ExecutionOrderSign
                    );
            }
        }

        // -- ATTRIBUTES

        #if UNITY_EDITOR
        public static MonoScript[]
            MonoScriptArray;
        #endif
        public static List<SCRIPT>
            ScriptList;
        public static Dictionary<string, SCRIPT>
            ScriptDictionary;
        public static List<SCRIPT_DEPENDENCY>
            ScriptDependencyList;

        // -- CONSTRUCTORS

        static UPDATE_SORTER(
            )
        {
            BuildMonoScriptArray();
            BuildScriptList();
            BuildScriptDependencyList();
            BuildScriptExecutionOrder();
            CheckScriptExecutionOrder();
            ApplyScriptExecutionOrder();
            Clear();
        }

        // -- INQUIRIES

        public static int GetSign(
            int integer
            )
        {
            if ( integer < 0 )
            {
                return -1;
            }
            else if ( integer > 0 )
            {
                return 0;
            }
            else
            {
                return 0;
            }
        }

        // ~~

        public static int GetScriptComparison(
            SCRIPT first_script,
            SCRIPT second_script
            )
        {
            return GetSign( first_script.ExecutionOrder - second_script.ExecutionOrder );
        }

        // ~~

        public static void Log(
            )
        {
            foreach ( SCRIPT script in ScriptList )
            {
                script.Log();
            }

            foreach ( SCRIPT_DEPENDENCY script_dependency in ScriptDependencyList )
            {
                script_dependency.Log();
            }
        }

        // -- OPERATIONS

        public static void BuildMonoScriptArray(
            )
        {
            MonoScriptArray = MonoImporter.GetAllRuntimeMonoScripts();
        }

        // ~~

        public static void BuildScriptList(
            )
        {
            Attribute[]
                attribute_array;
            Type
                mono_script_type;
            SCRIPT
                script;
            UPDATE
                script_update;

            ScriptList = new List<SCRIPT>();
            ScriptDictionary = new Dictionary<string, SCRIPT>();

            foreach ( MonoScript mono_script in MonoScriptArray )
            {
                mono_script_type = mono_script.GetClass();

                if ( mono_script_type != null
                     && mono_script_type.Name != null
                     && mono_script_type.Name != "" )
                {
                    attribute_array = Attribute.GetCustomAttributes( mono_script_type, typeof( UPDATE ) );

                    Debug.Assert( attribute_array.Length <= 1 );

                    if ( attribute_array.Length == 1 )
                    {
                        script_update = ( ( UPDATE )attribute_array[ 0 ] );

                        script = new SCRIPT();
                        script.MonoScript_ = mono_script;
                        script.OldExecutionOrder = MonoImporter.GetExecutionOrder( mono_script );
                        script.ExecutionOrder = script.OldExecutionOrder;
                        script.Type = mono_script_type;
                        script.TypeName = mono_script_type.Name;
                        script.ExecutionOrderSign = script_update.ExecutionOrderSign;
                        script.PriorTypeNameArray = script_update.PriorTypeNameArray;
                        script.NextTypeNameArray = script_update.NextTypeNameArray;

                        if ( script.ExecutionOrderSign < 0
                                  && script.ExecutionOrder >= 0 )
                        {
                            script.ExecutionOrder = -100;
                        }
                        else if ( script.ExecutionOrderSign > 0
                                  && script.ExecutionOrder <= 0 )
                        {
                            script.ExecutionOrder = 100;
                        }
                        else if ( script.ExecutionOrderSign == 0
                                  && script.ExecutionOrder != 0 )
                        {
                            script.ExecutionOrder = 0;
                        }

                        Debug.Assert( !ScriptDictionary.ContainsKey( script.TypeName ) );

                        ScriptList.Add( script );
                        ScriptDictionary.Add( script.TypeName, script );
                    }
                }
            }

            ScriptList.Sort( GetScriptComparison );
        }

        // ~~

        public static void BuildScriptDependencyList(
            )
        {
            SCRIPT
                next_script,
                prior_script;

            ScriptDependencyList = new List<SCRIPT_DEPENDENCY>();

            foreach ( SCRIPT script in ScriptList )
            {
                foreach ( string prior_Typename in script.PriorTypeNameArray )
                {
                    if ( prior_Typename != "*" )
                    {
                        if ( ScriptDictionary.TryGetValue( prior_Typename, out prior_script ) )
                        {
                            ScriptDependencyList.Add( new SCRIPT_DEPENDENCY( prior_script, script ) );
                        }
                        else
                        {
                            Debug.LogWarning( "Unknown script type name : " + prior_Typename );

                            script.Log();
                        }
                    }
                }

                foreach ( string next_Typename in script.NextTypeNameArray )
                {
                    if ( next_Typename != "*" )
                    {
                        if ( ScriptDictionary.TryGetValue( next_Typename, out next_script ) )
                        {
                            ScriptDependencyList.Add( new SCRIPT_DEPENDENCY( script, next_script ) );
                        }
                        else
                        {
                            Debug.LogWarning( "Unknown script type name : " + next_Typename );

                            script.Log();
                        }
                    }
                }
            }
        }

        // ~~

        public static void BuildScriptExecutionOrder(
            )
        {
            bool
                execution_order_has_changed;
            int
                next_script_execution_order,
                pass_count,
                pass_index,
                prior_script_execution_order;
            SCRIPT
                next_script,
                prior_script;

            pass_count = ScriptDependencyList.Count * 2;

            for ( pass_index = 0;
                  pass_index < pass_count;
                  ++pass_index )
            {
                execution_order_has_changed = false;

                foreach ( SCRIPT_DEPENDENCY script_dependency in ScriptDependencyList )
                {
                    prior_script = script_dependency.PriorScript;
                    next_script = script_dependency.NextScript;
                    prior_script_execution_order = prior_script.ExecutionOrder;
                    next_script_execution_order = next_script.ExecutionOrder;

                    if ( prior_script_execution_order >= next_script_execution_order )
                    {
                        if ( prior_script.ExecutionOrderSign
                             != next_script.ExecutionOrderSign )
                        {
                            Debug.LogWarning( "Incompatible script execution order signs" );

                            script_dependency.Log();
                        }
                        else
                        {
                            if ( prior_script_execution_order != next_script_execution_order )
                            {
                                prior_script.ExecutionOrder = next_script_execution_order;
                                next_script.ExecutionOrder = prior_script_execution_order;
                            }
                            else
                            {
                                if ( prior_script.ExecutionOrderSign < 0 )
                                {
                                    prior_script.ExecutionOrder = next_script_execution_order - 10;
                                }
                                else
                                {
                                    next_script.ExecutionOrder = prior_script_execution_order + 10;
                                }
                            }
                        }

                        execution_order_has_changed = true;
                    }
                }

                if ( !execution_order_has_changed )
                {
                    return;
                }
            }
        }

        // ~~

        public static void CheckScriptExecutionOrder(
            )
        {
            SCRIPT
                next_script,
                prior_script;

            foreach ( SCRIPT script in ScriptList )
            {
                if ( GetSign( script.ExecutionOrder ) != script.ExecutionOrderSign )
                {
                    Debug.LogWarning( "Invalid script execution order sign" );

                    script.Log();
                }
            }

            foreach ( SCRIPT_DEPENDENCY script_dependency in ScriptDependencyList )
            {
                prior_script = script_dependency.PriorScript;
                next_script = script_dependency.NextScript;

                if ( prior_script.ExecutionOrder
                     >= next_script.ExecutionOrder )
                {
                    Debug.LogWarning( "Invalid script execution orders" );

                    script_dependency.Log();
                }
            }
        }

        // ~~

        public static void ApplyScriptExecutionOrder(
            )
        {
            foreach ( SCRIPT script in ScriptList )
            {
                if ( script.ExecutionOrder != script.OldExecutionOrder )
                {
                    MonoImporter.SetExecutionOrder( script.MonoScript_, script.ExecutionOrder );
                }
            }
        }

        // ~~

        public static void Clear(
            )
        {
            MonoScriptArray = null;
            ScriptList = null;
            ScriptDictionary = null;
            ScriptDependencyList = null;
        }
    }
}
#endif
