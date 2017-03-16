﻿using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using ExtApp.Model;
using ExtApp.BLL;

namespace ExtApp.Controller
{
    /// <summary>
    /// 组织机构控制器
    /// </summary>
    public class DeptController : ApiBase
    {
        /// <summary>
        /// bll
        /// </summary>
        private  DeptBLL bll;

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List()
        {
            var list = bll.List().Select(o => new
            {
                ID = o.ID,
                PID = o.PDept == null ? 0 : o.PDept.ID,
                PName = o.PDept == null ? "" : o.PDept.Name,
                Code = o.Code,
                Name = o.Name,
                TypeID = o.Type == null ? 0 : o.Type.ID,
                TypeCode = o.Type == null ? "" : o.Type.Code,
                TypeName = o.Type == null ? "" : o.Type.Name,
                HeadID = o.Head == null ? 0 : o.Head.ID,
                HeadName = o.Head == null ? "" : o.Head.Name,
                AddUserID = o.AddUser == null ? 0 : o.AddUser.ID,
                AddUserName = o.AddUser == null ? "" : o.AddUser.Name,
                AddTime = o.AddTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Sort = o.Sort,
                Status = o.Status,
                Comment = o.Comment
            }).ToList();

            return Json(list);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add([FromBody]UserAddModel model)
        {
            var dept = new Dept
            {
                ID = 0,
                PDept = new Dept { ID = model.PID },
                Code = "",
                Name = model.Name,
                Type = model.TypeID == null ? null : new DicItem { ID = model.TypeID.Value },
                Head = model.HeadID == null ? null : new User { ID = model.HeadID.Value },
                AddUser = AdminHelper.Admin,
                AddTime = DateTime.Now,
                Sort = model.Sort == null ? 0 : model.Sort.Value,
                Status = model.Status == null ? 1 : model.Status.Value,
                Comment = model.Comment
            };
            return Json(bll.Add(dept));
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(Dept dept)
        {
            return Json(bll.Edit(dept));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            return Json(bll.Delete(id));
        }
    }
}
