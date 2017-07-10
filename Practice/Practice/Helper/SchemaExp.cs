using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.Helper
{
    class SchemaExp
    {
        public static void CreateDataBaseSchema()
        {
            var cfg = new Configuration();

            new SchemaExport(cfg).Drop(false, true);
            new SchemaExport(cfg).Create(false, true);
        }
    }
}
