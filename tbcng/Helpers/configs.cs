using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using tbcng.Models;
namespace tbcng.Helpers
{
    public static class configs
    {
        private static thietbicnEntities db = new thietbicnEntities();
        public static bool Sendmail(string from, string pass, string to, string topic, string content)
        {
            try
            {
                var fromAddress = from;
                var toAddress = to;
                //Password of your gmail address
                string fromPassword = pass;
                // Passing the values and make a email formate to display
                string subject = topic;
                string body = content;
                // smtp settings
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromAddress);
                message.To.Add(toAddress);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com";//"smtp.gmail.com";
                    smtp.Port = 587;// 465;//587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                    smtp.Timeout = 20000;
                }
                // Passing values to smtp object
                smtp.Send(message);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static string getAllMenu()
        {
            try
            {
                string rs = "<ul class=\"dropdown-menu\">";
                var p = (from q in db.cats where q.cat_parent_id == null select q).OrderBy(o => o.cat_pos).ToList();
                for (int i = 0; i < p.Count; i++)
                {
                    rs += "<li >";
                    rs += getAllChildMenu(p[i].cat_name,p[i].cat_id,1);
                    rs += "</li>";
                }
                rs += "</ul>";
                return rs;
            }
            catch
            {
                return "";
            }
        }
        public static string getAllChildMenu(string name,int id,int l)
        {
            try
            {
                string space="";
                for (int j = 0; j <= l*4; j++)
                {
                    space += "&nbsp";
                }
                string rs = "";// "<a href=\"#\">" + name + " </a>";
                string temp="<i class=\"fa fa-angle-down\"></i>";//"class=\"dropdown-submenu\"";
                string temp2 = "";
                string display = "display:block;";
                var p2 = (from q2 in db.cats where q2.cat_parent_id == id select q2).OrderBy(o => o.cat_pos).ToList();
                for (int ii = 0; ii < p2.Count; ii++)
                {
                    temp2=getAllChildMenu(p2[ii].cat_name,p2[ii].cat_id, l+1);
                    if (l > 1) display = "display:none;";
                    if (temp2 != "")
                    {
                        //rs += "<li class=\"dropdown-submenu\"><a class=\"test\" tabindex=\"-1\" href=\"#\">" + space + p2[ii].cat_name + "<span class=\"caret\"></span></a>";
                        //rs += "<ul class=\"dropdown-menu\">" + temp2 + "</li></ul>";
                        rs += "<li id=\"" + p2[ii].cat_id + "\" pid=\"" + p2[ii].cat_parent_id + "\"  style=\"" + display + "\" onclick=\"viewtree(" + p2[ii].cat_id + ");\"><a class=\"test\" tabindex=\"-1\" href=\"#\">" + space + p2[ii].cat_name + temp + "</a></li>";//<span class=\"caret\"  style='float:right;'></span>
                        rs += temp2;
                    }
                    else 
                    {
                        //rs += "<li ><a class=\"test\" tabindex=\"-1\" href=\"#\">" + space + p2[ii].cat_name + "<span class=\"caret\"></span></a></li>";
                        //rs += temp2;
                        rs += "<li id=\"" + p2[ii].cat_id + "\" pid=\"" + p2[ii].cat_parent_id + "\" style=\"" + display + "\"><a class=\"test\" tabindex=\"-1\" href=\"/san-pham/" + unicodeToNoMark(p2[ii].cat_name) + "-" + p2[ii].cat_id + "/all-0-0-1-1\">" + space + p2[ii].cat_name + "</a></li>";
                        rs += temp2;
                    }
                    
                }
                return rs;
            }
            catch
            {
                return "";
            }
        }
        public static string getAllMenu2()
        {
            try
            {
                string rs = "";
                var p = (from q in db.cats where q.cat_parent_id == null select q).OrderBy(o => o.cat_pos).ToList();
                for (int i = 0; i < p.Count; i++)
                {
                    //rs += "<li >";
                    rs += getAllChildMenu2(p[i].cat_name, p[i].cat_id, 1);
                    //rs += "</li>";
                }
                
                return rs;
            }
            catch
            {
                return "";
            }
        }
        public static string getAllChildMenu2(string name, int id, int l)
        {
            try
            {
                string space = "";
                for (int j = 0; j <= l * 4; j++)
                {
                    space += "&nbsp";
                }
                string rs = "";// "<a href=\"#\">" + name + " </a>";
                string temp = "";// "<i class=\"fa fa-angle-down\"></i>";//"class=\"dropdown-submenu\"";
                string temp2 = "";
                string display = "display:block;";
                var p2 = (from q2 in db.cats where q2.cat_parent_id == id select q2).OrderBy(o => o.cat_pos).ToList();
                for (int ii = 0; ii < p2.Count; ii++)
                {
                    temp2 = getAllChildMenu2(p2[ii].cat_name, p2[ii].cat_id, l + 1);
                    //if (l > 1) display = "display:none;";
                    if (temp2 != "")
                    {

                        rs += "<li id=\"" + p2[ii].cat_id + "\" pid=\"" + p2[ii].cat_parent_id + "\"  style=\"" + display + "\"><a class=\"test\" tabindex=\"-1\" href=\"/san-pham/" + unicodeToNoMark(p2[ii].cat_name) + "-" + p2[ii].cat_id + "/all-0-0-1-1\">" + space + p2[ii].cat_name + temp + "</a><span class=\"caret\" onclick=\"viewtree2(" + p2[ii].cat_id + ");\"></span></li>";
                        rs += temp2;
                    }
                    else
                    {
                        //rs += "<li ><a class=\"test\" tabindex=\"-1\" href=\"#\">" + space + p2[ii].cat_name + "<span class=\"caret\"></span></a></li>";
                        //rs += temp2;
                        rs += "<li id=\"" + p2[ii].cat_id + "\" pid=\"" + p2[ii].cat_parent_id + "\" style=\"" + display + "\"><a class=\"test\" tabindex=\"-1\" href=\"/san-pham/" + unicodeToNoMark(p2[ii].cat_name) + "-" + p2[ii].cat_id + "/all-0-0-1-1\">" + space + p2[ii].cat_name + "</a></li>";
                        rs += temp2;
                    }

                }
                return rs;
            }
            catch
            {
                return "";
            }
        }
        public static string getcatname(int? catid)
        {
            try
            {
                cat ct = db.cats.Find(catid);
                string cname=ct.cat_name.ToLowerInvariant();               
                //if (cname.EndsWith("-x")) cname = cname.Replace("-x", "x");
                //if (cname.EndsWith("-i")) cname = cname.Replace("-i", "i");
                //if (cname.EndsWith("-v")) cname = cname.Replace("-v", "v");
                return cname;
            }
            catch
            {
                return "chi-tiet";
            }
        }
        public static string getcatnameurl(int? catid)
        {
            try
            {
                cat ct = db.cats.Find(catid);
                string cname = ct.cat_name.ToLowerInvariant();
                cname = unicodeToNoMark(cname);
                if (cname.EndsWith("-x")) cname = cname.Replace("-x", "x");
                if (cname.EndsWith("-i")) cname = cname.Replace("-i", "i");
                if (cname.EndsWith("-v")) cname = cname.Replace("-v", "v");
                return cname.Replace(" ", "").Replace("-", "");
            }
            catch
            {
                return "chi-tiet";
            }
        }
        public const int ImageMinimumBytes = 512;
        public static bool IsImage(HttpPostedFileBase postedFile)
        {
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (postedFile.ContentType.ToLower() != "image/jpg" &&
                        postedFile.ContentType.ToLower() != "image/jpeg" &&
                        postedFile.ContentType.ToLower() != "image/pjpeg" &&
                        postedFile.ContentType.ToLower() != "image/gif" &&
                        postedFile.ContentType.ToLower() != "image/x-png" &&
                        postedFile.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (System.IO.Path.GetExtension(postedFile.FileName).ToLower() != ".jpg"
                && System.IO.Path.GetExtension(postedFile.FileName).ToLower() != ".png"
                && System.IO.Path.GetExtension(postedFile.FileName).ToLower() != ".gif"
                && System.IO.Path.GetExtension(postedFile.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            //-------------------------------------------
            //  Attempt to read the file and check the first bytes
            //-------------------------------------------
            try
            {
                if (!postedFile.InputStream.CanRead)
                {
                    return false;
                }

                if (postedFile.ContentLength < ImageMinimumBytes)
                {
                    return false;
                }

                byte[] buffer = new byte[512];
                postedFile.InputStream.Read(buffer, 0, 512);
                string content = System.Text.Encoding.UTF8.GetString(buffer);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            //-------------------------------------------
            //  Try to instantiate new Bitmap, if .NET will throw exception
            //  we can assume that it's not a valid image
            //-------------------------------------------

            try
            {
                using (var bitmap = new System.Drawing.Bitmap(postedFile.InputStream))
                {
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static string unicodeToNoMark(string input)
        {
            input = input.ToLowerInvariant().Trim();
            if (input == null) return "";
            string noMark = "a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,e,u,u,u,u,u,u,u,u,u,u,u,u,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,o,i,i,i,i,i,i,y,y,y,y,y,y,d,A,A,E,U,O,O,D";
            string unicode = "a,á,à,ả,ã,ạ,â,ấ,ầ,ẩ,ẫ,ậ,ă,ắ,ằ,ẳ,ẵ,ặ,e,é,è,ẻ,ẽ,ẹ,ê,ế,ề,ể,ễ,ệ,u,ú,ù,ủ,ũ,ụ,ư,ứ,ừ,ử,ữ,ự,o,ó,ò,ỏ,õ,ọ,ơ,ớ,ờ,ở,ỡ,ợ,ô,ố,ồ,ổ,ỗ,ộ,i,í,ì,ỉ,ĩ,ị,y,ý,ỳ,ỷ,ỹ,ỵ,đ,Â,Ă,Ê,Ư,Ơ,Ô,Đ";
            string[] a_n = noMark.Split(',');
            string[] a_u = unicode.Split(',');
            for (int i = 0; i < a_n.Length; i++)
            {
                input = input.Replace(a_u[i], a_n[i]);
            }
            input = input.Replace("  ", " ");
            input = Regex.Replace(input, "[^a-zA-Z0-9% ._]", string.Empty);
            input = removeSpecialChar(input);
            return input;
        }

        public static string removeSpecialChar(string input)
        {
            input = input.Replace("-", "").Replace(":", "").Replace(",", "").Replace("_", "").Replace("'", "").Replace("\"", "").Replace(";", "").Replace("”", "").Replace(".", "").Replace("%", "").Replace(" ", "-").Replace("--", "-");
            return input;
        }

        public static void SaveTolog(string log) {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~/" + "log.txt"), true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + log);
                sw.Close();
            }
        }
        public static void setCookie(string field, string value)
        {
            HttpCookie MyCookie = new HttpCookie(field);
            MyCookie.Value = value;
            MyCookie.Expires = DateTime.Now.AddDays(365);
            HttpContext.Current.Response.Cookies.Add(MyCookie);
            //Response.Cookies.Add(MyCookie);   

        }
        public static string getCookie(string v)
        {
            try
            {
                return HttpContext.Current.Request.Cookies[v].Value.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}