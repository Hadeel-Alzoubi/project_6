using MasterPiece_UsingMVCBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MasterPiece_UsingMVCBackEnd.Controllers
{
    public class HomeController : Controller
    {

        private MasterPieceEntities db = new MasterPieceEntities();



        public ActionResult Index()
        {
            return View(db.categories.ToList());
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

        public ActionResult login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login([Bind(Include = "id,email,pass")] user_ user)
        {


            if (ModelState.IsValid)
            {
                var u = db.user_.FirstOrDefault(validate => validate.email == user.email && validate.pass == user.pass);
                if (u != null)
                {
                    Session["em"] = u.email;
                    Session["id"] = u.id;
                    Session["name"] = u.name;
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(user);
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register([Bind(Include = "id,email,pass,name,adress,phoneNo")] user_ user)
        {


            if (ModelState.IsValid)
            {
                db.user_.Add(user);
                db.SaveChanges();
                return RedirectToAction("login");
            }

            return View(user);
        }

        public ActionResult ProductList(string sortOrder)
        {
            // Fetch products from the database
            var products = db.product_list.ToList();

            // Apply sorting logic based on sortOrder
            switch (sortOrder)
            {
                case "mostRecent":
                    products = products.OrderByDescending(p => p.date_added).ToList();
                    break;
                case "mostPopular":
                    products = products.OrderByDescending(p => p.popularity).ToList();
                    break;
                case "bestSelling":
                    products = products.OrderByDescending(p => p.sales_count).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.name).ToList();
                    break;
            }

            var productViewModels = products.Select(p => new product_list
            {
                id = p.id,
                name = p.name,
                description = p.description,
                price = p.price,
                img = p.img,
                //Rating = p.Rating
            }).ToList();

            return View(productViewModels);
        }

        public ActionResult AddToCart(int categoryId)
        {

            var em = Session["em"];
            
            if (em == null)
            {
                return RedirectToAction("login", "Home");
            }

            var userId = (int)Session["id"];
            string x = Convert.ToString(categoryId);

            var product = db.product_list.FirstOrDefault(p => p.id == categoryId);
            // تحقق ما إذا كان المنتج موجودًا بالفعل في سلة التسوق
            var existingCart = db.carts.FirstOrDefault(c => c.user_id == userId && c.product_id == x);

            if (existingCart == null)
            {
                var cartItem = new cart
                {
                    user_id = userId,
                    product_id = x,
                    img = product.img,
                    name = product.name,
                    price = product.price,

                    quantity = 1 // Set initial quantity to 1
                };

                db.carts.Add(cartItem);
            }
            else
            {
                existingCart.quantity++; // Increment quantity if already in cart
            }

            db.SaveChanges();

            TempData["Message"] = "Product added to cart!";
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            var userId = (int?)Session["id"];


            var cartItems = db.carts.Where(c => c.user_id == userId.Value).ToList();

            return View(cartItems);
        }

        public ActionResult RemoveFromCart(string id)
        {
            var cartItem = db.carts.FirstOrDefault(c => c.product_id == id);
            if (cartItem != null)
            {
                db.carts.Remove(cartItem);
                db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        public ActionResult sort(string sortOrder)
        {
            var products = from p in db.product_list
                           select p;

            switch (sortOrder)
            {
                case "mostRecent":
                    products = products.OrderByDescending(p => p.date_added); // Assuming DateAdded is a column
                    break;
                case "mostPopular":
                    products = products.OrderByDescending(p => p.popularity); // Assuming Popularity is a column
                    break;
                case "bestSelling":
                    products = products.OrderByDescending(p => p.sales_count); // Assuming SalesCount is a column
                    break;
                default:
                    products = products.OrderBy(p => p.name);
                    break;
            }

            return View(products.ToList());
        }

        public ActionResult Products(int categoryId)
        {
            var products = db.product_list.Where(p => p.category_id == categoryId).ToList();
            return View(products);
        }

        public ActionResult show(int categoryId)
        {
            var products = db.product_list.Where(p => p.id == categoryId).ToList();
            return View(products);
        }
        public ActionResult productsAll()
        {
            var products = db.product_list.ToList();
            return View(products);
        }
        public ActionResult profile()
        {

            var userId = Session["id"];

            if (userId == null)
            {
                return RedirectToAction("login");
            }

            int user;
            if (int.TryParse(Session["id"].ToString(), out user))
            {
              
                var user_ = db.user_.FirstOrDefault(u => u.id == user);
                if (user_ == null)
                {
                    return HttpNotFound("User not found");
                }
                return View(user_);
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult profile(user_ model)
        {
            // تحديث بيانات المستخدم
            if (ModelState.IsValid)
            {
                var userInDb = db.user_.FirstOrDefault(u => u.id == model.id);

                if (userInDb != null)
                {
                    userInDb.pass = model.pass;
                    userInDb.name = model.name;
                    userInDb.email = model.email;
                    userInDb.adress = model.adress;
                    userInDb.phoneNo = model.phoneNo;

                    db.SaveChanges();

                    TempData["Message"] = "Information updated successfully!";
                }

                return RedirectToAction("profile");
            }

            return View(model);
        }


        public ActionResult logout()
        {
            // Clear session
            Session["em"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult product_detail()
        {
            return View();
        }
        public ActionResult product_detail2()
        {
            return View();
        }
        public ActionResult product_detail3()
        {
            return View();
        }
        public ActionResult product_detail4()
        {
            return View();
        }
        public ActionResult product_detail5()
        {
            return View();
        }
        public ActionResult product_detail6()
        {
            return View();
        }
        public ActionResult product_detail7()
        {
            return View();
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index"); // Redirect or handle empty query
            }

            // Perform search query
            var results = db.product_list
                .Where(p => p.name.Contains(query) || p.description.Contains(query))
                .ToList();

            // Return results to a view
            return View(results);
        }
    }
}