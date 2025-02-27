using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing;
using T4_Pr1_Hugo_energies_2.Model;

namespace T4_Pr1_Hugo_energies_2.Pages
{
    public class SolutionsModel : PageModel
    {
		public List<ASystem> EnergySystems { get; set; } = new List<ASystem>();

		public void OnGet()
        {
            string path = @"ModelData\simulacions_energia.csv";
            if(System.IO.File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split('|');
                        if (values[1] == "Solar")
                        {
                            SolarSystem solar = new SolarSystem();

                            solar.Date = DateTime.Parse(values[0]);
                            solar.value = double.Parse(values[2]);
                            solar.ratio = double.Parse(values[3]);
                            /*solar.costPerKWh = double.Parse(values[4]);
                            solar.pricePerKWh = double.Parse(values[5]);
                            solar.totalCost = double.Parse(values[6]);
                            solar.totalPrice = double.Parse(values[7]);*/
							solar.energyGenerated = double.Parse(values[4]);

							EnergySystems.Add(solar);
                        }
                        else if (values[1] == "Eolic")
                        {
                            EolicSystem eolic = new EolicSystem();
                            eolic.Date = DateTime.Parse(values[0]);
                            eolic.value = double.Parse(values[2]);
                            eolic.ratio = double.Parse(values[3]);
                            eolic.costPerKWh = double.Parse(values[4]);
                            eolic.pricePerKWh = double.Parse(values[5]);
                            eolic.totalCost = double.Parse(values[6]);
                            eolic.totalPrice = double.Parse(values[7]);
                            eolic.energyGenerated = eolic.CalculateEnergy();
                            EnergySystems.Add(eolic);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found");
            }


        }
    }
}
