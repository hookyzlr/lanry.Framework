using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.Utility;

namespace Lanry.Model
{
    [Table("HM_Category")]
    public class HM_Category
    {
        [PropertyAttibute(PropertyName="CategoryID",IsIdentity=true,IsPrimaryKey=true)]
        public int CategoryID { set; get; }

        [PropertyAttibute(PropertyName = "CategoryName")]
        public string CategoryName { set; get; }

        [PropertyAttibute(PropertyName = "ParentID")]
        public int ParentID { set; get; }
    }
}
