using Erpyonetimi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Erpyonetimi.Context;
using Erpyonetimi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Erpyonetimi.Data.Helpers
{
    public class UserSession
    {
        public static Users?CurrentUser { get; set; }

        public static bool IsAdmin =>
            CurrentUser?.RolId == 1;

        public static bool IsDepo =>
            CurrentUser?.RolId == 2;

        public static bool IsSatis=>
            CurrentUser?.RolId == 3;
    }
}
