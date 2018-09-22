using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
         private EcommerceContext _context;
        
            public HomeController(EcommerceContext context)
            {
                _context = context;
            }
            public IActionResult Index()
            {
                return View("index");
            }
            [HttpPost("RegisterProcess")]
            public IActionResult Register(RegisterViewModel user){
                    if(ModelState.IsValid){
                        var userList = _context.users.Where(p => p.email== user.register_email).FirstOrDefault();
                        if(userList != null){
                            if(user.register_email == userList.email){
                                ModelState.AddModelError("register_email", "email exists");
                                return View("index");
                            }
                        }                                     
                    PasswordHasher<RegisterViewModel> Hasher = new PasswordHasher<RegisterViewModel>();
                    user.register_password = Hasher.HashPassword(user, user.register_password);
                    users User = new users(){
                        first_name = user.first_name,
                        last_name = user.last_name,
                        email = user.register_email,
                        password = user.register_password,
                        level = "user",
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(User);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("Id", (int)User.id);
                    if(User.level == "admin"){
                        return RedirectToAction("Dashboard");
                    }else{
                        return RedirectToAction("UserDashboard");
                    }              
                }else{
                    return View("index");
                }
            }

            [HttpPost("LoginProcess")]
            public IActionResult Login(LoginViewModel User){
                if(ModelState.IsValid){
                    users user = _context.users.Where(p => p.email== User.login_Email).SingleOrDefault();
                    if(user == null){
                        ModelState.AddModelError("login_Email", "Not a valid email or password");
                        return View("index");
                    }
                    else if(user != null && User.login_Password != null)
                        {
                            var Hasher = new PasswordHasher<users>();
                            if( 0 !=Hasher.VerifyHashedPassword(user, user.password, User.login_Password)){
                                HttpContext.Session.SetInt32("Id", (int)user.id);
                                int? id = HttpContext.Session.GetInt32("Id");
                                if(user.level == "admin"){
                                    return RedirectToAction("Dashboard");
                                }else{
                                    return RedirectToAction("UserDashboard");
                                }
                            }else{
                                ModelState.AddModelError("login_Password", "Not a valid email or password");
                                return View("index");
                            }
                    }                 
                }
                return View("index");
            }
            
            [HttpGet("dashboard")]
            public IActionResult Dashboard(){
                return View("dashboard");
            }

            [HttpGet("products")]
            public IActionResult Products(){
                List <products> TopProducts = _context.products.ToList();
                ViewBag.allProducts = TopProducts;
                return View("product");
            }
            [HttpPost("createcmt")]
            public IActionResult CreateCmt(commentViewModel newcmt, int productid){
                int? id = HttpContext.Session.GetInt32("Id");
                if(ModelState.IsValid){
                    comments newCmt = new comments{
                    productsid = productid,
                    usersid = (int)id,
                    comment = newcmt.comment,
                    rating = newcmt.rating,
                    created_at = DateTime.Now
                };
                _context.Add(newCmt);
                _context.SaveChanges();
                return RedirectToAction("productinfo",new{productid = productid});
                }
                return View("productinfo",new{productid = productid});
            }

            [HttpPost("removecmt")]
            public IActionResult RemoveCmt(int cmtid,int productid){
                 int? id = HttpContext.Session.GetInt32("Id");
                comments removecmt = _context.comments.Where(x=>x.id == cmtid).FirstOrDefault();
                _context.Remove(removecmt);
                _context.SaveChanges();
                return RedirectToAction("productinfo",new{productid = productid});
            }

            [HttpGet("products/new")]
            public IActionResult NewProduct(){
                return View("createproduct");
            }

            [HttpPost("products/create")]
            public IActionResult CreateProduct(ProductViewModel newproduct){
                int? id = HttpContext.Session.GetInt32("Id");
                if(ModelState.IsValid){
                    products product = new products(){
                        usersid = (int)id,
                        name = newproduct.Name,
                        img_name = newproduct.img,
                        inventory = newproduct.inventory,
                        description = newproduct.description,
                        price = newproduct.price,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(product);
                    _context.SaveChanges();
                    return RedirectToAction("Products");
                }
                return View("createproduct");
            }

            [HttpGet("products/{id}")]
            public IActionResult ProductDetail(int id){
                products ProCat = _context.products.Where(x=>x.id == id).Include(z=>z.productcategory).ThenInclude(x=>x.category).FirstOrDefault();
                List<categories> allCategories = _context.categories.Include(z=>z.CatPro).ThenInclude(x=>x.product).ToList();
                ViewBag.ProCat = ProCat;
                ViewBag.allCategories = allCategories;
                return View("productdetail");
            }

            [HttpPost("UpdateProduct")]
            public IActionResult UpdateProduct(ProductViewModel update, int productid){
                products updateproduct= _context.products.SingleOrDefault(x=>x.id == productid);
                updateproduct.name = update.Name;
                updateproduct.description =update.description;
                updateproduct.inventory = update.inventory;
                updateproduct.img_name = update.img;
                updateproduct.price = update.price;
                updateproduct.updated_at = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("productdetail", new{id = productid});
            }

            [HttpPost("AddCategory")]
            public IActionResult AddCategory(int categoryid, int productid){
                    productcategory connect = new productcategory(){
                    categoriesid = categoryid,
                    productsid = productid,
                };
                _context.Add(connect);
                _context.SaveChanges();
                return RedirectToAction("ProductDetail", new{id = productid});       
            }

            [HttpGet("categories")]
            public IActionResult Categories(){
                List <categories> allCategories = _context.categories.OrderBy(x=>x.name).ToList();
                ViewBag.allCategories = allCategories;
                return View("category");
            }


            [HttpGet("categories/new")]
            public IActionResult NewCategory(){
                return View("createcategory"); 
            }

            [HttpPost("categories/create")]
            public IActionResult CreateCategory(CategoryViewModel newCat){
                int? id = HttpContext.Session.GetInt32("Id");
                if(ModelState.IsValid){   
                    categories checkingCat = _context.categories.Where(x=>x.name == newCat.Name).FirstOrDefault();
                    if(checkingCat != null){
                        ModelState.AddModelError("name", "already in the database");
                        List <categories> category = _context.categories.OrderBy(x=>x.name).ToList();
                        ViewBag.allCategories = category;
                        return View("category");
                    }else{
                        categories newcat = new categories(){
                        usersid = (int)id,
                        name = newCat.Name,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(newcat);
                    _context.SaveChanges();
                    return RedirectToAction("Categories");
                    }                           
                }
                List <categories> allCategories = _context.categories.OrderBy(x=>x.name).ToList();
                ViewBag.allCategories = allCategories;
                return View("category");
            }


            [HttpGet("Customers")]
            public IActionResult Customers(){
                List<users> allCustomer= _context.users.Where(x=>x.level == "user").ToList();
                ViewBag.allCustomer = allCustomer;
                return View("customerlist");
            
            }
            [HttpGet("orders")]
            public IActionResult Order(){
                List<orders> allOrder = _context.orders.Include(x=>x.product).Include(x=>x.user).ToList();
                ViewBag.allOrder = allOrder;
                return View("orderlist");
            }

            [HttpGet("UserDashboard")]
            public IActionResult UserDashboard(){
                int? id = HttpContext.Session.GetInt32("Id");
                users UserInfo = _context.users.Where(x=>x.id == (int)id).Include(x=>x.UserOrder)
                .ThenInclude(x=>x.product).FirstOrDefault();
                List <products> allProducts = _context.products.ToList();
                ViewBag.UserInfo = UserInfo;
                ViewBag.allProducts = allProducts;
                return View("UserDashboard");
            }

            [HttpGet("productinfo/{productid}")]
            public IActionResult ProductInfo(int productid){
                int? id = HttpContext.Session.GetInt32("Id");
                products product = _context.products.Where(x=>x.id == productid)
                .Include(x=>x.productCmt).ThenInclude(x=>x.user).FirstOrDefault();
                users user = _context.users.Where(x=>x.id == (int)id).FirstOrDefault();
                ViewBag.product = product;
                ViewBag.user = user;
                return View("productinfo");
            }

            [HttpPost("helpless")]
            public IActionResult HelpLess(int commentid,int productid){
                int? id = HttpContext.Session.GetInt32("Id");
                helpless useranswer = new helpless(){
                    usersid = (int)id,
                    commentsid = commentid,
                };
                _context.Add(useranswer);
                _context.SaveChanges();
                return RedirectToAction("ProductInfo", new{productid = productid});
            }

            [HttpPost("helpful")]
            public IActionResult HelpFul(int commentid,int productid){
                int? id = HttpContext.Session.GetInt32("Id");
                helpful useranswer = new helpful(){
                    usersid = (int)id,
                    commentsid = commentid,
                };
                _context.Add(useranswer);
                _context.SaveChanges();
                return RedirectToAction("ProductInfo", new{productid = productid});                
            }

            [HttpPost("purchase")]
            public IActionResult PurchasePorduct(int productid, int amount){
                int? id = HttpContext.Session.GetInt32("Id");
                products PurchasedProduct = _context.products.FirstOrDefault(x=>x.id == productid);
                orders neworder = new orders(){
                    usersid = (int)id,
                    productsid = productid,
                    amount = amount,
                    price = Math.Round(PurchasedProduct.price * amount,2),
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now
                };
                _context.Add(neworder);
                PurchasedProduct.inventory -= neworder.amount;
                _context.SaveChanges();
                return RedirectToAction("UserOrder");
            }

            [HttpGet("userinfo")]
            public IActionResult UserInfo(){
                int? id = HttpContext.Session.GetInt32("Id");
                users profile = _context.users.Where(x=>x.id == (int)id).Include(x=>x.UserOrder)
                .ThenInclude(x=>x.product).FirstOrDefault();
                ViewBag.profile = profile;
                return View("userinfo");
            }
            [HttpGet("updateinfo")]
            public IActionResult UpdateInfo(){
                int? id = HttpContext.Session.GetInt32("Id");
                users profile = _context.users.Where(x=>x.id == (int)id).FirstOrDefault();
                List<addresses> AddressBook =_context.addresses.Where(x=>x.usersid == (int)id).ToList();
                ViewBag.profile = profile;
                ViewBag.AddressBook = AddressBook;
                return View("updateinfo");
            }
            [HttpPost("profilechange")]
            public IActionResult ProfileChange(RegisterViewModel update){
                int? id = HttpContext.Session.GetInt32("Id");
                users profile = _context.users.Where(x=>x.id == (int)id).FirstOrDefault();
                profile.first_name = update.first_name;
                profile.last_name = update.last_name;
                profile.email = update.register_email;
                profile.updated_at = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("UpdateInfo");
            }

            [HttpPost("createaddress")]
            public IActionResult CreateAddress(addressViewModel newAdd){
                int? id = HttpContext.Session.GetInt32("Id");
                if(ModelState.IsValid){
                    addresses newAddress = new addresses(){
                        usersid = (int)id,
                        address = newAdd.address,
                        apt = newAdd.apt,
                        zipcode = newAdd.zipcode,
                        city = newAdd.city,
                        state = newAdd.state,
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now
                    };
                    _context.Add(newAddress);
                    _context.SaveChanges();
                    return RedirectToAction("UpdateInfo");
                }
                return View("createaddress");
            }


            [HttpGet("logout")]
            public IActionResult logout(){
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
            }


    }
}
