using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class CsvReporter : IReporter
    {
        private readonly DirectoryInfo _directoryInfo;
        private readonly List<CsvStruct> _csvStructs = new List<CsvStruct>(1000);

        private const char Splitter = '|';

        public CsvReporter(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        public TimeSpan Delay { get; set; }

        public double this[string name]
        {
            set
            {
                lock (_csvStructs)
                {
                    _csvStructs.Add(new CsvStruct
                    {
                        Value = value,
                        Name = name,
                        TimeStamp = DateTime.Now
                    });
                }
            }
        }

        private struct CsvStruct
        {
            internal double Value { get; set; }
            internal DateTime TimeStamp { get; set; }
            internal string Name { get; set; }
        }

        public void Save()
        {
            var file = new FileInfo(string.Format(@"{0}\{1}.csv",
                                                  _directoryInfo.FullName,
                                                  DateTime.Now.ToString("yyyy.MM.dd_hh.mm.ss.fff")));

            var textWriter = new StreamWriter(file.OpenWrite(), Encoding.Unicode);
            textWriter.WriteLine(textWriter.Encoding.EncodingName.ToUpper());
            textWriter.WriteLine(Splitter);
            textWriter.WriteLine("{1}{0}1{0}Server Local{0}10{0}2", Splitter, "Andrey Cherkashin");

            lock (_csvStructs)
            {
                foreach (var tag in _csvStructs)
                {
                    var time = tag.TimeStamp.Subtract(Delay);
                    textWriter.WriteLine("{1}{0}0{0}{2}{0}{3}{0}1{0}{4}{0}192",
                                         Splitter,
                                         tag.Name,
                                         time.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture),
                                         time.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture),
                                         tag.Value.ToString("F1", CultureInfo.InvariantCulture));
                }
                _csvStructs.Clear();
            }
            textWriter.Close();
        }
    }
}
