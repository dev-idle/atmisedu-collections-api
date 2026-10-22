using atMISEDU.Contract.Enumerations;

namespace atMISEDU.Contract.DTOs
{
    public class UploadFileRequest
    {
        public string Content { get; set; }
        public string RelativePath { get; set; }
        public string FileName { get; set; }
        public EnumOptionPath enumOptionPath { get; set; }
    }
}