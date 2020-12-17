using ConversionTests.Conversion;
using ConversionTests.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionTests
{
	class Program
	{
		static void Main(string[] args)
		{
			if (!CheckImportDataPathAndExportDataPath(args, out string error, out string importDataPath, out string exportDataPath))
			{
				Console.WriteLine(error);
				WaitForUserInputThenExit();
			}
			if (importDataPath == "" || exportDataPath == "")
			{
				Console.WriteLine("importDataPath == '' || exportDataPath == ''");
				WaitForUserInputThenExit();
			}

			var rootFolders = GetListOfTaskdataFolders(importDataPath);
			if (rootFolders == null)
			{
				Console.WriteLine("rootFolders == null");
				WaitForUserInputThenExit();
			}

			int count = 0;
			int totalFolders = rootFolders.Count();
			foreach (var folder in rootFolders)
			{
				count++;
				Console.WriteLine($"Starting ADAPT conversion for folder {Path.GetFileName(folder)} ({count}/{totalFolders})");

				// Selecting import and export plugin
				// Using Nuget package
				var isoxmlPlugin = new AgGateway.ADAPT.ISOv4Plugin.Plugin();
				// Using reference to project
				var geoJsonPlugin = new GeoJSONPlugin.Plugin();

				try
				{
					if (AdaptConverter.Convert(isoxmlPlugin, geoJsonPlugin, folder, exportDataPath))
					{
						Console.WriteLine($"Successfull ADAPT conversion for folder {folder}");
						continue;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine($"Unsuccesfull conversion for folder {Path.GetFileName(folder)}");
					Console.WriteLine($"Exception: {e.Message} InnerException: {e.InnerException?.Message}");
				}

			}
			Console.WriteLine($"ADAPT conversion succesfull for folders in {importDataPath} to folder {exportDataPath}");
			WaitForUserInputThenExit();
		}

		private static void WaitForUserInputThenExit()
		{
			Console.ReadLine();
			Environment.Exit(-1);
		}

		private static bool CheckImportDataPathAndExportDataPath(string[] args, out string error, out string importDataPath, out string exportDataPath)
		{
			error = "";
			importDataPath = "";
			exportDataPath = "";
			if (args != null)
			{
				if (args.Count() == 2)
				{
					importDataPath = args[0];
					if (!Directory.Exists(importDataPath))
					{
						error += "import path does not exist, ";
					}
					exportDataPath = args[1];
					if (!Directory.Exists(exportDataPath))
					{
						error += "export path does not exist, ";
					}

					if (error == "")
					{
						return true;
					}
				}
			}
			else
			{
				error += "import (1) and export(2) datapath needed, ";
			}
			error += "stopping conversion..";
			return false;
		}

		private static List<string> GetListOfTaskdataFolders(string dataPath)
		{
			var taskdataFolders = new List<string>();
			taskdataFolders.Add(dataPath);
			return taskdataFolders;
		}
	}
}
