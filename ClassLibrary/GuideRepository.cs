using ClassLibrary.Data;
using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassLibrary.Models.Guide;
using static ClassLibrary.Models.Visit;

namespace ClassLibrary
{
    public class GuideRepository
    {
        private readonly ZooContext _context;
        public GuideRepository(ZooContext context)
        {
            _context = context;
        }
        public void AddGuide()
        {
     
        }

        public void UpdateGuide()
        {
        
        }

        public void DeleteGuide()
        {
            
        }

        public void ViewGuides()
        {
        }

        public void SeedGuides()
        {
            if (!_context.Guides.Any())
            {

                List<Guide> guides = new List<Guide>
                {
                    new Guide { FirstName = "Doctor Jones", GuideCompetence = Guide.Competence.Land},
                    new Guide { FirstName = "Professor Alan Grant", GuideCompetence = Guide.Competence.Air},
                    new Guide { FirstName = "Captain Nemo", GuideCompetence = Guide.Competence.Water},
                    new Guide { FirstName = "Doctor Beth Smith", GuideCompetence = Guide.Competence.Land}
                };

                foreach (var guide in guides)
                {
                    _context.Guides.Add(guide);
                }
                _context.SaveChanges();
            }
        }
     /// <summary>
     /// Gets us a list of guides with the right competence for the chosen animal
     /// </summary>
     /// <param name="animalId"></param>
     /// <returns>List of suitable guides</returns>
        public List<Guide> IsGuideCompetent(int animalId) 
        {
            Competence neededCompentence;

            var animalType = _context.Animals //Find out the habitat of chosen animal
                .Where(a => a.Id == animalId)
                .Select(a =>
                    a is Land ? "1" :
                    a is Water ? "2" :
                    a is Air ? "0" :
                    "Unknown"
                )
                .FirstOrDefault();

            int typeID = Convert.ToInt32(animalType); //create exception if parsing fails?

            if (typeID == 0)
            {
                neededCompentence = Competence.Air;
            }
            else if (typeID == 1)
            {
                neededCompentence = Competence.Land;
            }
            else
            {
                neededCompentence = Competence.Water;
            }

            return _context.Guides.Where(g => g.GuideCompetence == neededCompentence).ToList(); 
        }
        /// <summary>
        /// Picks the first competent and availbable guide.
        /// </summary>
        /// <param name="competentGuides"></param>
        /// <param name="visitDate"></param>
        /// <param name="timeSlot"></param>
        /// <returns>a guide object</returns>
        public Guide IsGuideAvailable(List <Guide> competentGuides, DateTime visitDate, TimeSlot timeSlot)
        {
            Guide chosenGuide = null;

            foreach (var guide in competentGuides)
            {
                var availableGuides = _context.Visits
                    .Where(v => v.GuideId == guide.Id) //current guide
                    .Where(v => v.VisitDate == visitDate) 
                    .Where(v => v.VisitTimeSlot == timeSlot) 
                    .ToList();

                if (availableGuides.Count == 0)
                {
                    //Sets the guide for the visit if they're not booked that day (at all)
                    chosenGuide = guide;
                    break;
                }
                else if (availableGuides.Any(g => g.VisitTimeSlot != timeSlot))
                {
                    //Sets the guide for the visit if they're not booked on that time slot already
                    chosenGuide = guide;
                    break;
                }            
            }
            return chosenGuide;


        }

    }
}
