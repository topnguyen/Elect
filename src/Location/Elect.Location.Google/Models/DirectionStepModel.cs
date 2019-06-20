#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.com/ </Url>
//     <Author> Top </Author>
//     <Project> Elect </Project>
//     <File>
//         <Name> DirectionStepModel.cs </Name>
//         <Created> 20/03/2018 11:57:22 AM </Created>
//         <Key> edbf8c9b-99bd-4743-a49e-88dfc447b473 </Key>
//     </File>
//     <Summary>
//         DirectionStepModel.cs is a part of Elect
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

namespace Elect.Location.Google.Models
{
    public class DirectionStepModel
    {
        public int Index { get; set; }

        public string Description { get; set; }

        public double Distance { get; set; }

        public string DistanceText { get; set; }

        public double Duration { get; set; }

        public string DurationText { get; set; }
    }
}