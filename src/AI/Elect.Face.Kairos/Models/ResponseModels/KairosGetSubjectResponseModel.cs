using System.Collections.Generic;
using Elect.Face.Kairos.Models.InfoModels;
using Newtonsoft.Json;

namespace Elect.Face.Kairos.Models.ResponseModels
{
    public class KairosGetSubjectResponseModel : KairosBaseResponseModel
    {
        public List<KairosFaceIdModel> FaceIds { get; set; }
    }
}