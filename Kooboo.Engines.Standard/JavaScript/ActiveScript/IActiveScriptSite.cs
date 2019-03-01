//Copyright (c) 2018 Yardi Technology Limited. Http://www.kooboo.com 
//All rights reserved.
namespace SassAndCoffee.JavaScript.ActiveScript {
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Implemented by the host to create a site for the Windows Script engine. Usually, this site
    /// will be associated with the container of all the objects that are visible to the script
    /// (for example, the ActiveX Controls). Typically, this container will correspond to the document
    /// or page being viewed. Microsoft Internet Explorer, for example, would create such a container
    /// for each HTML page being displayed. Each ActiveX control (or other automation object) on the
    /// page, and the scripting engine itself, would be enumerable within this container.
    /// </summary>
    [Guid("DB01A1E3-A42B-11cf-8F20-00805F2CD064")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IActiveScriptSite {
        /// <summary>
        /// Retrieves the locale identifier associated with the host's user interface. The scripting
        /// engine uses the identifier to ensure that error strings and other user-interface elements
        /// generated by the engine appear in the appropriate language.
        /// </summary>
        /// <param name="lcid">A variable that receives the locale identifier for user-interface
        /// elements displayed by the scripting engine.</param>
        void GetLCID(out int lcid);

        /// <summary>
        /// Allows the scripting engine to obtain information about an item added with the
        /// IActiveScript.AddNamedItem method.
        /// </summary>
        /// <param name="name">The name associated with the item, as specified in the
        /// IActiveScript.AddNamedItem method.</param>
        /// <param name="returnMask">A bit mask specifying what information about the item should be
        /// returned. The scripting engine should request the minimum amount of information possible
        /// because some of the return parameters (for example, ITypeInfo) can take considerable
        /// time to load or generate.</param>
        /// <param name="item">A variable that receives a pointer to the IUnknown interface associated
        /// with the given item. The scripting engine can use the IUnknown.QueryInterface method to
        /// obtain the IDispatch interface for the item. This parameter receives null if returnMask
        /// does not include the ScriptInfo.IUnknown value. Also, it receives null if there is no
        /// object associated with the item name; this mechanism is used to create a simple class when
        /// the named item was added with the ScriptItem.CodeOnly flag set in the
        /// IActiveScript.AddNamedItem method.</param>
        /// <param name="typeInfo">A variable that receives a pointer to the ITypeInfo interface
        /// associated with the item. This parameter receives null if returnMask does not include the
        /// ScriptInfo.ITypeInfo value, or if type information is not available for this item. If type
        /// information is not available, the object cannot source events, and name binding must be
        /// realized with the IDispatch.GetIDsOfNames method. Note that the ITypeInfo interface
        /// retrieved describes the item's coclass (TKIND_COCLASS) because the object may support
        /// multiple interfaces and event interfaces. If the item supports the IProvideMultipleTypeInfo
        /// interface, the ITypeInfo interface retrieved is the same as the index zero ITypeInfo that
        /// would be obtained using the IProvideMultipleTypeInfo.GetInfoOfIndex method.</param>
        void GetItemInfo(
            [MarshalAs(UnmanagedType.LPWStr)] string name,
            ScriptInfoFlags returnMask,
            [MarshalAs(UnmanagedType.IUnknown)] out object item,
            out IntPtr typeInfo);

        /// <summary>
        /// Retrieves a host-defined string that uniquely identifies the current document version. If
        /// the related document has changed outside the scope of Windows Script (as in the case of an
        /// HTML page being edited with Notepad), the scripting engine can save this along with its
        /// persisted state, forcing a recompile the next time the script is loaded.
        /// </summary>
        /// <param name="versionString">The host-defined document version string.</param>
        void GetDocVersionString([MarshalAs(UnmanagedType.BStr)] out string versionString);

        /// <summary>
        /// Informs the host that the script has completed execution.
        /// </summary>
        /// <param name="result">A variable that contains the script result, or null if the script
        /// produced no result.</param>
        /// <param name="exceptionInfo">Contains exception information generated when the script
        /// terminated, or null if no exception was generated.</param>
        void OnScriptTerminate(object result, System.Runtime.InteropServices.ComTypes.EXCEPINFO exceptionInfo);

        /// <summary>
        /// Informs the host that the scripting engine has changed states.
        /// </summary>
        /// <param name="scriptState">Indicates the new script state.</param>
        void OnStateChange(ScriptState scriptState);

        /// <summary>
        /// Informs the host that an execution error occurred while the engine was running the script.
        /// </summary>
        /// <param name="scriptError">A host can use this interface to obtain information about the
        /// execution error.</param>
        void OnScriptError(IActiveScriptError scriptError);

        /// <summary>
        /// Informs the host that the scripting engine has begun executing the script code.
        /// </summary>
        void OnEnterScript();

        /// <summary>
        /// Informs the host that the scripting engine has returned from executing script code.
        /// </summary>
        void OnLeaveScript();
    }
}
