using BasicCommon;
using BasicData;
using BasicFramework.Component;
using BasicUPMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using BasicUPMS.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThirdApi.qcloud.api;
using ThirdApi.qiniu.api;

namespace BasicUPMS.Controllers
{
    public class MaterialController : CURDTemplate<MuKeMaterial>
    {
        public ActionResult Index(string viewName, MaterialTypes materialType)
        {
            ViewBag.QCloudSecretId = UPMSConfig.QCloudSecretId;
            ViewBag.transcodeNotifyUrl = "http://" + Request.Url.Authority + "/Material/QCloudTransCodeCallBack";

            if (string.IsNullOrWhiteSpace(viewName))
            {
                switch (materialType)
                {
                    case MaterialTypes.Images: viewName = "Images"; break;
                    case MaterialTypes.Audio:
                    //case MaterialTypes.Video:
                    case MaterialTypes.OtherFile: viewName = "NomalFile"; break;
                    case MaterialTypes.Video:
                        viewName = "QCloudUpload";
                        break;
                    default: viewName = "NomalFile"; break;
                }

            }
            //类型
            var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                               where tax.Status == MuKeEnabledStatus.Enabled
                               && tax.TaxonomyType == (TaxonomyTypes)materialType
                               select new
                               {
                                   id = tax.Id,
                                   pid = tax.ParentId,
                                   text = tax.Name
                               };

            ViewBag.taxonomyTypes = taxonomyType.ToList();
            return View(viewName, materialType);
        }

        /// <summary>
        ///  列表
        /// </summary>
        /// <param name="viewName">为了提高可用性，支持自定义视图名称</param>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult List(string viewName, SearchMaterialRequester request)
        {

            ViewBag.QCloudSecretId = UPMSConfig.QCloudSecretId;
            ViewBag.transcodeNotifyUrl = "http://" + Request.Url.Authority + "/Material/QCloudTransCodeCallBack";
            //如果是Ajax请求
            if (Request.IsAjaxRequest())
            {
                //素材管理
                if (string.IsNullOrWhiteSpace(viewName))
                {
                    switch (request.MaterialType)
                    {
                        case MaterialTypes.Images: viewName = "_ImageItems"; break;
                        case MaterialTypes.Audio:
                        //case MaterialTypes.Video:
                        case MaterialTypes.OtherFile: viewName = "_NomalFileItems"; break;
                        case MaterialTypes.Video: viewName = "_QCloudUploadItems"; break;
                        default: viewName = "_NomalFileItems"; break;
                    }
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(viewName))
                {
                    switch (request.MaterialType)
                    {
                        case MaterialTypes.Images: viewName = "Images"; break;
                        case MaterialTypes.Audio:
                        //case MaterialTypes.Video:
                        case MaterialTypes.OtherFile: viewName = "NomalFile"; break;
                        case MaterialTypes.Video: viewName = "QCloudUpload"; break;
                        default: viewName = "NomalFile"; break;
                    }

                }
                //类型
                var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                                   where tax.Status == MuKeEnabledStatus.Enabled
                                   && tax.TaxonomyType == (TaxonomyTypes)request.MaterialType
                                   select new
                                   {
                                       id = tax.Id,
                                       pid = tax.ParentId,
                                       text = tax.Name
                                   };

                ViewBag.taxonomyTypes = taxonomyType.ToList();
            }


            IQueryable<MuKeMaterial> queryable;

            //显示全部分类
            if (request.TaxonomyId == 0)
            {
                queryable = (from t1 in SessionContext.Repository.MuKeMaterials
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             where t1.Id == t2.ObjectId && t2.Taxonomy.TaxonomyType == (TaxonomyTypes)request.MaterialType
                             orderby t1.Id descending
                             select t1);
            }
            else
            {
                queryable = (from t1 in SessionContext.Repository.MuKeMaterials
                             from t2 in SessionContext.Repository.TaxonomyRelationships
                             where t1.Id == t2.ObjectId && t2.TaxonomyId == request.TaxonomyId
                             orderby t1.Id descending
                             select t1);

            }
            // 查询名称是否为空
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                queryable = queryable.Where(item => item.Name.Contains(request.Name));
            }
            if (request.MaterialType == MaterialTypes.Video)
            {
                queryable = queryable.Where(item => item.transcodedone==true);
            }
            // 查询结果分页
            var data = queryable.ToPagedList(request.PageIndex, request.PageSize);
            if (request.MaterialType == MaterialTypes.Images)
            {
                foreach (var d in data)
                {
                    if (!string.IsNullOrWhiteSpace(d.LinkUrl))
                    {
                        d.RealLinkUrl = QiNiuApi.GetQiNiuUrlByKey(d.LinkUrl);
                    }
                }
            }
            

