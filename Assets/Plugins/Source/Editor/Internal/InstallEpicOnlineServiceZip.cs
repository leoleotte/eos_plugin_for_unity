﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Playeveryware.Editor;
using System.Threading.Tasks;
using System.IO.Compression;

namespace PlayEveryWare.EpicOnlineServices
{
    public class InstallEpicOnlineServiceZip : UnityEditor.EditorWindow
    {
        private string pathToJSONPackageDescription;
        private string pathToZipFile;

        private string pathToImportDescDirectory;
        private PlatformImportInfoList importInfoList;

        [MenuItem("Tools/EOS Plugin/Install EOS zip")]
        public static void ShowWindow()
        {
            GetWindow(typeof(InstallEpicOnlineServiceZip), false, "Install EOS Zip", true);
        }

        static public void UnzipEntry(ZipArchiveEntry zipEntry, string pathName)
        {
            using (var destStream = File.OpenWrite(pathName))
            {
                zipEntry.Open().CopyTo(destStream);
            }
        }

        //-------------------------------------------------------------------------
        static public void UnzipFile(string pathToZipFile, string dest)
        {
            // unzip files
            using (var filestream = new FileStream(pathToZipFile, FileMode.Open))
            {
                using (var zipArchive = new ZipArchive(filestream))
                {
                    string extraPath = "";
                    //search for guaranteed file to check if SDK root is inside any extraneous subfolders
                    foreach (var zipEntry in zipArchive.Entries)
                    {
                        if (zipEntry.FullName.EndsWith("SDK/Tools/EOSBootstrapper.exe"))
                        {
                            extraPath = zipEntry.FullName.Replace("SDK/Tools/EOSBootstrapper.exe", "");
                            break;
                        }
                    }

                    int zipCount = zipArchive.Entries.Count;
                    float i = 0.0f;
                    foreach (var zipEntry in zipArchive.Entries)
                    {
                        if (string.IsNullOrWhiteSpace(zipEntry.Name))
                        {
                            i += 1.0f;
                            continue;
                        }

                        EditorUtility.DisplayProgressBar("Unzipping file", "Unzipping " + Path.GetFileName(pathToZipFile), i / zipCount);
                        string targetPath = zipEntry.FullName;
                        if (!string.IsNullOrEmpty(extraPath))
                        {
                            targetPath = targetPath.Replace(extraPath, "");
                        }
                        string pathName = Path.Combine(dest, targetPath);
                        string parentDirectory = Path.GetDirectoryName(pathName);
                        if (!Directory.Exists(parentDirectory))
                        {
                            Directory.CreateDirectory(parentDirectory);
                        }

                        UnzipEntry(zipEntry, pathName);
                        i += 1.0f;
                    }
                    EditorUtility.ClearProgressBar();
                }
            }
        }
        //-------------------------------------------------------------------------
        public string ToCapitalize(string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }
            return str.ToUpper();
        }

        //-------------------------------------------------------------------------
        private void Awake()
        {
            pathToImportDescDirectory = Application.dataPath + "/../etc/EOSImportDesriptions/";
            var JSONPackageDescription = File.ReadAllText(pathToImportDescDirectory + "eos_platform_import_info_list.json");
            importInfoList = JsonUtility.FromJson<PlatformImportInfoList>(JSONPackageDescription);
        }

        //-------------------------------------------------------------------------
        private void OnDestroy()
        {
            //JsonUtility.ToJson(importInfoList);
        }

        //-------------------------------------------------------------------------
        private void OnGUI()
        {
            GUILayout.Label("Install EOS Files into project");

            GUILayout.Label("JSON Description Path");
            GUILayout.BeginHorizontal(GUIStyle.none);
            if (GUILayout.Button("Select", GUILayout.Width(100)))
            {
                pathToJSONPackageDescription = EditorUtility.OpenFilePanel("Pick JSON Package Description", "", "json");
            }
            GUILayout.Label(pathToJSONPackageDescription);
            GUILayout.EndHorizontal();

            GUILayout.Label("Select Zip Path");
            GUILayout.BeginHorizontal(GUIStyle.none);
            if (GUILayout.Button("Select", GUILayout.Width(100)))
            {
                pathToZipFile = EditorUtility.OpenFilePanel("Pick Zip File", "", "zip");
            }
            GUILayout.Label(pathToZipFile);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Install"))
            {
                string tmpDir = PackageFileUtils.GenerateTemporaryBuildPath();

                try
                {
                    UnzipFile(pathToZipFile, tmpDir);

                    var toConvert = new List<string>();
                    // Convert files to a consistant line ending
                    foreach (var entity in Directory.EnumerateFiles(tmpDir, "*", SearchOption.AllDirectories))
                    {
                        if (Path.GetExtension(entity) == ".cs")
                        {
                            toConvert.Add(entity);
                        }
                    }

                    for (int i = 0; i < toConvert.Count; ++i)
                    {
                        var entity = toConvert[i];
                        EditorUtility.DisplayProgressBar("Converting line endings", Path.GetFileName(entity), (float)i / toConvert.Count);
                        PackageFileUtils.Dos2UnixLineEndings(entity);
                    }
                    EditorUtility.ClearProgressBar();


                    foreach (var platformImportInfo in importInfoList.platformImportInfoList)
                    {
                        if (platformImportInfo.isGettingImported)
                        {
                            var JSONPackageDescription = File.ReadAllText(pathToImportDescDirectory + platformImportInfo.descPath);
                            var packageDescription = JsonUtility.FromJson<PackageDescription>(JSONPackageDescription);

                            var fileResults = PackageFileUtils.GetFileInfoMatchingPackageDescription(tmpDir, packageDescription);
                            // This should be the correct directory
                            var projectDir = PackageFileUtils.GetProjectPath();
                            PackageFileUtils.CopyFilesToDirectory(projectDir, fileResults);
                        }
                    }

                }
                finally
                {
                    //clean up unzipped files on success or error
                    Directory.Delete(tmpDir, true);
                }
            }

            foreach (var platformImportInfo in importInfoList.platformImportInfoList)
            {
                EpicOnlineServicesConfigEditor.AssigningBoolField(platformImportInfo.platform, ref platformImportInfo.isGettingImported, 300);
            }

        }

    }
}