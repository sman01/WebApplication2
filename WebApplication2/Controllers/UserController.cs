using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        assignmentEntities2 UserDb = new assignmentEntities2();//Here we are calling Entityframework,in which your selected table place

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserDetails()

        {

            var result = UserDb.users.ToList();

            List<Users> users = new List<Users>();



            foreach (var Rows in result)

            {



                Users UserVar = new Users();
                UserVar.id = Rows.id;
                UserVar.name = Rows.name;

                UserVar.email = Rows.email;

                UserVar.designation = Rows.designation;

                UserVar.uname = Rows.uname;
                UserVar.access = Rows.access;

                users.Add(UserVar);

            }



            return View(users);

        }

        //Add record For Rendering the view or design

        public ActionResult Addrecord()

        {

            ViewBag.Message = "Add User Detail";

            return PartialView("Addrecord");

        }

        //Add Record in database

        [HttpPost]

        public ActionResult Addrecord(Users UserDetail)

        {

           user UsrVar = new user();
       if      ((UserDetail.id !=0) || (UserDetail.id!=null))
            UsrVar.id = UserDetail.id;  
            UsrVar.name = UserDetail.name;

            UsrVar.email= UserDetail.email;

            UsrVar.designation= UserDetail.designation;
            UsrVar.uname = UserDetail.uname;
            UsrVar.access = UserDetail.access;
            
            UserDb.users.AddOrUpdate(UsrVar);

            UserDb.SaveChanges();

            return RedirectToAction("UserDetails", "User");

        }

        //Get the Details and display in textbox for Edit

        public ActionResult GetUserDetail(int Id)

        {

            ViewBag.Message = "Edit User Detail";

            Users Usr = new Users();

            var Result = UserDb.users.Find(Id);
            Usr.id = Result.id; 
            Usr.name = Result.name;

            Usr.email = Result.email;

            Usr.designation = Result.designation;

            Usr.uname = Result.uname;
            Usr.access=Result.access;

            return PartialView("AddRecord", Usr);

        }

        //Value will be edit in database

        [HttpPost]

        public ActionResult GetUserDetail(Users Usr)

        {

            user UsrVar = UserDb.users.Where(m => m.id == Usr.id).FirstOrDefault();
            UsrVar.id= Usr.id;
            UsrVar.name = Usr.name;

            UsrVar.email = Usr.email;

            UsrVar.designation = Usr.designation;

            UsrVar.uname = Usr.uname;
            UsrVar.access = Usr.access;

            UserDb.SaveChanges();

            return RedirectToAction("UserDetails", "User");



        }
        public ActionResult Createnewuser()

        {

            ViewBag.Message = "Add User Detail";

            return PartialView("Createnewuser");

        }

        [HttpPost]
        //public ActionResult Createrecord(Users UserDetail)

        //{

        //    user UsrVar = new user();

        //    UsrVar.name = UserDetail.name;

        //    UsrVar.email = UserDetail.email;

        //    UsrVar.designation = UserDetail.designation;




        //    UserDb.users.AddOrUpdate(UsrVar);

        //    UserDb.SaveChanges();

        //    return RedirectToAction("UserDetails", "User");

        //}



        public ActionResult Createnewuser(NewUser User)

        {

            String strConnString = "Data Source=.\\sqlexpress;Initial Catalog=assignment;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            SqlConnection con = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "newCred";

            cmd.Parameters.Add("@uname", SqlDbType.VarChar).Value = User.name;

            cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = User.pass;

            cmd.Parameters.Add("@access", SqlDbType.Int).Value = User.access;

            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = User.name;

            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = User.email;
            cmd.Parameters.Add("@designation", SqlDbType.VarChar).Value = User.designation;


            cmd.Connection = con;

            try

            {

                con.Open();

                cmd.ExecuteNonQuery();

                return RedirectToAction("UserDetails", "User");
            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

                con.Dispose();

            }

        }



        //For Deleting the record

        [HttpPost]

        //public ActionResult DeleteUser(string uname)

        //{

        //    user User = UserDb.users.Where(m => m.uname.Contains(uname)).FirstOrDefault();
        //    credential Cred = UserDb.credentials.Where(m => m.uname.Contains(uname)).FirstOrDefault();

        //    UserDb.users.Remove(User);
        //    UserDb.credentials.Remove(Cred);


        //    UserDb.SaveChanges();

        //    string Success = "success";

        //    return Json(Success);

        //}
        public ActionResult DeleteUser(string id)

        {

            String strConnString = "Data Source=.\\sqlexpress;Initial Catalog=assignment;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";

            SqlConnection con = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "dlcust";

            cmd.Parameters.Add("@uname", SqlDbType.VarChar).Value = id;



            cmd.Connection = con;

            try

            {

                con.Open();

                cmd.ExecuteNonQuery();

                return RedirectToAction("UserDetails", "User");
            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                con.Close();

                con.Dispose();

            }

        }

    }
}