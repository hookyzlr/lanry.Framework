using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lanry.Utility;

namespace Lanry.Model
{
    [Table("TB1")]
    public class TB1
    {
        [PropertyAttibute(PropertyName="ID",IsIdentity=true,IsPrimaryKey=true)]
        public int ID { set; get; }

        [PropertyAttibute(PropertyName = "Name")]
        public string Name { set; get; }

        [PropertyAttibute(PropertyName = "ParentID")]
        public int ParentID { set; get; }
    }
}
