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
    public class SysUserRepository : ISysUserRepository, IDisposable
    {

        public IQueryable<SysUser> GetList(AppsDBEntities db)
        {
            IQueryable<SysUser> list = db.SysUser.AsQueryable();
            return list;
        }

        public int Create(SysUser entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysUser.Add(entity);
                return db.SaveChanges();
            }
        }

        public int Delete(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysUser entity = db.SysUser.SingleOrDefault(a => a.Id == id);
                if (entity != null)
                {

                    db.SysUser.Remove(entity);
                }
                return db.SaveChanges();
            }
        }

        public void Delete(AppsDBEntities db, string[] deleteCollection)
        {
            IQueryable<SysUser> collection = from f in db.SysUser
                                             where deleteCollection.Contains(f.Id)
                                             select f;
            foreach (var deleteItem in collection)
            {
                db.SysUser.Remove(deleteItem);
            }
        }

        public int Edit(SysUser entity)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.SysUser.Attach(entity);
                //db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                db.Entry<SysUser>(entity).State = EntityState.Modified;
                return db.SaveChanges();
            }
        }

        public SysUser GetById(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                return db.SysUser.SingleOrDefault(a => a.Id == id);
            }
        }

        public bool IsExist(string id)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                SysUser entity = GetById(id);
                if (entity != null)
                    return true;
                return false;
            }
        }
        public void Dispose()
        {

        }

        public IQueryable<P_Sys_GetRoleByUserId_Result> GetRoleByUserId(AppsDBEntities db, string userId)
        {
            return db.P_Sys_GetRoleByUserId(userId).AsQueryable();
        }

        public void UpdateSysRoleSysUser(string userId, string[] roleIds)
        {
            using (AppsDBEntities db = new AppsDBEntities())
            {
                db.P_Sys_DeleteSysRoleSysUserByUserId(userId);
                foreach (string roleid in roleIds)
                {
                    if (!string.IsNullOrWhiteSpace(roleid))
                    {
                        db.P_Sys_UpdateSysRoleSysUser(roleid, userId);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