            //全部返回的话有问题，那个页面用到了 那个页面上增加七牛的信息，不统一添加
            //            if (viewName == "SelectFilesNew" && request.MaterialType == MaterialTypes.Images)
            //            {
            //            }
            //            else
            //            {
            //                foreach (MuKeMaterial material in data)
            //                {
            //                    material.LinkUrl = QiNiuApi.GetQiNiuUrlByKey(material.LinkUrl);
            //                }
            //            }


            if (Request.QueryString["loadTaxonomys"] == "true")
            {
                var taxonomyType = from tax in SessionContext.Repository.Taxonomys
                                   where tax.Status == MuKeEnabledStatus.Enabled
                                   && tax.TaxonomyType == (TaxonomyTypes)request.MaterialType
                                   select new
                                   {
                                       id = tax.Id,
                                       pid = tax.ParentId,
                                       text = tax.Name
                                   };

                ViewBag.taxonomyTypes = taxonomyType.ToList();

            }
            ViewBag.MaterialType = request.MaterialType;
            return View(viewName, data);
        }

        /// <summary>
        /// 文件、图片上传
        /// </summary>
        /// <param name="taxonomy"></param>
        /// <returns></returns>
        public ActionResult FileUpload(long taxonomy)
        {
            try
            {
                var forkey = taxonomy + "/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                //保存到磁盘
                var relativeAddress = "/" + UPMSConfig.FilePhysicalPath + "" + forkey;
                var savePath = Server.MapPath(relativeAddress);
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                var file = Request.Files[0];
                var fileExt = Path.GetExtension(file.FileName).ToLower();
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExt;
                var filePath = savePath + newFileName;
                file.SaveAs(filePath);

                //拼接七牛key
                var qiniukey = QiNiuApi.Pre_Taxonomy + "/" + forkey + newFileName;

                var ret = QiNiuApi.UpLoadMinFile(UPMSConfig.QiNiuBucket, qiniukey, filePath);

                if (ret == null)
                {
                    SessionContext.Logger.Error("上传错误，ret 返回null");
                    return Content("上传过程中出现错误");
                }
                if (!ret.OK)
                {
                    SessionContext.Logger.Error(ret.Exception.Message);
                    return Content(ret.Exception.Message);
                }


                //保存到数据库
                long createUser = long.Parse(((FormsIdentity)SessionContext.Principal.Identity).Label);
                var newMaterial = SessionContext.Repository.MuKeMaterials.Add(new MuKeMaterial
                {
                    CreateUser = createUser,
                    //                    LinkUrl = "http://"+UPMSConfig.QiNiuDomainName+"/"+ retmes,
                    LinkUrl = ret.key,
                    //LinkUrl = DateTime.Now.ToString("yyyyMMdd") + "/" + newFileName,
                    MimeType = fileExt,
                    Name = file.FileName
                });
                SaveChanges();
                SessionContext.Repository.TaxonomyRelationships.Add(new TaxonomyRelationships
                {
                    CreateUser = createUser,
                    ObjectId = newMaterial.Id,
                    TaxonomyId = taxonomy
                });
                SaveChanges();

                //删除服务器上的文件
                //System.IO.File.Delete(filePath);
                //Directory.Delete(savePath);
                
                return Json(new { jsonrpc = 2.0, result = "成功了", id = 12 });
            }
            catch (Exception ex)
            {
                SessionContext.Logger.Error(ex.ToString());
                return Content("error");
            }
        }

