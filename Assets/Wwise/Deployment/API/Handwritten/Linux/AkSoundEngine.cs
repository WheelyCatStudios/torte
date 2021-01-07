public partial class AkSoundEngine
{
#if (UNITY_STANDALONE_LINUX && !UNITY_EDITOR) || UNITY_EDITOR_LINUX
	/// <summary>
	///     Converts "AkOSChar*" C-strings to C# strings.
	/// </summary>
	/// <param name="ptr">"AkOSChar*" memory pointer passed to C# as an IntPtr.</param>
	/// <returns>Converted string.</returns>
	public static string StringFromIntPtrOSString(System.IntPtr ptr)
	{
		return StringFromIntPtrString(ptr);
	}
#endif
}

