using cloudweb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNet.Identity;



namespace cloudweb.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult SlideShow(int slide_id = 0)
        {
            SlideShowViewModel slideshow = new SlideShowViewModel();
            using (var db = new PostEntities())
            {
                var slide = db.SlideShows.Where(x => x.Id == slide_id).FirstOrDefault();
                    slideshow.Duration = slide.Duration;
                slideshow.Effect = slide.Effect;
                string[] sids = slide.PhotoIds.Split(';');
                slideshow.photos = new List<Table>();
                foreach (var s in sids)
                {
                    if (s != "")
                    {
                        int id = Convert.ToInt32(s);
                        slideshow.photos.Add(db.Tables.Where(x => x.Id == id).FirstOrDefault());
                    }
                }
            }
            return View("SlideShow", slideshow);
        }

        public ActionResult ChangeShow(int slide_id = 0)
        {
            SlideShowViewModel slideshow = new SlideShowViewModel();
            using (var db = new PostEntities())
            {
                var slide = db.SlideShows.Where(x => x.Id == slide_id).FirstOrDefault();
                slideshow.Duration = slide.Duration;
                slideshow.Effect = slide.Effect;
                slideshow.Name = slide.SlideName;
                string[] sids = slide.PhotoIds.Split(';');
                slideshow.several_photos = new List<Table>();
                string user_id = User.Identity.GetUserId();
                slideshow.photos = db.Tables.Where(x => x.userid == user_id).ToList();
                foreach (var s in sids)
                {
                    if (s != "")
                    {
                        int id = Convert.ToInt32(s);
                        slideshow.several_photos.Add(db.Tables.Where(x => x.Id == id).FirstOrDefault());
                    }
                }
            }
            ViewBag.State = "change";
            string paths = "";
            foreach (var s in slideshow.several_photos)
            {
                paths += "#" + "http://res.cloudinary.com/" + s.path;
            }
            ViewBag.Paths = paths;
            return View("CreateSlideShow", slideshow);
        }

        public ActionResult ListSlideShow(string user_id = "")
        {
            Session["CurrentUserId"] = User.Identity.GetUserId();
            user_id = User.Identity.GetUserId();

            List<SlideShow> slides;
            using (var db = new PostEntities())
            {
                slides = db.SlideShows.Where(x => x.UserId == user_id).ToList();
            }
            return View("ListSlideShow", slides);
        }

        public ActionResult DeleteShow(int slide_id = 0)
        {
            using (var db = new PostEntities())
            {
                var slide = db.SlideShows.Where(x => x.Id == slide_id).FirstOrDefault();
                db.SlideShows.Remove(slide);
                db.SaveChanges();
            }
            return RedirectToAction("ListSlideShow", new { user_id = User.Identity.GetUserId() });
        }



        public ActionResult CreateSlideShow(string user_id = "")
        {
            Session["CurrentUserId"] = User.Identity.GetUserId();
            user_id = User.Identity.GetUserId();
            SlideShowViewModel slideshow = new SlideShowViewModel();
            using (var db = new PostEntities())
            {
                slideshow.photos = db.Tables.Where(x => x.userid == user_id).ToList();
                foreach (var slide in slideshow.photos)
                    slideshow.booleans.Add(false);
            }
            return View(slideshow);
        }

        [HttpPost]
        public ActionResult CreateSlideShow(SlideShowViewModel slideshow, string inputData)
        {
            string user_id = (string)Session["CurrentUserId"];
            if (ModelState.IsValid)
            {
                slideshow.Duration = Math.Abs(slideshow.Duration);
                using (var db = new PostEntities())
                {
                    slideshow.photos = new List<Table>();
                    IEnumerable<Table> photos = db.Tables.Where(x => x.userid == user_id).ToList();
                    string[] data = inputData.Split('#');
                    foreach (var item in data)
                    {
                        if (item != "")
                        {
                            string data_item = item.Replace("http://res.cloudinary.com/", "");
                            slideshow.photos.Add(photos.Where(x => x.path == data_item).FirstOrDefault());
                        }
                    }
                    string name;
                    if (slideshow.Name != null)
                        name = slideshow.Name;
                    else
                        name = "slideshow" + db.SlideShows.Where(x => x.UserId == user_id).Count().ToString();
                    SlideShow show = new SlideShow()
                    {
                        Duration = slideshow.Duration,
                        SlideName = name,
                        Effect = slideshow.Effect,
                        UserId = user_id,
                        PhotoIds = ""
                    };
                    foreach (var item in slideshow.photos)
                        show.PhotoIds += item.Id + ";";
                    db.SlideShows.Add(show);
                    db.SaveChanges();
                }
                return View("SlideShow", slideshow);
            }
            return RedirectToAction("Wall", new { user_id = user_id });
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RedactPhoto(string id = "")
        {
            Table photo;
            Session["CurrentPhoto"] = id;
            int photoid = Convert.ToInt32(id);
            using (var db = new PostEntities())
            {
                photo = db.Tables.Where(x => x.Id == photoid).FirstOrDefault();
            }

            return View(photo);
        }

        public ActionResult DragImages()
        {
            List<Table> list = new List<Table>();
            using (var db = new PostEntities())
            {
                string id = User.Identity.GetUserId();

                list = db.Tables.Where(x => x.userid == id).ToList();
            }
            return View("PhotoList", list);
        }

        [HttpPost]
        public ActionResult DeletePhoto(string id = "")
        {
            string old_id = (string)Session["CurrentPhoto"];
            return RedirectToAction("DragImages");
        }

        [HttpPost]
        public ActionResult Create(string imageData)
        {
            string old_id = (string)Session["CurrentPhoto"];
            string filename = DateTime.Now.ToString().Replace("/", "-").Replace(" ", "- ").Replace(":", "") + ".png";

            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));

            string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

            // var fileName1 = Path.GetFileName(file.FileName); 

            bool isExists = System.IO.Directory.Exists(pathString);

            if (!isExists)
                System.IO.Directory.CreateDirectory(pathString);

            var path = string.Format("{0}\\{1}", pathString, filename);



            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            Account account = new Account("keipa",
                                          "169521694664552",
                                          "mjcJV3u03EKrObKfsltivxXK_lo");
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(path)
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            var imageuri = uploadResult.Uri;
            var userid = User.Identity.GetUserId();
            Table newimage = new Table();

            newimage.userid = userid;
            newimage.path = uploadResult.Uri.AbsolutePath;
            using (var db = new PostEntities())
            {
                // db.Tables.Add(newimage); 
                db.Tables.Add(newimage);
                db.SaveChanges();
            };

            List<Table> list = new List<Table>();
            using (var db = new PostEntities())
            {
                string id = User.Identity.GetUserId();
                list = db.Tables.Where(x => x.userid == id).ToList();
            }
            return View("PhotoList", list);

        }

        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));
                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");
                        var fileName1 = Path.GetFileName(file.FileName);
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        Account account = new Account("keipa",
                                                      "169521694664552",
                                                      "mjcJV3u03EKrObKfsltivxXK_lo");
                        Cloudinary cloudinary = new Cloudinary(account);
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(path)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);




                        using (var db = new PostEntities())
                        {
                            var userid = User.Identity.GetUserId();
                            Table newimage = new Table();
                            newimage.userid = userid;
                            newimage.path = uploadResult.Uri.AbsolutePath;
                            db.Tables.Add(newimage);
                            db.SaveChanges();
                            //RedirectToAction("DragImages");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
    }
}