using InfoTrack_Dev.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace InfoTrack_Dev.Controllers
{
    public class HomeController : Controller
    {

        //populate model
        public Search search = new Search()
        {
            keyword = @"infotrack",
            filename = @"online title search - Google Search.html",
            urlCount = 0,
            textCount = 0,
            lines = new LinkedList<int>(),
            error = ""
        };

        public ActionResult Index()
        {
            //Gets Data
            getCount(search.keyword);
            
            ViewBag.Message = search;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection frmobj) //FormCollection  
        {
            //Gets value from form
            string TempKeyword = frmobj["KeywordInput"];
            string TempFile = frmobj["FileInput"];

            //Checks Strings are valid
            if (string.IsNullOrEmpty(TempKeyword) || string.IsNullOrEmpty(TempFile))
            {
                ResetSearch();
                search.error = "Invalid Input";
                ViewBag.Message = search;
                return View();
            }

            //Reset and start new search
            ResetSearch();
            search.keyword = TempKeyword;
            search.filename = TempFile;
            getCount(search.keyword);

            ViewBag.Message = search;
            return View();
        }

        //Resets model
        public void ResetSearch()
        {
            search.keyword = null;
            search.filename = null;
            search.urlCount = 0;
            search.textCount = 0;
            search.lines = new LinkedList<int>();
            search.error = "";
        }

        //Returns count of keywords in file
        public Search getCount(string keyword)
        {
            int lineCount = 0;            
            string line;
            //Catch exception if file is missing
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Server.MapPath("~/" + search.filename));

                while ((line = file.ReadLine()) != null)
                {

                    //Tracks place in file
                    lineCount++;

                    //Checks match on URL
                    if (line.Contains(BuildURL(search.keyword)))
                    {
                        search.lines.AddLast(lineCount);
                        search.urlCount++;
                    }

                    //Else changes match on keyword
                    else if (line.Contains(search.keyword))
                    {
                        search.lines.AddLast(lineCount);
                        search.textCount++;

                    }
                }
            }

            //Catch invalid files
            catch (FileNotFoundException e)
            {
                search.error = "No file found... Applying default";
                search.filename = @"online title search - Google Search.html";
                Console.WriteLine(e);
            }
            return search;
        }

        //Builds URL for search
        public string BuildURL(string url)
        {
            return "www." + url +".com.au";
        }
    }
}