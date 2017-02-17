using Apps.IDAL;
using Apps.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.DAL
{
    public class SysRoleRepository : IDisposable, ISysRoleRepository
    {
        public IQueryable<SysRole> GetList(AppsDBEntities db)
        {
            IQueryable<SysRole> list = db.SysRole.AsQueryable();
            return list;
        }

        public int Create(SysRole entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysRole.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysRole entity = db.SysRole.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.SysRole.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public int Edit(SysRole entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysRole.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<SysRole>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public SysRole GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysRole.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysRole entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }

        public void Dispose()
        {

        }

        public IQueryable<SysUser> GetRefSysUser(AppsDBEntities db, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return from m in db.SysRole
                       from f in m.SysUser
                       where m.Id == id
                       select f;
            }
            return null;
        }

        public IQueryable<P_Sys_GetUserByRoleId_Result> GetUserByRoleId(AppsDBEntities db, string roleId)
        {
            return db.P_Sys_GetUserByRoleId(roleId).AsQueryable();
        }

        public void UpdateSysRoleSysUser(string roleId, string[] userIds)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.P_Sys_DeleteSysRoleSysUserByRoleId(roleId);
                foreach (string userid in userIds)
                {
                    if (!string.IsNullOrWhiteSpace(userid))
                    {
                        db.P_Sys_UpdateSysRoleSysUser(roleId, userid);
                    }
                }
                db.SaveChanges();
            }
        }


    }
}
