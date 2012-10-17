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
        public static DataList<TB1> GetTestData()
        {
            return CatergoryRepository.GetTestData();
        }

        public static int Save(TB1 en)
        {
            return CatergoryRepository.Save(en);
        }
    }
}
