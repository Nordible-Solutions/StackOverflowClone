﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Devcorner.NIdenticon;
using Devcorner.NIdenticon.BrushGenerators;

namespace UI.Controllers
{
    public class NIdenticonController : Controller
    {
        [Route("nidenticon/{dimension:range(10,500)=50}/{text?}")]
        public ActionResult Index(int dimension, string text)
        {
            text = text ?? HttpContext.Request.UserHostAddress;
            var ic = new IdenticonGenerator();
            ic.DefaultBrushGenerator = new StaticColorBrushGenerator(StaticColorBrushGenerator.ColorFromText(text));
            var bitmap = ic.Create(text, new Size(dimension, dimension));

            var ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            return File(ms.ToArray(), "image/png");
        }

    }
}