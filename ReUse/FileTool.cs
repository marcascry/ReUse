using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ReUse.Option;
using SearchOption = System.IO.SearchOption;

namespace ReUse
{
    public class FileTool
    {


        #region Get

        /// <summary>
        /// <para>Get all file in the directory</para>
        /// <para>Default Level 1</para>
        /// </summary>
        public static List<FileInfo> GetFiles(String path, SearchOption option = SearchOption.TopDirectoryOnly, string pattern = "*")
        {
            DirectoryInfo pathInfo = new DirectoryInfo(path);
            List<FileInfo> fileList = new List<FileInfo>();
            if (pathInfo.Exists)
            {
                fileList = pathInfo.GetFiles(pattern,option).ToList();
            }

            return fileList;
        }

        


        /// <summary>
        /// <para>Get all folder in the directory</para>
        /// <para>Default Level 1</para>
        /// </summary>
        public static List<DirectoryInfo> GetFolders(String path, SearchOption option = SearchOption.TopDirectoryOnly, string pattern = "*")
        {
            DirectoryInfo pathInfo = new DirectoryInfo(path);
            List<DirectoryInfo> folderList = new List<DirectoryInfo>();
            if (pathInfo.Exists)
            {
                folderList = pathInfo.GetDirectories(pattern, option).ToList();
            }
            return folderList;
        }


