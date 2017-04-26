using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Code
{
    /// <summary>  
    /// 图片识别，验证识别  
    /// </summary>  
    public class Identify
    {
        /// <summary>  
        /// 功能描述：静态成员 识别数字、字母，验证码等图片上的内容  
        /// </summary>  
        /// <param name="img_path">待识别内容的图片</param>  
        /// <param name="save_dir">识别内容保存目录</param>  
        /// <param name="scale">放大处理比率（建议值范围：100~1000）-（参数：提高识别成功率）</param>  
        /// <returns></returns>  
        public static string StartIdentifyingCaptcha(string img_path, string save_dir, int scale)
        {
            return StartIdentifyingCaptcha(img_path, save_dir, 4, 0, scale);
        }
        /// <summary>  
        /// 功能描述：静态成员 识别数字、字母，验证码等图片上的内容  
        /// </summary>  
        /// <param name="img_path">待识别内容的图片</param>  
        /// <param name="save_dir">识别内容保存目录</param>  
        /// <param name="content_type">内容分类（0：纯数字 1：纯字母 2：数字与字母混合 3：纯汉字，4、中英文混合）-（参数：提高识别成功率）</param>  
        /// <param name="content_length">内容长度（参数：提高识别成功率）</param>  
        /// <param name="scale">放大处理比率（建议值范围：100~1000）-（参数：提高识别成功率）</param>  
        /// <returns></returns>  
        public static string StartIdentifyingCaptcha(string img_path, string save_dir, byte content_type, int content_length, int scale)
        {
            string captcha = "";            // 识别的验证码  

            //int captcha_length = 6;         // 验证码长度(参数：提高识别成功率)  

            //int scale = 730;                // 放大处理比率(参数：提高识别成功率)  

            do
            {
                Common.StartProcess("cmd.exe", new string[] {
                      String.Format(@"tesseract.exe {0} {1}result",img_path,save_dir),
                                           "exit"
                 });
                //Common.StartProcess("cmd.exe", new string[] {
                //     String.Format(@"convert.exe  {0} -resize {1}%   {2}captcha.jpg",img_path,scale,save_dir),
                //      String.Format(@"tesseract.exe {0}captcha.jpg {0}result",save_dir),

                //                                                "exit"
                // });
                //识别验证码(一、安装ImageMagick，二、拷备tesseract目录至应用程序下)
                //Common.StartProcess("cmd.exe", new string[] { "cd tesseract" ,  

                //                                              // 输换图片  
                //                                              String.Format(@"convert.exe -compress none -depth 8 -alpha off -scale {0}% -colorspace gray {1} {2}\captcha.tif",scale,img_path,save_dir),  

                //                                              // 识别图片  
                //                                              String.Format(@"tesseract.exe {0}\captcha.tif {0}\result",save_dir),

                //                                              "exit"});

                // 读取识别的验证码  
                StreamReader reader = new StreamReader(String.Format(@"{0}result.txt", save_dir));
                if(reader.ReadLine()!=null)
                captcha = reader.ReadLine().Trim();

                reader.Close();
                var  lines=File.ReadAllLines(String.Format(@"{0}result.txt", save_dir));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lines.Length; i++)
                {
                    sb.Append(lines[i]+"\r\n");
                }
                captcha = sb.ToString();
                // 匹配规则  
                string pattern = "";

                // 纯数字  
                if (content_type == 0)
                {
                    pattern = (content_length > 0) ? "^[0-9]{" + content_length + "}$" : "^[0-9]+$";
                }
                // 纯字母  
                else if (content_type == 1)
                {
                    pattern = (content_length > 0) ? "^[a-zA-Z]{" + content_length + "}$" : "^[a-zA-Z]+$";
                }
                // 数字与字母混合  
                else if (content_type == 2)
                {
                    pattern = (content_length > 0) ? "^[a-zA-Z0-9]{" + content_length + "}$" : "^[a-zA-Z0-9]+$";
                }
                // 纯汉字  
                else if (content_type == 3)
                {
                    pattern = (content_length > 0) ? "^[\u4e00-\u9fa5]{" + content_length + "}$" : "^[\u4e00-\u9fa5]+$";
                }
                // 中英文混合  
                else if(content_type==4)
                {
                    pattern = (content_length > 0) ? "^[A-Za-z0-9\u4e00-\u9fa5]{" + content_length + "}$" : "^[A-Za-z0-9\u4e00-\u9fa5]+$";
                }
                else
                {
                    break;
                }


                // 识别失败，调整放大比率重新识别  
                if (pattern != "" && !Regex.IsMatch(captcha, pattern))
                {
                    captcha = "";

                    scale++;
                }

                // 放大比率为9倍时，仍无法识别，认为识别失败，退出识别  
                if (scale > 900)
                {
                    captcha = "";

                    break;
                }

            } while (captcha == "");

            return captcha;
        }
    }

}
