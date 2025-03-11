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
        public string SystemType { get; set; }

        [BindProperty]
        public double CalculationParameter { get; set; }

        [BindProperty]
        public double Ratio { get; set; }

        public IActionResult OnPost()
        {
            ASystem simulation = SystemType switch
            {
                "Solar" => new SolarSystem(),
                "Eolic" => new EolicSystem(),
                "Hydroelectric" => new HydroelectricSystem(),
                _ => null
            };

            if (simulation == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid system type.");
                return Page();
            }

            simulation.Date = DateTime.Now;
            simulation.value = CalculationParameter;
            simulation.ratio = Ratio;
            simulation.CalculateEnergy();

            string path = Path.Combine(Directory.GetCurrentDirectory(), "ModelData", "simulacions_energia.csv");
            bool fileExists = System.IO.File.Exists(path);
            using (var writer = new StreamWriter(path, append: true))
            {
                if (!fileExists)
                {
                    writer.WriteLine("DateGenerated|SystemType|CalculationParameter|Ratio|EnergyGenerated");
                }
                writer.WriteLine($"{simulation.Date.ToString("yyyy-MM-dd")}|{simulation.SimulationType}|{simulation.value}|{simulation.ratio}|{simulation.energyGenerated}");
                writer.WriteLine(); // Añadir una nueva línea después de cada entrada
            }

            return RedirectToPage("/Simulations");
        }
    }
}