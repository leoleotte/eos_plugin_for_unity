// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.IntegratedPlatform
{
	/// <summary>
	/// Required initialization options to use with <see cref="Options" /> for Steam.
	/// Steamworks API needs to be at least v1.13
	/// Steam Sanitization requires at least v1.45
	/// <seealso cref="Options" />
	/// </summary>
	public struct SteamOptions
	{
		/// <summary>
		/// Usage of this parameter is dependent on the specified <see cref="IntegratedPlatformManagementFlags" />.
		/// 
		/// Optional with <see cref="IntegratedPlatformManagementFlags.LibraryManagedByApplication" />.
		/// Set to override the loaded library basename, or use <see langword="null" /> to assume the default basename by platform:
		/// 
		/// - Linux: libsteam_api.so,
		/// - macOS: libsteam_api.dylib,
		/// - Windows 32-bit: steam_api.dll,
		/// - Windows 64-bit: steam_api64.dll.
		/// 
		/// Required with <see cref="IntegratedPlatformManagementFlags.LibraryManagedBySDK" />.
		/// Set to a fully qualified file path to the Steamworks SDK runtime library on disk.
		/// </summary>
		public Utf8String OverrideLibraryPath { get; set; }

		/// <summary>
		/// Used to specify the major version of the Steam SDK your game is compiled against, e.g.:
		/// 
		/// Options.SteamMajorVersion = 1;
		/// </summary>
		public uint SteamMajorVersion { get; set; }

		/// <summary>
		/// Used to specify the minor version of the Steam SDK your game is compiled against, e.g.:
		/// 
		/// Options.SteamMinorVersion = 57;
		/// </summary>
		public uint SteamMinorVersion { get; set; }

		internal void Set(ref SteamOptionsInternal other)
		{
			OverrideLibraryPath = other.OverrideLibraryPath;
			SteamMajorVersion = other.SteamMajorVersion;
			SteamMinorVersion = other.SteamMinorVersion;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct SteamOptionsInternal : IGettable<SteamOptions>, ISettable<SteamOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_OverrideLibraryPath;
		private uint m_SteamMajorVersion;
		private uint m_SteamMinorVersion;

		public Utf8String OverrideLibraryPath
		{
			get
			{
				Utf8String value;
				Helper.Get(m_OverrideLibraryPath, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_OverrideLibraryPath);
			}
		}

		public uint SteamMajorVersion
		{
			get
			{
				return m_SteamMajorVersion;
			}

			set
			{
				m_SteamMajorVersion = value;
			}
		}

		public uint SteamMinorVersion
		{
			get
			{
				return m_SteamMinorVersion;
			}

			set
			{
				m_SteamMinorVersion = value;
			}
		}

		public void Set(ref SteamOptions other)
		{
			m_ApiVersion = IntegratedPlatformInterface.SteamOptionsApiLatest;
			OverrideLibraryPath = other.OverrideLibraryPath;
			SteamMajorVersion = other.SteamMajorVersion;
			SteamMinorVersion = other.SteamMinorVersion;
		}

		public void Set(ref SteamOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = IntegratedPlatformInterface.SteamOptionsApiLatest;
				OverrideLibraryPath = other.Value.OverrideLibraryPath;
				SteamMajorVersion = other.Value.SteamMajorVersion;
				SteamMinorVersion = other.Value.SteamMinorVersion;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_OverrideLibraryPath);
		}

		public void Get(out SteamOptions output)
		{
			output = new SteamOptions();
			output.Set(ref this);
		}
	}
}