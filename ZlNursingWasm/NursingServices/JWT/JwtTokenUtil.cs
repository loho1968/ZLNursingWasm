using Microsoft.IdentityModel.Tokens;
using NursingModel.TaskModels;
using NursingCommon;using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT;
using JWT.Serializers;
using Newtonsoft.Json.Linq;

namespace NursingServices
{
    /// <summary>
    /// token生成类
    /// </summary>
    public class JwtTokenUtil
    {

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetToken(string name, string chineseName)
        {
            var claims = new[]
           {
                new Claim("Name", name)//用户名
                
            };
            // 获取密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SiteConfig.Secret));
            //凭证 ，根据密钥生成
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //   JWT 标准规定字段:
            //  iss: token 是给谁的  发送者
            //  aud: 接收的
            //  sub: token 主题
            //  exp: token 过期时间，Unix 时间戳格式
            //  iat: token 创建时间， Unix 时间戳格式
            //  jti: 针对当前 token 的唯一标识
            var token = new JwtSecurityToken(
                issuer: "zlsoft",
                audience: chineseName,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Token校验
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckToken(string token, out string name, out string username, out string deviceId, out string error)
        {
            name = "";
            error = "";
            username = "";//真实姓名
            deviceId = "";
            try
            {
                if (string.IsNullOrEmpty(token)) return false;

                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);

                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                //获取私钥
                string secret = GetSecret();
                Dictionary<string, string> playloadInfo = decoder.DecodeToObject<Dictionary<string, string>>(token, secret, true);
                if (playloadInfo != null)
                {
                    if (!string.IsNullOrEmpty(playloadInfo["Name"]))
                    {

                        name = playloadInfo["Name"];
                        username = playloadInfo["aud"];
                        deviceId = playloadInfo["DeviceId"];
                    }
                    else
                    {
                        error = "error:目标token格式不正确！";
                        return false;
                    }
                }
            }
            catch (TokenExpiredException)
            {
                error = "error:token超时，请重新获取！";
                return false;
            }
            catch (SignatureVerificationException)
            {
                error = "error:token签名无效！";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <returns></returns>
        private static string GetSecret()
        {
            string key = SiteConfig.Secret;
            return key;
        }


    }
}
