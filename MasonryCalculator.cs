using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MasonryQuantities.Models
{
    public class MasonryCalculator
    {
        private const double blockLength = 0.44;
        private const double blockHeight = 0.215;
        private const double blockDepth = 0.100;
        private const double mortarHeight = 0.012;

        public static string[] Blocktypes
        {
            get
            {
                return new string[] {"Solid on edge", "Solid on flat", "Cavity Block" };
            }
        }
        public static string[] BlockStrengths
        {
            get
            {
                return new string[] {"Please Choose one..", "1 Newton/mm2", "2 Newton/mm2", "5 Newton/mm2", "10 Newton/mm2" };
            }
        }
        [Required]
        [Display(Name = "Select a type of block")]
        public string BlockType { get; set; }

        [Required]
        [Display(Name = "Select a Compressive strength of the block")]
        public string BlockStrength { get; set; }

        [Required]
        [Display(Name = "Wall Height")]
        public double WallHeight { get; set; }

        [Required]
        [Display(Name = "Wall Length")]
        public double WallLength { get; set; }

        
        [Display(Name = "Specify an Easting for the wall (e.g. 1001.234):")]
        public double WallEast { get; set; }


        [Display(Name = "Specify a Northing for the wall (e.g. 1001.523):")]
        public double WallNorth { get; set; }

        public static double CalcNumBlocks(double wallHeight, double wallLength, string blockType)
        {
            if (blockType == "Solid on edge" || blockType == "Cavity Block")
            {
                double numBlocks = ((wallLength / blockLength) * (wallHeight / blockHeight));

                return Math.Round(numBlocks, 0);
            }
            else if (blockType == "Solid on flat")
            {
                double numBlocks = ((wallLength / blockLength) * (wallHeight / blockHeight))*2;
                return Math.Round(numBlocks, 0);

            }
            else
            {
                throw new ArgumentException("Invalid");
            }
        }
        public static double CalcMortarAmount(double wallHeight, double wallLength, string blockType)
        {
            if (blockType == "Solid on edge" || blockType == "Cavity Block")
            {
                double mortarAmount = ((blockHeight * mortarHeight) * (wallLength / blockLength)) + ((mortarHeight * wallLength) * (wallHeight / blockHeight));
                return Math.Round(mortarAmount, 2);
            }
            else if (blockType == "Solid on flat")
            {
                double mortarAmount = (((blockHeight * mortarHeight) * (wallLength / blockLength)) + ((mortarHeight * wallLength) * (wallHeight / blockHeight)))*2;
                mortarAmount = mortarAmount * blockDepth;
                return Math.Round(mortarAmount, 2);
            }
            else
            {
                throw new ArgumentException("Invalid");
            }
        }
        public void AddWallToDB(MasonryCalculator MC)
        {
            ProjectMunitEntities3 pmu = new ProjectMunitEntities3();
            WallInformation walli = new WallInformation();            
            walli.WallHeight = MC.WallHeight.ToString();
            walli.Walllength = MC.WallLength.ToString();
            walli.BlockType = MC.BlockType;
            pmu.WallInformations.Add(walli);
            pmu.SaveChanges();
        }



    }
}