        /// <summary>
        /// 文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="downloadfilename">下载文件名</param>
        /// <returns></returns>
        public ActionResult DownFile(string fileName, string downloadfilename)
        {
            //string file = "~/" + UPMSConfig.FilePhysicalPath + fileName;
            //            string file =fileName;
            //            // 文件物理路径获取
            //            string absoluFilePath = Server.MapPath(file);
            //            var name = Path.GetFileName(absoluFilePath);
            //            // 是否存在该文件
            //            if (System.IO.File.Exists(absoluFilePath))
            //            {
            //                return File(absoluFilePath, "application/zip-x-compressed", name);
            //            }
            //            else
            //            {
            //                return Content("文件不存在！");
            //            }

            return File(fileName, "application/zip-x-compressed", downloadfilename);
        }


        //        public ActionResult QCloudUpload()
        //        {
        //            return View();
        //        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QCloudTransCodeCallBack()
        {
            try
            {
                StreamReader sr = new StreamReader(Request.InputStream);
                var backStr = sr.ReadToEnd();
                //SessionContext.Logger.Debug("backStr-----" + backStr);

                JObject strObj = JObject.Parse(backStr);
                var talk = strObj.GetValue("task").ToString();//file_upload 上传完毕  transcode：转码完毕
                var status = strObj.GetValue("status").ToString();//0正确 
                var message = strObj.GetValue("message").ToString();
                if (status != "0")
                {
                    SessionContext.Logger.Error("腾讯转码回调错误-----status=" + status + " 错误信息:" + message);
                }
                JObject data = JObject.Parse(strObj.GetValue("data").ToString());

                var file_id = data.GetValue("file_id").ToString();
                JObject image_video = JObject.Parse(data.GetValue("image_video").ToString());
               

                JArray videoUrls = JArray.Parse(image_video.GetValue("videoUrls").ToString());
                string videoUrl = "";
                long duration = 0;
                int vbitrate = 0;
                int vheight = 0;
                int vwidth = 0;
                duration = Convert.ToInt64(image_video.GetValue("duration"));

                var firstDefault = videoUrls.FirstOrDefault(x => JObject.Parse(x.ToString()).GetValue("definition").ToString() == "210");  //210 m3u8
                if (firstDefault == null)
                {
                    firstDefault = videoUrls.FirstOrDefault();
                }

                var originalVideo= videoUrls.FirstOrDefault(x => JObject.Parse(x.ToString()).GetValue("definition").ToString() == "0");
                var originalVideoUrl = JObject.Parse(originalVideo.ToString()).GetValue("url").ToString();
                //大小只能用原始文件大小，不能用转码之后的
               

                var firstDefaultObj = JObject.Parse(firstDefault.ToString());
                videoUrl = firstDefaultObj.GetValue("url").ToString();
                
                vbitrate = Convert.ToInt32(firstDefaultObj.GetValue("vbitrate"));
                vheight = Convert.ToInt32(firstDefaultObj.GetValue("vheight"));
                vwidth = Convert.ToInt32(firstDefaultObj.GetValue("vwidth"));

                var video = SessionContext.Repository.MuKeMaterials.FirstOrDefault(x => x.QCloudServerFileId == file_id);
                if (video == null)
                {
                    return new EmptyResult();
                }
                SessionContext.Repository.MuKeMaterials.Attach(video);
                video.LinkUrl = videoUrl;
                video.OriginalVideoUrl = originalVideoUrl;
                video.vbitrate = vbitrate;
                video.vheight = vheight;
                video.vwidth = vwidth;
                video.videoduration = duration;
                if (talk == "transcode")
                {
                    video.transcodedone = true;
                }
                SessionContext.Repository.SaveChanges();
            }
            catch (Exception ex)
            {
                SessionContext.Logger.Error("腾讯回调错误："+ex.ToString());
            }
            

            return new EmptyResult();
        }

