using FileManager.WebAPI.Models;
using FileManager.WebAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager.WebAPI.Helpers
{
    public static class DirectoryHelper
    {
        private const int tenMb = 10 * 1024 * 1024;

        public static DirectoryM GetDirectoryModelByPath(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            return Convert(directory);
        }

        private static DirectoryM Convert(DirectoryInfo dirInfo)
        {
            //could take top file from all files with LINQ, but it's relatively quick as is
            IEnumerable<FileInfo> topFilesList = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            IEnumerable<FileInfo> allFilesList = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<DirectoryInfo> subdirectoriesList = dirInfo.GetDirectories();

            IEnumerable<FileM> topFiles = topFilesList.Select(f => Convert(f));
            IEnumerable<FileM> allFiles = allFilesList.Select(f => Convert(f));
            IEnumerable<DirectoryM> subdirectories = subdirectoriesList.Select(sd => Convert(sd));

            var dirM = new DirectoryM()
            {
                Name = dirInfo.Name,
                Path = dirInfo.FullName,
                Files = topFiles,
                Subdirectories = subdirectories,
                CountOf = GetCountOf(allFiles)
            };
            return dirM;
        }

        private static FileM Convert(FileInfo fileInfo)
        {
            var fileM = new FileM()
            {
                Name = fileInfo.Name,
                Size = fileInfo.Length
            };
            return fileM;
        }

        private static Dictionary<FileSizeSpan, int> GetCountOf(IEnumerable<FileM> files)
        {
            const int tenMb = DirectoryHelper.tenMb;

            Dictionary<FileSizeSpan, int> quanOf = DirectoryHelper.IntiFileSizeSpansDict();

            foreach (var file in files)
                if (file.Size < tenMb)
                    ++quanOf[FileSizeSpan.LessTenMb];
                else if (file.Size >= tenMb && file.Size < 5 * tenMb)
                    ++quanOf[FileSizeSpan.TenToFiftyMb];
                else if (file.Size >= 10 * tenMb)
                    ++quanOf[FileSizeSpan.GreaterHundredMb];
                else
                    ++quanOf[FileSizeSpan.Other];

            return quanOf;

            #region leagcy
            ////using LINQ's and PLINQ's GroupBy considered, bt it's slightly slower
            //var groupedSing = allFilesList.GroupBy(f => DirectoryHelper.GetFileSizeSpan(f));
            //var grouped = allFilesList.GroupBy(f => DirectoryHelper.GetFileSizeSpan(f)).AsParallel();
            //foreach (var group in grouped)
            //{
            //    quanOfByGrp[group.Key] = group.Count();
            //}
            #endregion
        }

        #region leagcy
        public static FileSizeSpan GetFileSizeSpan(FileInfo file)
        {
            return file.Length < tenMb ? FileSizeSpan.LessTenMb
                 : tenMb <= file.Length && file.Length < 5 * tenMb ? FileSizeSpan.TenToFiftyMb
                 : 10 * tenMb <= file.Length ? FileSizeSpan.GreaterHundredMb
                 : FileSizeSpan.Other;
        }
        #endregion

        private static Dictionary<FileSizeSpan, int> IntiFileSizeSpansDict()
        {
            var dict = new Dictionary<FileSizeSpan, int>();

            foreach (FileSizeSpan span in Enum.GetValues(typeof(FileSizeSpan)))
                dict.Add(span, 0);
            return dict;
        }
    }
}