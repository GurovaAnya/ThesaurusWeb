using DefinitionExtractionWeb.Models;
using DefinitionExtractionWeb.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace DefinitionExtractionWeb.Queries
{
    public class DEQueries
    {
        //DEDatabaseEntities db = new DEDatabaseEntities();
        public List<Definition> GetDefinitions (DateTime beg, DateTime end, int relID, int relDescID)
        {
            
            List<Definition> def;
            using (DEDatabaseEntities db = new DEDatabaseEntities())
            {
                def = db.Definitions
                    .Where
                    (definition => definition.Insert_date >= beg
                    && definition.Insert_date <= end
                    && (definition.Descriptor.Relations.Where(r => r.Descriptor1.ID == relDescID).Select(r => r.Relation_types).Where(r => r.ID == relID).Count() > 0
                    || definition.Descriptor.Relations1.Where(r => r.Descriptor.ID == relDescID).Select(r => r.Relation_types).Where(r => r.ID == relID).Count() > 0)
                    )
                    .ToList();
                return def;
            }
        }

        //public ChartViewModel GetChartForUsers (DateTime beg, DateTime end)
        //{
        //    var data = db.Definitions.Where(def => def.Insert_date >= beg && def.Insert_date <= end)
        //        .Select(def => new StatsViewModel(){ User = def.User, Definition = def }).ToList();
        //    var info = db.Users.Select(user => new UserTableViewModel{ FullName = user.First_name + " " + user.Last_name, 
        //        Email = user.Email, Count = user.Definitions.Count
        //        //(def=>def.Insert_date>=dates[0]&&def.Insert_date<=dates[1]) 
        //    })
        //        .OrderByDescending(user=>user.Count).ToList();
        //    return new ChartViewModel(beg, end, data);
        //}

        //public ChartViewModel GetChartForUsers()
        //{
        //    var data = db.Definitions
        //                   .Select(def => new StatsViewModel() { User = def.User, Definition = def }).ToList();
        //    return new ChartViewModel(data);
        //}

        //public object GetUsersStatistics()
        //{
        //    var def = db.Users.Select(user => new { UserID = user.ID,
        //        UserName = user.First_name + " " + user.Last_name, Count = user.Definitions.Count }).ToList();

        //    return def;
        //}

        public async Task<List<UserTableViewModel>> GetUsersStatistics(DateTime beg, DateTime end)
        {
            using (DEDatabaseEntities db = new DEDatabaseEntities())
            {
                //var list = await db.Users.ToArrayAsync();            
                //    var info = list.Select(user => new UserTableViewModel
                //    {
                //        FullName = user.First_name + " " + user.Last_name,
                //        Email = user.Email,
                //        Count = user.Definitions.Where(def => def.Insert_date >= beg && def.Insert_date <= end).Count()
                //        //(def=>def.Insert_date>=dates[0]&&def.Insert_date<=dates[1]) 
                //    })
                //        .Where(usertable => usertable.Count > 0)
                //        .OrderByDescending(user => user.Count).ToList();
                var experiment = (from table in
                    (from user in db.Users
                     select new UserTableViewModel
                     {
                         FullName = user.First_name + " " + user.Last_name,
                         Email = user.Email,
                         Count = (from def in user.Definitions
                                  where def.Insert_date >= beg && def.Insert_date <= end
                                  select def).Count()
                     })
                                 where table.Count > 0
                                 orderby table.Count descending
                                 select table).ToList();

                    return experiment;
                
            }
            //var info = db.Definitions.Where(def => def.Insert_date >= beg && def.Insert_date <= end)
            //    .GroupBy(def =>def.User_ID).Select(group=>new UserTableViewModel { FullName = group.Key.})
            }

    }
}