        public ActionResult UploadSaveQCloud(string fileName, string serverFileId, long taxonomy)
        {
            try
            {

                string videoUrl = "";
                int vbitrate = 0;
                int vheight = 0;
                int vwidth = 0;

                //不需要保存到磁盘



                #region 不需要转码的方法
                //这里是不需要转码的方法
                //                var res = QCloudApi.GetVideoInfo(serverFileId);
                //                if (res["code"] != "0")
                //                {
                //                    SessionContext.Logger.Error(res["message"]);
                //                    return Json(new { result = "获取信息错误，请查看日志", serverFileId = serverFileId }, JsonRequestBehavior.AllowGet);
                //                }


                //                try
                //                {
                //var playSet = res["message"];
                //默认获原始视频 https://www.qcloud.com/doc/api/257/1285   definition=0 表示原始视频

                //var qCloudPlaySets = JsonConvert.DeserializeObject<List<QCloudPlaySet>>(playSet);

                //var qCloudPlaySet = qCloudPlaySets.Where(x => x.definition == 0).FirstOrDefault();

                //videoUrl = qCloudPlaySet.url;
                //不转码的视频 腾讯返回的 码率 高度 宽度 都是0
                //vbitrate = qCloudPlaySet.vbitrate;
                //vheight = qCloudPlaySet.vheight;
                //vwidth = qCloudPlaySet.vwidth;

                //                }
                //                catch (Exception ex)
                //                {
                //                    SessionContext.Logger.Error(ex.ToString());
                //                    return Json(new { result = ex.Message, serverFileId = serverFileId }, JsonRequestBehavior.AllowGet);
                //                }

                #endregion


                //保存到数据库
                long createUser = long.Parse(((FormsIdentity)SessionContext.Principal.Identity).Label);
                var fileExt = Path.GetExtension(fileName).ToLower();
                var newMaterial = SessionContext.Repository.MuKeMaterials.Add(new MuKeMaterial
                {
                    CreateUser = createUser,
                    MaterialType = MaterialTypes.Video,
                    QCloudServerFileId = serverFileId,
                    LinkUrl = videoUrl,
                    //LinkUrl = DateTime.Now.ToString("yyyyMMdd") + "/" + newFileName,
                    MimeType = fileExt,
                    Name = fileName,
                    vbitrate = vbitrate,
                    vheight = vheight,
                    vwidth = vwidth,
                    transcodedone=false
                });
                SaveChanges();
                SessionContext.Repository.TaxonomyRelationships.Add(new TaxonomyRelationships
                {
                    CreateUser = createUser,
                    ObjectId = newMaterial.Id,
                    TaxonomyId = taxonomy
                });
                SaveChanges();

                return Json(new { result = "ok", serverFileId = serverFileId }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { result = ex.Message, serverFileId = serverFileId }, JsonRequestBehavior.AllowGet);
            }
            //return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult QCloudSignature(string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                args = "";
            }
            var sk = UPMSConfig.QCloudSecretKey;
            var resultstr = Signature(args, sk);
            //var jsonstr = "{\"result\":\"" + result + "\"}";
            return Json(new { result = resultstr }, JsonRequestBehavior.AllowGet);
        }

        private string Signature(string signStr, string secret)
        {
            using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secret)))
            {
                byte[] hash = mac.ComputeHash(Encoding.UTF8.GetBytes(signStr));
                return Convert.ToBase64String(hash);
            }
        }

        [HttpPost]
        public override JsonResult Remove(long id)
        {
            string resMsg = "";
            var result = CheckParameterCallback<long>(() =>
            {
                var entity = SessionContext.Repository.Set<MuKeMaterial>().Find(id);
                if (entity != null)
                {
                        SessionContext.Repository.Set<MuKeMaterial>().Remove(entity);
                        return 0;
                }
                return -1;
            }, id);
            if (result.IsSuccess && result.Data == -1)
            {
                result.IsSuccess = false;
                result.Message = ErrorMessage.ArgumentError;
            }
            if (result.IsSuccess && result.Data == -2)
            {
                result.IsSuccess = false;
                result.Message = resMsg;
            }
            return Json(result);
        }
    }
}
