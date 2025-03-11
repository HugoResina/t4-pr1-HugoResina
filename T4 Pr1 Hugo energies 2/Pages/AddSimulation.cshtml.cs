using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;
using T4_Pr1_Hugo_energies_2.Model;

namespace T4_Pr1_Hugo_energies_2.Pages
{
    public class AddSimulationModel : PageModel
    {
        [BindProperty]
        public ASystem Simulation { get; set; }

        [BindProperty]
        public string SystemType { get; set; }

        [BindProperty]
        public double CalculationParameter { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            switch (SystemType)
            {
                case "Solar":
                    Simulation = new SolarSystem();
                    break;
                case "Eolic":
                    Simulation = new EolicSystem();
                    break;
                case "Hydroelectric":
                    Simulation = new HydroelectricSystem();
                    break;
            }

            Simulation.Date = DateTime.UtcNow;
            Simulation.value = CalculationParameter;
            Simulation.CalculateEnergy();
            Simulation.totalCost = Simulation.energyGenerated * Simulation.costPerKWh;
            Simulation.totalPrice = Simulation.energyGenerated * Simulation.pricePerKWh;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "ModelData", "simulacions_energia.csv");
            bool fileExists = System.IO.File.Exists(path);
            using (var writer = new StreamWriter(path, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("DateGenerated|SystemType|CalculationParameter|Ratio|EnergyGenerated|CostPerKWh|PricePerKWh|TotalCost|TotalPrice");
                }
                writer.WriteLine($"{Simulation.Date}|{Simulation.SimulationType}|{Simulation.value}|{Simulation.ratio}|{Simulation.energyGenerated}|{Simulation.costPerKWh}|{Simulation.pricePerKWh}|{Simulation.totalCost}|{Simulation.totalPrice}");
            }

            return RedirectToPage("/Simulations");
        }
    }
}