using System.Threading.Tasks;
using bookpage.webui.EMailServices;
using bookpage.webui.Extensions;
using bookpage.webui.Identity;
using bookpage.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bookpage.webui.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;//UserManager ıdentity içinde varolan bir yapı. <User> clasımız.Usermanager kullanıcı oluşturma login parla sıfırlama gibi temel işlemleri barındırır.
        private SignInManager<User> _singInManager;//cookie işleri
        private IEmailSender _ıemailSender;

        public AccountController(UserManager<User> userManager,SignInManager<User> singInManager,IEmailSender IemailSender)
        {
            _userManager=userManager;
            _singInManager=singInManager;
            _ıemailSender=IemailSender;
        }
        public IActionResult Login(string returnUrl=null)
        {//mesela appte bir sayfaya girmek istedim login ister gireresem beni o en son bastıım sayfaya göndersin. bunun için yukarı string return url = null dedim
            return View(new LoginModel{
                ReturnUrl=returnUrl
            });//bunula bastım login.cshtml'de gizledim input olarak aşağıdada post edicem.Yani urldeki sayfayı açınca return nul yazan ve yanında olan şetler var onlaır alır burada tutar postlada gönderir
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }//eğer hata yoksa

            var user= await _userManager.FindByNameAsync(model.UserName);//vt'den verdiğim useri alalım bakalım varmı
            if(user==null)
            {
                TempData.Put("message",new AlertMessage()
                {
                    Title="Hata",
                    Message="Girdiğiniz kullanıcı adı kayıtlı değil lütfen tekrar deneyiniz.",
                    AlertType="danger"
                });
                return View(model);
            }
            var result= await _singInManager.PasswordSignInAsync(user,model.Password,true,false);//kullanıcının tarayıcısına coockie bırakıcaz. 1.parametre useri verdim, şifre bekliyo 2. şifreyi verdim. coockienin tarayıcı kapanınca silinip silinmemeisyle alakalı 3.false dedim tarayıcı kapanınca bilgi silinir,şimdi true yaptım 60 dksonra silinir. kullanıcı başarısız giriş yaparsa 4. false dersek hesap kilitlenme işlemi kapalı
            //kullanıcının confirmemail alanı false ise onu login yapma
            if(!await _userManager.IsEmailConfirmedAsync(user))//bu mail alanı onaylanmışmı demek true ise buraya girmez zaten direk login olur
            {
                TempData.Put("message",new AlertMessage()
                {
                    Title="Link Onayı",
                    Message="Lütfen Emailinize Gelen Link ile Üyeliğinizi Onaylayınız.",
                    AlertType="primary"
                });
                return View();
            }
            
           if(result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"~/");//değer nulsa anasayfaya gider ?? ".." ile 
            }
            ModelState.AddModelError("","Kullanıcı Adı veya Parola Yanlış");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//getle gönderilen token posta gelmiyosa işlemi gerçekleştirme buna crosside atakları denir
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user=new User()
            {
                FirstName=model.FirstName,
                LastName=model.LastName,
                UserName=model.UserName,
                Email=model.EMail,
            };//modeldekileri usere attım passwordi kashlememiz şifrelemememiz gerek açık olmaması gerek usermanager aracılığı ile halledeceğiz
            
            var result=await _userManager.CreateAsync(user,model.Password);//creatasync bir user bekliyodu verdik birde şifre onuda modelden gönderdik
            if(result.Succeeded)
            {  //token işlemi
                var code=await _userManager.GenerateEmailConfirmationTokenAsync(user);//verdiğin user bilgisiyle bize token oluşturuyo ve bu bilgi vtye kaydediiyo bizde bu bilgiyle onaylama yapıcaz ve şimdi url bilgilisi oluşturucaz ve bu eşleşiyosa bu durumda onaylama işlemi olacak
                string url=Url.Action("ConfirmEmail","Account",new{//bize uel oluşturacak metoddan accountun confrimine gider giderken userin user.Id sini ve tokeni gönder
                    userId=user.Id,
                    token=code
                });
                //email
                 await _ıemailSender.SendEmailAsync(model.EMail,"Hesabınızı Onaylayınız",$"Lütfen EMail Hesabınızı Onaylamak İçin Linke <a href='https://localhost:5001{url}'> Tıklayınız </a>");

                return RedirectToAction("Login","Account");
            }
           // ModelState.AddModelError("","Bilinmeyen bir hata oluştu, lütfen tekrar deneyiniz");//modalstateadderror mantığı model state içine gönderiyosun "" boş olduğu için en üstte verdiğin hata mesajı yazar
            TempData.Put("message",new AlertMessage()
                {
                    Title="Bilinmeyen bir hata oluştu",
                    Message="Bilinmeyen bir hata oluştu, lütfen tekrar deneyiniz.",
                    AlertType="danger"
                });
            return View(model);
        }

        public async Task<IActionResult> Logout(RegisterModel model)
        {
            await _singInManager.SignOutAsync();
            TempData.Put("message",new AlertMessage()
                {
                    Title=":(",
                    Message="Gidişin bizi üzüyor...",
                    AlertType="danger"
                });
            return Redirect("~/");
        }

          public async Task<IActionResult> ConfirmEmail(string userId,string token)//userId bilgisi hangi kullanıcı hesabı bu ve Token bilgisi benzersiz sayı gelecek ve kullanıya maili göndericez kullanıcıda buna tıklayınca hesabı onaylanacak
          {
             if(userId==null||token==null )//gelen userId bilgisi veya token nulsa
             {
                 TempData.Put("message",new AlertMessage()
                {
                    Title="Geçersiz Token",
                    Message="Geçersin Bir Token",
                    AlertType="danger"
                });
                 return View();
             }
             var user=await _userManager.FindByIdAsync(userId);//userId bilgisiyle vtyi kontrol edelim
             if(user!=null)// bu userIdsiyle eşleşen kayıt varmı
             {
                //eğer user nula eşit değilse
                 var result=await _userManager.ConfirmEmailAsync(user,token);//useri ve tokeni veririm oda confirmemail alanını true yapar
                 if(result.Succeeded)
                 {
                    TempData.Put("message",new AlertMessage()
                {
                    Title="Hesabınız Onaylandı",
                    Message="Tebrikler Hesabınız Onaylandı",
                    AlertType="success"
                });
                    return View();
                 }
             }
              TempData.Put("message",new AlertMessage()
                {
                    Title="Hata",
                    Message="Hesabınız Onaylanmadı",
                    AlertType="warning"
                });
              return View();
           }
        /* public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId==null || token ==null)
            {
                CreateMessage("Geçersiz token.","danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user!=null)
            {
                var result = await _userManager.ConfirmEmailAsync(user,token);
                if(result.Succeeded)
                {
                    CreateMessage("Hesabınız onaylandı.","success");
                    return View();
                }
            }
            CreateMessage("Hesabınızı onaylanmadı.","warning");
            return View();
        } */

         public IActionResult ForgotPassword()
         {
             return View();
         }
         [HttpPost]
         public async Task<IActionResult> ForgotPassword(string email)
         {
             if(string.IsNullOrEmpty(email))
             {
                 return View();
             }
             var user= await _userManager.FindByEmailAsync(email);//vtde bu bilgi varmı
             if(user==null)
             {
                 return View();
             }

             var token=await _userManager.GeneratePasswordResetTokenAsync(user);//zaten yukarıda ngeliyorsa user vardır ve resetler bekler await olduğu için neyi bekler email göndermemizi
             //şimdi email gönderme işlemi yapmamız gerkiyordu bunu yukarıdan aldım copy paste yaptım
             //token işlemi
                var code=await _userManager.GeneratePasswordResetTokenAsync(user);//tokeni oluşturduk emaili password olarak değiştiriyorum
                string url=Url.Action("ResetPassword","Account",new{//
                    userId=user.Id,
                    token=code
                });
                //email
                 await _ıemailSender.SendEmailAsync(email,"Şifre Yenileme",$"Parolanızı yenilemek için linke  <a href='https://localhost:5001{url}'> Tıklayınız </a>");

             return View();
         }
         public IActionResult ResetPassword(string userId, string token)//urlden ıdyi ve tokeni alcaz bunları bir hidden olarak oşuşturucaz ve servara post edicez
         {
             if(userId==null||token==null)
             {
                 return RedirectToAction("Home","Index");
             }
             var model=new ResetPasswordModel{
                 Token=token//input hidden olduğu için direk yerleşir
             };
             return View();   
         }
       /* [HttpPost]
         public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
         {//sayfada verdiğimiz bilgilerin yanında ekstra bilgileri alıcaz 
           //kullanıcı exsra mail ve yeni parola bilgisini girip gönderecek
           if(!ModelState.IsValid)
           {
               return View(model);
           }
           var user=await _userManager.FindByEmailAsync(model.Email);//bu email vtde varmı
           if(user==null)//eğer yoksa
           {
               return RedirectToAction("Home","Index");
           }
           //user bilgisi varsa bu aşamada resetleyelim
           var result=await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
           
           if(result.Succeeded)
           {
               RedirectToAction("Login","Account");
           }
             return View(model);   
         } */
         

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {//sayfada verdiğimiz bilgilerin yanında ekstra bilgileri alıcaz 
           //kullanıcı exsra mail ve yeni parola bilgisini girip gönderecek
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user= await _userManager.FindByEmailAsync(model.Email);
            if(user==null)
            {
                return RedirectToAction("Home","Index");
            }
                       //user bilgisi varsa bu aşamada resetleyelim
            var result=await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
            return View(model);
        }

        
    }
}