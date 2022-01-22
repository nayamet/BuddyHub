using AutoMapper;
using BOL;
using BOL.Dto;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SaveService
    {
        public static void Add(SaveDto save)
        {
            DataAccessFactory.SaveDataAccess().Add(Mapper.Map<Save>(save));
        }

        public static IEnumerable<SaveDto> GetSaveByUserId(int id)
        {
            var saves = DataAccessFactory.SaveDataAccess().Get().Select(Mapper.Map<Save, SaveDto>);
            if (saves == null) return null;
            var data = (from s in saves
                        where s.FK_Users_Id == id
                        select s);
            return data;
        }

        public static bool DeleteSave(int id)
        {
            var save = DataAccessFactory.SaveDataAccess().Get(id);

            if (save == null) { return false; }
            else
            {
                DataAccessFactory.SaveDataAccess().Delete(id);
                return true;
            }
        }
    }
}
