﻿/*
	This file is part of KSPe, a component for KSP Enhanced /L
	unless when specified otherwise below this code is:
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
    along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/

using SIO = System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace KSPe.Util
{
	public static class CkanTools
	{
		public const string CKAN_URL = "https://ksp.lisias.net/blogs/rants/on-CKAN/";

		[System.Serializable]
		public class Registry
		{
			[System.Serializable]
			public class Repository
			{
				public string last_server_etag;
				public string name;
				public int priority;
				public string uri;
			}

			[System.Serializable]
			public class Module
			{
			}

			public string registry_version;
			public Dictionary<string,Repository> sorted_repositories;
			//public Dictionary<string,Module> available_modules;
		}

		private static bool? is_ckan_installed = null;
		public static bool CheckCkanInstalled()
		{
			if (null != is_ckan_installed) return (bool)is_ckan_installed;

			string path = SIO.Path.Combine(KSPUtil.ApplicationRootPath, "CKAN");
			is_ckan_installed = SIO.Directory.Exists(path);
			path = SIO.Path.Combine(path, "registry.json");
			is_ckan_installed &= SIO.File.Exists(path);
#if false
			if ((bool)is_ckan_installed)
			{
				string text = SIO.File.ReadAllText(path);
				Registry registry = JsonUtility.FromJson<Registry>(text);
				is_ckan_installed = registry.sorted_repositories.ContainsKey("default");
				if (!(bool)is_ckan_installed) return false;

				is_ckan_installed = null != registry.sorted_repositories["default"].uri;
				if (!(bool)is_ckan_installed) return false;

				is_ckan_installed = registry.sorted_repositories["default"].uri.StartsWith("https://github.com/KSP-CKAN/");
			}
#endif
			return (bool)is_ckan_installed;
		}

		public static void OpenURL(string url)
		{
			Application.OpenURL(CheckCkanInstalled() ? CKAN_URL : url);
		}

	}
}