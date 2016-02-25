using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        [AllowHtml]
        public string Text { get; set; }
    }
}