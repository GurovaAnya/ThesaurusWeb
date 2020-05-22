using DefinitionExtractionWeb.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DefinitionExtractionWeb.Queries
{
    public class DEQueries
    {
        DEDatabaseEntities db = new DEDatabaseEntities();
        public List<Definition> GetDefinitions (DateTime beg, DateTime end, int relID, int relDescID)
        {
            
            List<Definition> def;
            def = db.Definitions
                .Where
                (definition => definition.Insert_date >= beg
                && definition.Insert_date <= end
                && (definition.Descriptor.Relations.Where(r => r.Descriptor.ID == relDescID).Select(r=>r.Relation_types).Where(r=>r.ID==relID).Count()>0
                || definition.Descriptor.Relations1.Where(r=>r.Descriptor1.ID==relDescID).Select(r => r.Relation_types).Where(r => r.ID == relID).Count() > 0)
                )
                .ToList();
            return def;
        }
    }
}