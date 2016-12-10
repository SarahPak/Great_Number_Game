using System;
using Nancy;
namespace GreatNumber
{
    public class GameModule : NancyModule
    {
        public GameModule()
        {
            Get("/", args => 
            {   
                if (Session["currRandnum"] == null) 
                {
                    Session["currRandnum"] = new Random().Next(1, 101);
                }
                ViewBag.Show = true;
                ViewBag.Randnum = Session["currRandnum"];
                if (((string)Session["check"]) == "toolow")
                {
                    ViewBag.Show = false;
                    ViewBag.Low = true;
                }
                else if (((string)Session["check"]) == "toohigh") 
                {
                    ViewBag.Show = false;
                    ViewBag.High = true;
                }
                else if (((string)Session["check"]) == "correct") 
                {
                    ViewBag.Show = false;
                    ViewBag.Correct = true;
                }
                Console.WriteLine(Session["currRandnum"]);
                return View["Game"];
            });

            Post("/formsubmitted", args =>
            {
                int myNum = Request.Form.myGuess;
                if (myNum < (int)(Session["currRandnum"]))
                {
                    Session["check"] = "toolow";
                } 
                else if (myNum > (int)(Session["currRandnum"])) 
                {
                    Session["check"] = "toohigh";
                }
                else if (myNum == (int)(Session["currRandnum"])) 
                {
                    Session["check"] = "correct";
                }
                return Response.AsRedirect("/");
            });

            Post("/reset", args => 
            {
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });           
        }
    }
}