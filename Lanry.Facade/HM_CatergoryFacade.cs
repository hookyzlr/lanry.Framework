using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.Model;
using Lanry.Repository;
using Lanry.Utility;

namespace Lanry.Facade
{
    public static class HM_CatergoryFacade
    {
        public static DataList<HM_Category> GetCatergory()
        {
            return CatergoryRepository.GetCatergory();
        }
    }
}
