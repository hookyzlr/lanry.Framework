﻿using System;
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
        /// <summary>
        /// 获取测试数据
        /// </summary>
        /// <returns></returns>
        public static DataList<TB1> GetTestData()
        {
            return CatergoryRepository.GetTestData();
        }

        public static int Save(TB1 en)
        {
            return CatergoryRepository.Save(en);
        }
        
        public static int Update(TB1 en)
        {
            return CatergoryRepository.Update(en);
        }
    }
}