        /// <summary>
        /// <para>Get all file in the directory</para>
        /// <para>Default Level All</para>
        /// </summary>
        public static List<FileInfo> GetFiles(String path, String key, SearchOption option = SearchOption.AllDirectories, string pattern = "*")
        {
            List<FileInfo> result = new List<FileInfo>();
            try
            {
                List<FileInfo> fileList = GetFiles(path, option, pattern);
                
                foreach (FileInfo file in fileList)
                {
                    if (file.Name.Contains(key))
                    {
                        result.Add(file);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
                return result;
            }
        }


        /// <summary>
        /// <para>Get all folder in the directory</para>
        /// <para>Default Level All</para>
        /// </summary>
        public static List<DirectoryInfo> GetFolders(String path, String key, SearchOption option = SearchOption.AllDirectories, string pattern = "*")
        {
            List<DirectoryInfo> result = new List<DirectoryInfo>();
            try
            {
                List<DirectoryInfo> folderList = GetFolders(path, option, pattern);
                foreach (DirectoryInfo folder in folderList)
                {
                    if (folder.Name.Contains(key))
                    {
                        result.Add(folder);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
                return result;
            }
        }

        /// <summary>
        /// <para>Get Size of file</para>
        /// </summary>
        public static String GetFileSize(FileInfo file)
        {
            long size = file.Length;
            int count = 0;
            while (size > 1000)
            {
                size /= 1000;
                count++;
            }

            return $"{size} {(SizeUnit)count}";
        }

        /// <summary>
        /// <para>Get Size of file</para>
        /// </summary>
        public static long GetFileLength(FileInfo file)
        {
            long size = file.Length;
            return size;
        }

        /// <summary>
        /// <para>Get Size of folder</para>
        /// </summary>
        public static String GetFolderSize(DirectoryInfo folder)
        {
            long size = 0;
            List<FileInfo> fileList = GetFiles(folder.FullName,SearchOption.AllDirectories);
            foreach (FileInfo file in fileList)
            {
                size += file.Length;
            }
            int count = 0;
            while (size > 1000)
            {
                size /= 1000;
                count++;
            }

            return $"{size} {(SizeUnit)count}";
        }

        /// <summary>
        /// <para>Get Size of folder</para>
        /// </summary>
        public static long GetFolderLength(DirectoryInfo folder)
        {
            long size = 0;
            List<FileInfo> fileList = GetFiles(folder.FullName, SearchOption.AllDirectories);
            foreach (FileInfo file in fileList)
            {
                size += file.Length;
            }
            return size;
        }
        #endregion

        #region Copy

        /// <summary>
        /// <para>Copy file</para>
        /// <para>Override</para>
        /// </summary>
        public static void CopyFile(String from, String to, SystemSafe safe=SystemSafe.Override)
        {
            try
            {
                if (File.Exists(from))
                {
                    to = FileSafeAction(to, safe);
                    if (!String.IsNullOrEmpty(to))
                        File.Copy(from, to);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// <para>Copy everything in the directory</para>
        /// <para>Level All</para>
        /// </summary>
        public static void CopyDirectory(String from, String to, SystemSafe safe=SystemSafe.Override)
        {
            try
            {
                Directory.CreateDirectory(to);
                List<DirectoryInfo> folderList = GetFolders(from);
                foreach (DirectoryInfo folder in folderList)
                {
                    CopyDirectory(folder.FullName, $@"{to}\{folder.Name}", safe);
                }
                List<FileInfo> fileList = GetFiles(from);
                foreach (FileInfo file in fileList)
                {
                    CopyFile(file.FullName, $@"{to}\{file.Name}", safe);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// <para>Put file in recycle bin</para>
        /// <para>Override</para>
        /// </summary>
        public static void DeleteFile(String path, RecycleOption recycle = RecycleOption.SendToRecycleBin)
        {
            try
            {
                if (File.Exists(path))
                {
                    FileSystem.DeleteFile(path, UIOption.OnlyErrorDialogs, recycle);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }


        //public static void DeleteDirectory(String path)
        //{
        //    List<DirectoryInfo> folderList = GetFolders(path);
        //    foreach (DirectoryInfo folder in folderList)
        //    {
        //        DeleteDirectory(folder.FullName);
        //        Directory.Delete(folder.FullName);
        //    }
        //    List<FileInfo> fileList = GetFiles(path);
        //    foreach (FileInfo file in fileList)
        //    {
        //        File.Delete(file.FullName);
        //    }
        //}


        /// <summary>
        /// <para>Delete everything in the directory</para>
        /// <para>Level All</para>
        /// </summary>
        public static void DeleteDirectory(String path, RecycleOption recycle = RecycleOption.SendToRecycleBin)
        {
            try
            {
                FileSystem.DeleteDirectory(path, UIOption.OnlyErrorDialogs, recycle);
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }

        }
        #endregion

        #region Move

        /// <summary>
        /// <para>Move file</para>
        /// <para>Override</para>
        /// </summary>
        public static void MoveFile(String from, String to, SystemSafe safe=SystemSafe.Override)
        {
            try
            {
                if (File.Exists(from))
                {
                    to = FileSafeAction(to, safe);
                    if (!String.IsNullOrEmpty(to))
                        File.Move(from, to);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }


        /// <summary>
        /// <para>Move everything in the directory</para>
        /// <para>Level All</para>
        /// </summary>
        //public static void MoveDirectory(String from, String to, FileSafe safe)
        //{
        //    try
        //    {
        //        Directory.Move(from, to);
        //    }
        //    catch(Exception e)
        //    {
        //        ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
        //    }
        //}

        public static void MoveDirectory(String from, String to, SystemSafe safe=SystemSafe.Override)
        {
            try
            {
                Directory.CreateDirectory(to);
                List<DirectoryInfo> folderList = GetFolders(from);
                foreach (DirectoryInfo folder in folderList)
                {
                    MoveDirectory(folder.FullName, $@"{to}\{folder.Name}", safe);
                    DeleteDirectory(folder.FullName);
                }
                List<FileInfo> fileList = GetFiles(from);
                foreach (FileInfo file in fileList)
                {
                    MoveFile(file.FullName, $@"{to}\{file.Name}", safe);
                }
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region Encrypt
        /// <summary>
        /// <para>Write Encrypt String to file</para>
        /// </summary>
        public static void WriteEncryptText(String path, String content, int password)
        {
            Char[] charArray = content.ToCharArray();
            for (int a = 0; a < charArray.Length; a++)
            {
                charArray[a] += (Char)(Math.Abs(password));
            }
            content = new string(charArray);
            File.WriteAllText(path, content);
        }


        /// <summary>
        /// <para>Write Encrypt List to file</para>
        /// </summary>
        public static void WriteEncryptLine(String path, List<String> contentList, int password)
        {
            String content = String.Join("", contentList);
            Char[] charArray = content.ToCharArray();
            for (int a = 0; a < charArray.Length; a++)
            {
                charArray[a] += (Char)(Math.Abs(password));
            }
            content = new string(charArray);
            File.WriteAllText(path, content);
        }


        /// <summary>
        /// <para>Write Encrypt Array to file</para>
        /// </summary>
        public static void WriteEncryptLine(String path, String[] contentArray, int password)
        {
            String content = String.Join("\n", contentArray);
            Char[] charArray = content.ToCharArray();
            for (int a = 0; a < charArray.Length; a++)
            {
                charArray[a] += (Char)(Math.Abs(password));
            }
            content = new string(charArray);
            File.WriteAllText(path, content);
        }
        #endregion

        #region Decrypt
        /// <summary>
        /// <para>Read Decrypt String from file</para>
        /// </summary>
        public static String ReadDecryptText(String path, int password)
        {
            String content = File.ReadAllText(path);
            Char[] charArray = content.ToCharArray();
            for (int a = 0; a < charArray.Length; a++)
            {
                charArray[a] -= (Char)(Math.Abs(password));
            }
            content = new string(charArray);
            return content;
        }


        /// <summary>
        /// <para>Read Decrypt Array from file</para>
        /// </summary>
        public static String[] ReadDecryptLine(String path, int password)
        {
            String content = File.ReadAllText(path);
            Char[] charArray = content.ToCharArray();
            for (int a = 0; a < charArray.Length; a++)
            {
                charArray[a] -= (Char)(Math.Abs(password));
            }
            content = new string(charArray).Replace("\r\n", "\n");
            return content.Split('\n');
        }

        #endregion

        #region Safe
        private static string FileSafeAction(String to, SystemSafe safe=SystemSafe.Override)
        {
            try
            {
                if (File.Exists(to))
                {
                    String toName, toExt;
                    int Counter = 1;
                    switch (safe)
                    {
                        case SystemSafe.KeepBoth:
                            toName = Path.GetFileNameWithoutExtension(to);
                            Boolean LastisBracket = toName.Substring(toName.Length - 1).Equals(")");
                            Boolean ContainRepaeat = int.TryParse(toName.Substring(toName.LastIndexOf('(')).Replace("(", "").Replace(")", ""), out int count);
                            if (LastisBracket)
                            {
                                if (ContainRepaeat)
                                {
                                    toName = toName.Substring(0, toName.LastIndexOf('('));
                                }
                            }
                            toExt = Path.GetExtension(to);
                            while (File.Exists(to))
                            {
                                to = $"{toName}({Counter++}){toExt}";
                            }
                            break;

                        case SystemSafe.Override:
                            DeleteFile(to);
                            break;

                        case SystemSafe.Skip:
                            to = null;
                            break;
                    }
                }
                return to;
            }
            catch (Exception e)
            {
                ErrorLog.Write(e, MethodBase.GetCurrentMethod().Name);
                return to;
            }
        }
        #endregion


    }
}
