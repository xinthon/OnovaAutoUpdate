using OnovaAutoUpdate.ViewModels.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnovaAutoUpdate.ViewModels.Framework
{
    public interface IViewModelFactory
    {
        DashboardViewModel CreateDashboardViewModel();
    }
}
