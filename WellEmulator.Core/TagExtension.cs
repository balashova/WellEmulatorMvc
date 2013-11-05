using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public static class TagExtension
    {
        public static void NextValue(this Tag tag, Random random)
        {
            var newValue = tag.Value + random.NextDouble() * tag.Delta - tag.Delta / 2;
            tag.Value = newValue > tag.MaxValue ? tag.MaxValue : newValue < tag.MinValue ? tag.MinValue : newValue;
        }
    }
}
