using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasonryQuantities.Models;

namespace MasonryQuantities.Controllers
{
    public class BlockController : Controller
    {
        [HttpGet] 
        public ActionResult Index()
        {
            ViewBag.BlockType = new SelectList(MasonryCalculator.Blocktypes);
            ViewBag.BlockStrength = new SelectList(MasonryCalculator.BlockStrengths);
            return View();
        }
        [HttpPost]
        public ActionResult Index(MasonryCalculator MC, string Blocktype, string BlockStrength)
        {
            if (ModelState.IsValid)
            {
                MC.BlockType = Blocktype;
                MC.BlockStrength = BlockStrength;
                return RedirectToAction("Confirm", MC);
            }
            else
            {
                return View();
            }
        }
        public ActionResult Confirm(MasonryCalculator MC)
        {
            ViewBag.nmb = MasonryCalculator.CalcNumBlocks(MC.WallLength, MC.WallHeight, MC.BlockType);
            ViewBag.Mortamt = MasonryCalculator.CalcMortarAmount(MC.WallLength, MC.WallHeight, MC.BlockType);
            
            
            MC.AddWallToDB(MC);
            return View(MC);
        }

        public ActionResult DBInfor()
        {
            var entities = new ProjectMunitEntities3();
            return View(entities.WallInformations.ToList());           
        }
        

       
    }
}
