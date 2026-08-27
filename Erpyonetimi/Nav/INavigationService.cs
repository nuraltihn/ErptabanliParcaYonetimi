
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Data.Helpers;


namespace Erpyonetimi.Nav

{
    public interface INavigationService
    {
            object CurrentView { get; }
            event EventHandler CurrentViewChanged;
            void Navigate (Pages page);
           
      

    }
}
