using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartClinic.DatabaseHelper;

namespace SmartClinic
{
    
    public static class SelectedItems
    {
        public static List<Medicine> SelectedMedicines { get; } = new List<Medicine>();
        public static List<Advice> SelectedAdvices { get; } = new List<Advice>();
    }
}
